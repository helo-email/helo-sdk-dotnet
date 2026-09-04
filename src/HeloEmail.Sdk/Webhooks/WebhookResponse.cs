using System.Collections.Generic;

namespace HeloEmail.Sdk.Webhooks
{
    /// <summary>
    /// Webhook configuration properties.
    /// </summary>
    public class WebhookResponse
    {
        /// <summary>
        /// Unique identifier for the webhook.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Channel this webhook is scoped to, or null for account-wide.
        /// </summary>
        public string ChannelId { get; set; }

        /// <summary>
        /// The HTTPS URL events are delivered to.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// HMAC-SHA256 signing key used to verify webhook authenticity. Sent as the X-Helo-Webhook-Signature request header on each delivery.
        /// </summary>
        public string PayloadSigningKey { get; set; }

        /// <summary>
        /// Whether the webhook is active and will receive events.
        /// </summary>
        public bool? Enabled { get; set; }

        /// <summary>
        /// Custom headers sent with each webhook delivery.
        /// </summary>
        public List<WebhookHeader> AdditionalHeaders { get; set; }

        /// <summary>
        /// Event types that trigger this webhook.
        /// </summary>
        public List<WebhookEvent> Events { get; set; }

        /// <summary>
        /// The most recent delivery outcome for this webhook, or null if no delivery has been attempted yet.
        /// </summary>
        public WebhookLastResponse LastResponse { get; set; }
    }
}
