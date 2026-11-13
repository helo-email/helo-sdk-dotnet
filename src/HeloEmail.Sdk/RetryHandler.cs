using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace HeloEmail.Sdk
{
    /// <summary>
    /// Retries a request (up to <see cref="MaxRetries"/> times, with jittered backoff)
    /// after a network failure or a 408, 429, 500, 502, 503, 504 response, but only when
    /// repeating it cannot do anything twice: its method is idempotent, or it carries an
    /// idempotency key. A 429 is retried regardless, since the server refused it without
    /// acting on it. A <c>Retry-After</c> longer than the SDK waits is not retried early;
    /// the error goes to the caller.
    /// </summary>
    /// <remarks>
    /// <c>AddHelo()</c> adds this to the client it registers. If you register your
    /// own <see cref="HttpClient"/>, add it with <c>.AddHttpMessageHandler(() => new RetryHandler())</c>.
    /// Each attempt is abandoned after <see cref="AttemptTimeout"/> and retried like a network
    /// failure. <see cref="HttpClient.Timeout"/> (100s by default) covers every attempt together
    /// and would cut them short, so a client using this handler should set it to
    /// <see cref="Timeout.InfiniteTimeSpan"/>, as <c>AddHelo()</c> does.
    /// </remarks>
    public class RetryHandler : DelegatingHandler
    {
        /// <summary>How many times a failed request is retried unless configured otherwise.</summary>
        public const int DefaultMaxRetries = 2;

        /// <summary>How long each attempt may take unless configured otherwise, as in every Helo SDK.</summary>
        public static readonly TimeSpan DefaultAttemptTimeout = TimeSpan.FromSeconds(30);

        private const double InitialRetryDelay = 0.5;
        private const double MaxRetryDelay = 8.0;

        private static readonly HashSet<int> RetryStatuses = new HashSet<int> { 408, 429, 500, 502, 503, 504 };

        private static readonly HashSet<string> IdempotentMethods =
            new HashSet<string> { "GET", "HEAD", "OPTIONS", "PUT", "DELETE" };

        private static readonly Random Jitter = new Random();

        /// <param name="maxRetries">How many times a failed request is retried; 0 disables retries.</param>
        /// <param name="attemptTimeout">How long each attempt may take (default <see cref="DefaultAttemptTimeout"/>);
        /// <see cref="Timeout.InfiniteTimeSpan"/> disables it.</param>
        public RetryHandler(int maxRetries = DefaultMaxRetries, TimeSpan? attemptTimeout = null)
        {
            MaxRetries = maxRetries;
            AttemptTimeout = attemptTimeout ?? DefaultAttemptTimeout;
        }

        /// <summary>How many times a failed request is retried; 0 disables retries.</summary>
        public int MaxRetries { get; }

        /// <summary>How long each attempt may take; <see cref="Timeout.InfiniteTimeSpan"/> if unlimited.</summary>
        public TimeSpan AttemptTimeout { get; }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            var canRepeat = IdempotentMethods.Contains(request.Method.Method.ToUpperInvariant()) ||
                            request.Headers.Any(h => h.Key.EndsWith("Idempotency-Key", StringComparison.OrdinalIgnoreCase));

            for (var attempt = 0; ; attempt++)
            {
                HttpResponseMessage response;
                // Its own timer per attempt, so a retry gets the whole timeout; still linked to the
                // caller's token, so cancelling the call cancels the attempt.
                using (var attemptCts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken))
                {
                    if (AttemptTimeout != Timeout.InfiniteTimeSpan) attemptCts.CancelAfter(AttemptTimeout);
                    try
                    {
                        response = await base.SendAsync(request, attemptCts.Token).ConfigureAwait(false);
                    }
                    catch (HttpRequestException) when (canRepeat && attempt < MaxRetries)
                    {
                        await Task.Delay(BackoffDelay(attempt), cancellationToken).ConfigureAwait(false);
                        continue;
                    }
                    catch (OperationCanceledException e) when (!cancellationToken.IsCancellationRequested)
                    {
                        // The attempt's timer, not the caller: a timeout, retried like a network failure.
                        if (canRepeat && attempt < MaxRetries)
                        {
                            await Task.Delay(BackoffDelay(attempt), cancellationToken).ConfigureAwait(false);
                            continue;
                        }
                        // What HttpClient throws when its own Timeout elapses, so callers catch one shape.
                        var message = $"The request was canceled after {AttemptTimeout.TotalSeconds}s, the attempt timeout.";
                        throw new TaskCanceledException(message, new TimeoutException(message, e));
                    }
                }

                var delay = RetryDelay(response, attempt, canRepeat);
                if (delay == null) return response;

                response.Dispose();
                await Task.Delay(delay.Value, cancellationToken).ConfigureAwait(false);
            }
        }

        private TimeSpan? RetryDelay(HttpResponseMessage response, int attempt, bool canRepeat)
        {
            var status = (int)response.StatusCode;
            if (attempt >= MaxRetries || !RetryStatuses.Contains(status)) return null;
            if (status != 429 && !canRepeat) return null;

            var retryAfter = RetryAfter(response);
            if (retryAfter == null) return BackoffDelay(attempt);
            return retryAfter.Value.TotalSeconds <= MaxRetryDelay ? retryAfter : null;
        }

        /// <summary>Exponential backoff with full jitter.</summary>
        private static TimeSpan BackoffDelay(int attempt)
        {
            var ceiling = Math.Min(MaxRetryDelay, InitialRetryDelay * Math.Pow(2, attempt));
            double fraction;
            lock (Jitter) fraction = Jitter.NextDouble();
            return TimeSpan.FromSeconds(fraction * ceiling);
        }

        /// <summary>Retry-After, as either seconds or an HTTP date.</summary>
        private static TimeSpan? RetryAfter(HttpResponseMessage response)
        {
            var header = response.Headers.RetryAfter;
            if (header == null)
            {
                // A fractional number of seconds isn't valid to the typed header, so it lands here.
                IEnumerable<string> raw;
                double seconds;
                if (response.Headers.TryGetValues("Retry-After", out raw) &&
                    double.TryParse(raw.FirstOrDefault(), NumberStyles.Float, CultureInfo.InvariantCulture, out seconds))
                    return TimeSpan.FromSeconds(Math.Max(seconds, 0));
                return null;
            }
            if (header.Delta != null) return header.Delta;
            if (header.Date == null) return null;

            var delay = header.Date.Value - DateTimeOffset.UtcNow;
            return delay > TimeSpan.Zero ? delay : (TimeSpan?)null;
        }
    }
}
