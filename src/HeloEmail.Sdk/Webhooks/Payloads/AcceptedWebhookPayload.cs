using System.Collections.Generic;

namespace HeloEmail.Sdk.Webhooks.Payloads
{
    /// <summary>
    /// Payload delivered for the `accepted` event.
    /// </summary>
    public class AcceptedWebhookPayload : DeliveryWebhookPayload
    {
        public override WebhookEvent EventType => WebhookEvent.Accepted;

        /// <summary>
        /// The recipients the message was sent to.
        /// </summary>
        public List<string> Recipients { get; set; }
    }
}
