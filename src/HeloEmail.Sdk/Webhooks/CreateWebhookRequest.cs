using System.Collections.Generic;

namespace HeloEmail.Sdk.Webhooks
{
    /// <summary>
    /// Request body for creating a new webhook.
    /// </summary>
    public class CreateWebhookRequest
    {
        /// <summary>
        /// The HTTPS URL to deliver webhook events to.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// List of event types that will trigger this webhook.
        /// </summary>
        public List<WebhookEvent> Events { get; set; }

        /// <summary>
        /// Optional channel scope. If set, only events for this channel trigger the webhook.
        /// </summary>
        public string ChannelId { get; set; }

        /// <summary>
        /// Custom headers to include in each webhook request. Each header name is prefixed with X-Customer-.
        /// </summary>
        public List<WebhookHeader> AdditionalHeaders { get; set; }

        /// <summary>
        /// Whether the webhook is active or not. Defaults to true.
        /// </summary>
        public bool? Enabled { get; set; }
    }
}
