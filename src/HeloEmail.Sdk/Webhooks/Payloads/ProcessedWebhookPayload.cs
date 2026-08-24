using System.Collections.Generic;

namespace HeloEmail.Sdk.Webhooks.Payloads
{
    /// <summary>
    /// Payload delivered for the `processed` event.
    /// </summary>
    public class ProcessedWebhookPayload : DeliveryWebhookPayload
    {
        public override WebhookEvent EventType => WebhookEvent.Processed;

        /// <summary>
        /// The recipients the message was sent to.
        /// </summary>
        public List<string> Recipients { get; set; }
    }
}
