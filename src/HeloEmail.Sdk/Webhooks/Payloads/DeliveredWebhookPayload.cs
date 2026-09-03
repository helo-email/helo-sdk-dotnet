namespace HeloEmail.Sdk.Webhooks.Payloads
{
    /// <summary>
    /// Payload delivered for the `delivered` event.
    /// </summary>
    public class DeliveredWebhookPayload : RecipientWebhookPayload
    {
        public override WebhookEvent EventType => WebhookEvent.Delivered;

        /// <summary>
        /// Details specific to the `delivered` event.
        /// </summary>
        public DeliveredDetails Details { get; set; }
    }
}
