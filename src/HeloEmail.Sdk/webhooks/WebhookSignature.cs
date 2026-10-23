using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace HeloEmail.Sdk.Webhooks
{
    /// <summary>
    /// Why a webhook signature was rejected. See <see cref="WebhookSignatureException.Error"/>.
    /// </summary>
    public enum WebhookSignatureError
    {
        /// <summary>
        /// The header was not in the documented <c>t={timestamp},v{version}={signature}</c> form.
        /// </summary>
        MalformedHeader,

        /// <summary>
        /// The header carried only signing schemes this SDK does not know how to verify.
        /// Upgrading the SDK is the fix; see <see cref="WebhookSignature.SupportedVersions"/>.
        /// </summary>
        UnsupportedVersion,

        /// <summary>
        /// The signature was correctly formed but its timestamp is too far from the current
        /// time, so it may be a replay.
        /// </summary>
        TimestampSkew,

        /// <summary>
        /// The signature did not match the body, either because the body was tampered with or
        /// the signing key is wrong.
        /// </summary>
        SignatureMismatch
    }

    /// <summary>
    /// Thrown by <see cref="WebhookSignature.Verify(string, string, string)"/> when a webhook
    /// cannot be trusted. <see cref="Error"/> says why.
    /// </summary>
    public class WebhookSignatureException : Exception
    {
        public WebhookSignatureException(WebhookSignatureError error, string message) : base(message)
        {
            Error = error;
        }

        /// <summary>
        /// The reason the signature was rejected.
        /// </summary>
        public WebhookSignatureError Error { get; }
    }

    /// <summary>
    /// Helpers for verifying the signature on an incoming webhook request.
    /// </summary>
    public static class WebhookSignature
    {
        /// <summary>
        /// Signing schemes this SDK can verify. The signature header may carry several versions
        /// at once (<c>t=...,v1=...,v2=...</c>) so that a new scheme can be rolled out while
        /// receivers upgrade; verification uses the newest version present that appears in this
        /// list, and ignores the rest.
        /// </summary>
        public static readonly IReadOnlyList<int> SupportedVersions = new[] { 1 };

        /// <summary>
        /// How far a signature's timestamp may be from the current time before it is rejected.
        /// </summary>
        public static readonly TimeSpan MaxTimestampSkew = TimeSpan.FromMinutes(5);

        private static readonly Regex TimestampValueRegex = new Regex(@"^\d+$", RegexOptions.Compiled);
        private static readonly Regex SignatureKeyRegex = new Regex(@"^v(\d+)$", RegexOptions.Compiled);
        private static readonly Regex HexSignatureRegex = new Regex(@"^[a-f0-9]+$", RegexOptions.Compiled);

        /// <summary>
        /// Verify a webhook signature header against the raw request body. Returns normally when
        /// the signature is valid and otherwise throws a <see cref="WebhookSignatureException"/>
        /// whose <see cref="WebhookSignatureException.Error"/> says why it was rejected.
        /// </summary>
        /// <param name="signatureHeader">Value of the signature header sent with the webhook.</param>
        /// <param name="requestBody">Raw (unparsed) request body.</param>
        /// <param name="signingKey">Signing key for the webhook endpoint.</param>
        /// <exception cref="WebhookSignatureException">The signature was rejected.</exception>
        public static void Verify(string signatureHeader, string requestBody, string signingKey)
        {
            Verify(signatureHeader, Encoding.UTF8.GetBytes(requestBody ?? string.Empty), signingKey);
        }

        /// <summary>
        /// Verify a webhook signature header against the raw request body bytes.
        /// </summary>
        /// <inheritdoc cref="Verify(string, string, string)"/>
        public static void Verify(string signatureHeader, byte[] requestBody, string signingKey)
        {
            string timestamp;
            Dictionary<int, List<string>> signatures;
            ParseHeader(signatureHeader, out timestamp, out signatures);

            var version = NewestSupportedVersion(signatures);
            if (version == null)
            {
                var present = string.Join(", ", signatures.Keys.OrderBy(v => v).Select(v => "v" + v));
                throw new WebhookSignatureException(WebhookSignatureError.UnsupportedVersion,
                    "Unsupported webhook signature version: header carries only " + present);
            }

            // The header regex only admits digits, so the sole way this fails is overflow.
            long timestampSeconds;
            if (!long.TryParse(timestamp, out timestampSeconds))
            {
                throw new WebhookSignatureException(WebhookSignatureError.MalformedHeader,
                    "Malformed webhook signature header");
            }

            var skewSeconds = Math.Abs(DateTimeOffset.UtcNow.ToUnixTimeSeconds() - timestampSeconds);
            if (skewSeconds > (long)MaxTimestampSkew.TotalSeconds)
            {
                throw new WebhookSignatureException(WebhookSignatureError.TimestampSkew,
                    $"Webhook signature timestamp outside tolerance: off by {skewSeconds}s, " +
                    $"tolerance is {(long)MaxTimestampSkew.TotalSeconds}s");
            }

            var computed = SignatureForVersion(version.Value, requestBody, signingKey, timestamp);
            if (!signatures[version.Value].Any(signature => SecureEquals(computed, signature)))
            {
                throw new WebhookSignatureException(WebhookSignatureError.SignatureMismatch,
                    "Webhook signature mismatch");
            }
        }

        /// <summary>
        /// Verify a webhook signature header, returning <c>false</c> instead of throwing.
        /// </summary>
        /// <seealso cref="Verify(string, string, string)"/>
        public static bool IsValid(string signatureHeader, string requestBody, string signingKey)
        {
            return IsValid(signatureHeader, Encoding.UTF8.GetBytes(requestBody ?? string.Empty), signingKey);
        }

        /// <summary>
        /// Verify a webhook signature header against the raw request body bytes, returning
        /// <c>false</c> instead of throwing.
        /// </summary>
        /// <seealso cref="Verify(string, byte[], string)"/>
        public static bool IsValid(string signatureHeader, byte[] requestBody, string signingKey)
        {
            try
            {
                Verify(signatureHeader, requestBody, signingKey);
                return true;
            }
            catch (WebhookSignatureException)
            {
                return false;
            }
        }

        /// <summary>
        /// Compute the hex-encoded HMAC-SHA256 signature for a webhook payload, using the v1
        /// signing scheme.
        /// </summary>
        /// <param name="payload">Raw (unparsed) request body.</param>
        /// <param name="key">Signing key for the webhook endpoint.</param>
        /// <param name="timestamp">Unix timestamp in seconds, as sent in the signature header.</param>
        public static string Generate(string payload, string key, string timestamp)
        {
            return Generate(Encoding.UTF8.GetBytes(payload ?? string.Empty), key, timestamp);
        }

        /// <summary>
        /// Compute the hex-encoded HMAC-SHA256 signature for raw webhook payload bytes, using the
        /// v1 signing scheme.
        /// </summary>
        /// <inheritdoc cref="Generate(string, string, string)"/>
        public static string Generate(byte[] payload, string key, string timestamp)
        {
            var prefix = Encoding.UTF8.GetBytes(timestamp + ".");
            var message = new byte[prefix.Length + payload.Length];
            Buffer.BlockCopy(prefix, 0, message, 0, prefix.Length);
            Buffer.BlockCopy(payload, 0, message, prefix.Length, payload.Length);

            using (var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(key ?? string.Empty)))
            {
                return ToHex(hmac.ComputeHash(message));
            }
        }

        // Compute the signature for one signing scheme. This is the single place a new scheme
        // needs to be added.
        private static string SignatureForVersion(int version, byte[] payload, string key, string timestamp)
        {
            switch (version)
            {
                case 1:
                    return Generate(payload, key, timestamp);
                default:
                    throw new WebhookSignatureException(WebhookSignatureError.UnsupportedVersion,
                        "Unsupported webhook signature version: v" + version);
            }
        }

        // Split the header into its timestamp and its signatures keyed by version. Elements that
        // are not recognized are ignored, so that a sender adding new elements does not break
        // verification here.
        private static void ParseHeader(string signatureHeader, out string timestamp,
            out Dictionary<int, List<string>> signatures)
        {
            timestamp = null;
            signatures = new Dictionary<int, List<string>>();

            foreach (var element in (signatureHeader ?? string.Empty).Split(','))
            {
                var trimmed = element.Trim();
                var separator = trimmed.IndexOf('=');
                if (separator < 0)
                {
                    continue;
                }

                var key = trimmed.Substring(0, separator);
                var value = trimmed.Substring(separator + 1);

                if (key == "t")
                {
                    if (!TimestampValueRegex.IsMatch(value))
                    {
                        throw new WebhookSignatureException(WebhookSignatureError.MalformedHeader,
                            "Malformed webhook signature header");
                    }

                    timestamp = value;
                    continue;
                }

                var match = SignatureKeyRegex.Match(key);
                if (!match.Success)
                {
                    continue;
                }

                int version;
                if (!int.TryParse(match.Groups[1].Value, out version))
                {
                    continue;
                }

                // Only versions this SDK verifies have a signature format it can insist on;
                // anything else is recorded but left unchecked.
                if (SupportedVersions.Contains(version) && !HexSignatureRegex.IsMatch(value))
                {
                    throw new WebhookSignatureException(WebhookSignatureError.MalformedHeader,
                        "Malformed webhook signature header");
                }

                List<string> versionSignatures;
                if (!signatures.TryGetValue(version, out versionSignatures))
                {
                    versionSignatures = new List<string>();
                    signatures[version] = versionSignatures;
                }

                versionSignatures.Add(value);
            }

            if (timestamp == null || signatures.Count == 0)
            {
                throw new WebhookSignatureException(WebhookSignatureError.MalformedHeader,
                    "Malformed webhook signature header");
            }
        }

        // Pick the highest version present that this SDK can verify, so that once a sender emits
        // a newer scheme the older one stops being honored here.
        private static int? NewestSupportedVersion(Dictionary<int, List<string>> signatures)
        {
            var supported = signatures.Keys.Where(v => SupportedVersions.Contains(v)).ToList();
            return supported.Count == 0 ? (int?)null : supported.Max();
        }

        // Constant-time comparison; netstandard2.0 has no CryptographicOperations.FixedTimeEquals.
        private static bool SecureEquals(string computed, string given)
        {
            var left = Encoding.UTF8.GetBytes(computed);
            var right = Encoding.UTF8.GetBytes(given);
            if (left.Length != right.Length)
            {
                return false;
            }

            var difference = 0;
            for (var i = 0; i < left.Length; i++)
            {
                difference |= left[i] ^ right[i];
            }

            return difference == 0;
        }

        private static string ToHex(byte[] bytes)
        {
            var builder = new StringBuilder(bytes.Length * 2);
            foreach (var b in bytes)
            {
                builder.Append(b.ToString("x2"));
            }

            return builder.ToString();
        }
    }
}
