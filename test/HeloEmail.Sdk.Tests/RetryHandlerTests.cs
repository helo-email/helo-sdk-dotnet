using System.Net;
using System.Net.Http;

namespace HeloEmail.Sdk.Tests;

/// <summary>The retry policy every Helo SDK shares.</summary>
public class RetryHandlerTests
{
    /// <summary>Replies in order, recording every request it receives.</summary>
    private class SequenceHandler(params object[] replies) : HttpMessageHandler
    {
        private readonly Queue<object> _replies = new(replies);
        public List<HttpRequestMessage> Requests { get; } = new();

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            Requests.Add(request);
            var reply = _replies.Count > 1 ? _replies.Dequeue() : _replies.Peek();
            if (reply is Exception exception) throw exception;

            var (status, retryAfter) = ((int, string?))reply;
            var response = new HttpResponseMessage((HttpStatusCode)status) { Content = new StringContent("{}") };
            if (retryAfter != null) response.Headers.TryAddWithoutValidation("Retry-After", retryAfter);
            return Task.FromResult(response);
        }
    }

    private static (HttpMessageInvoker Invoker, SequenceHandler Handler) Create(int maxRetries, params object[] replies)
    {
        var handler = new SequenceHandler(replies);
        return (new HttpMessageInvoker(new RetryHandler(maxRetries) { InnerHandler = handler }), handler);
    }

    private static Task<HttpResponseMessage> Send(HttpMessageInvoker invoker, HttpMethod method, string? key = null)
    {
        var request = new HttpRequestMessage(method, "https://api.example.test/things");
        if (method != HttpMethod.Get) request.Content = new StringContent("{}");
        if (key != null) request.Headers.Add("X-Example-Idempotency-Key", key);
        return invoker.SendAsync(request, CancellationToken.None);
    }

    [Fact]
    public async Task RetriesAnIdempotentRequest()
    {
        var (invoker, handler) = Create(2, (503, "0"), (200, (string?)null));

        var response = await Send(invoker, HttpMethod.Get);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal(2, handler.Requests.Count);
    }

    [Fact]
    public async Task RetriesAPostCarryingAnIdempotencyKey()
    {
        var (invoker, handler) = Create(2, (502, "0"), (204, (string?)null));

        var response = await Send(invoker, HttpMethod.Post, "key-1");

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        Assert.Equal(2, handler.Requests.Count);
        Assert.Equal("{}", await handler.Requests[1].Content!.ReadAsStringAsync());
    }

    // Without a key, the server may have acted on the first attempt before failing.
    [Fact]
    public async Task DoesNotRetryAPostWithoutAnIdempotencyKey()
    {
        var (invoker, handler) = Create(2, (503, "0"));

        var response = await Send(invoker, HttpMethod.Post);

        Assert.Equal(HttpStatusCode.ServiceUnavailable, response.StatusCode);
        Assert.Single(handler.Requests);
    }

    [Fact]
    public async Task DoesNotRetryAPostWithoutAKeyAfterANetworkError()
    {
        var (invoker, handler) = Create(2, new HttpRequestException("connection refused"));

        await Assert.ThrowsAsync<HttpRequestException>(() => Send(invoker, HttpMethod.Post));
        Assert.Single(handler.Requests);
    }

    [Fact]
    public async Task RetriesANetworkErrorOnAnIdempotentRequest()
    {
        var (invoker, handler) = Create(2, new HttpRequestException("connection reset"), (200, (string?)null));

        var response = await Send(invoker, HttpMethod.Get);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal(2, handler.Requests.Count);
    }

    // A 429 means the server refused the request without acting on it.
    [Fact]
    public async Task RetriesARateLimitedPostEvenWithoutAKey()
    {
        var (invoker, handler) = Create(2, (429, "0"), (204, (string?)null));

        var response = await Send(invoker, HttpMethod.Post);

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }

    [Fact]
    public async Task DoesNotRetryEarlyWhenRetryAfterIsLongerThanItWaits()
    {
        var (invoker, handler) = Create(2, (429, "60"));

        var response = await Send(invoker, HttpMethod.Get);

        Assert.Equal((HttpStatusCode)429, response.StatusCode);
        Assert.Single(handler.Requests);
    }

    [Fact]
    public async Task GivesUpAfterMaxRetries()
    {
        var (invoker, handler) = Create(1, (500, "0"));

        var response = await Send(invoker, HttpMethod.Get);

        Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
        Assert.Equal(2, handler.Requests.Count);
    }

    [Fact]
    public async Task MaxRetriesZeroDisablesRetries()
    {
        var (invoker, handler) = Create(0, (503, "0"));

        await Send(invoker, HttpMethod.Get);

        Assert.Single(handler.Requests);
    }

    /// <summary>Never answers its first <c>hangs</c> requests, as a stalled server wouldn't.</summary>
    private class HangingHandler(int hangs) : HttpMessageHandler
    {
        public int Attempts { get; private set; }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            if (++Attempts <= hangs) await Task.Delay(Timeout.Infinite, cancellationToken);
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }

    private static (HttpMessageInvoker Invoker, HangingHandler Handler) CreateHanging(int maxRetries, int hangs)
    {
        var handler = new HangingHandler(hangs);
        var retry = new RetryHandler(maxRetries, TimeSpan.FromMilliseconds(50)) { InnerHandler = handler };
        return (new HttpMessageInvoker(retry), handler);
    }

    [Fact]
    public async Task AbandonsAnAttemptThatTakesTooLongAndRetriesIt()
    {
        var (invoker, handler) = CreateHanging(2, hangs: 1);

        var response = await Send(invoker, HttpMethod.Get);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal(2, handler.Attempts);
    }

    // The shape HttpClient's own Timeout throws, so existing catch blocks still apply.
    [Fact]
    public async Task ThrowsATimeoutAfterTheLastAttempt()
    {
        var (invoker, handler) = CreateHanging(1, hangs: 2);

        var e = await Assert.ThrowsAsync<TaskCanceledException>(() => Send(invoker, HttpMethod.Get));

        Assert.IsType<TimeoutException>(e.InnerException);
        Assert.Equal(2, handler.Attempts);
    }

    [Fact]
    public async Task DoesNotRetryWhenTheCallerCancels()
    {
        var handler = new HangingHandler(hangs: 2);
        var invoker = new HttpMessageInvoker(new RetryHandler(2, Timeout.InfiniteTimeSpan) { InnerHandler = handler });
        using var cts = new CancellationTokenSource(TimeSpan.FromMilliseconds(50));

        var request = new HttpRequestMessage(HttpMethod.Get, "https://api.example.test/things");
        var e = await Assert.ThrowsAnyAsync<OperationCanceledException>(() => invoker.SendAsync(request, cts.Token));

        Assert.IsNotType<TimeoutException>(e.InnerException);
        Assert.Equal(1, handler.Attempts);
    }
}
