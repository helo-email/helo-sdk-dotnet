namespace HeloEmail.Sdk.Webhooks.Payloads
{
    /// <summary>
    /// Fields common to every domain webhook payload.
    /// </summary>
    public abstract class DomainWebhookPayload : WebhookPayload
    {
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
    }
}
