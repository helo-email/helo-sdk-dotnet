using System;

namespace HeloEmail.Sdk.Webhooks
{
    /// <summary>
    /// Payload delivered when a return path domain is verified.
    /// </summary>
    public class ReturnPathDomainVerifiedPayload
    {
        public WebhookEvent EventType { get; set; }

        /// <summary>
        /// Unique identifier for the domain.
        /// </summary>
        public string DomainId { get; set; }

        /// <summary>
        /// The name of the domain.
        /// </summary>
        public string DomainName { get; set; }

        /// <summary>
        /// The host for the DNS record.
        /// </summary>
        public string DnsRecordHost { get; set; }

        /// <summary>
        /// When the event occurred (ISO 8601 / RFC 3339).
        /// </summary>
        public DateTimeOffset Timestamp { get; set; }
    }
}
