using System;

namespace HeloEmail.Sdk.Webhooks
{
    /// <summary>
    /// The most recent delivery outcome recorded for a webhook.
    /// </summary>
    public class WebhookLastResponse
    {
        /// <summary>
        /// HTTP status code returned by the endpoint, or null when no HTTP response was received (network error, timeout, or blocked host).
        /// </summary>
        public int? StatusCode { get; set; }

        /// <summary>
        /// Reason a delivery failed without an HTTP response (e.g. a connection error or BlockedHost). Null when an HTTP response was received.
        /// </summary>
        public string Error { get; set; }

        /// <summary>
        /// Timestamp of the most recent delivery attempt.
        /// </summary>
        public DateTimeOffset? At { get; set; }
    }
}
