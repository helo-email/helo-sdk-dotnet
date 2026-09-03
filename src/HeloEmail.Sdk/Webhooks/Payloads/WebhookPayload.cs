using System;

namespace HeloEmail.Sdk.Webhooks.Payloads
{
    /// <summary>
    /// Fields common to every webhook payload.
    /// </summary>
    public abstract class WebhookPayload
    {
        /// <summary>
        /// The event type that triggered the webhook delivery.
        /// </summary>
        public abstract WebhookEvent EventType { get; }

        /// <summary>
        /// When the event occurred.
        /// </summary>
        public DateTimeOffset Timestamp { get; set; }
    }
}
