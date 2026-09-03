using System.Collections.Generic;

namespace HeloEmail.Sdk.Webhooks
{
    /// <summary>
    /// Request body for updating a webhook. Only provided fields are changed.
    /// </summary>
    public class UpdateWebhookRequest
    {
        /// <summary>
        /// New delivery URL. Must be HTTPS.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Replaces the full list of event type subscriptions.
        /// </summary>
        public List<WebhookEvent> Events { get; set; }

        /// <summary>
        /// Set to a UUID to scope to a channel, or null to remove channel scoping.
        /// </summary>
        public string ChannelId { get; set; }

        /// <summary>
        /// Replaces all custom headers. Set to null to remove all custom headers.
        /// </summary>
        public List<WebhookHeader> AdditionalHeaders { get; set; }

        /// <summary>
        /// Enables or disables the webhook.
        /// </summary>
        public bool? Enabled { get; set; }
    }
}
