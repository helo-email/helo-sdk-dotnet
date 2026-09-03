namespace HeloEmail.Sdk.Webhooks.Payloads
{
    /// <summary>
    /// Payload delivered for the `unsubscribed` event.
    /// </summary>
    public class UnsubscribedWebhookPayload : RecipientWebhookPayload
    {
        public override WebhookEvent EventType => WebhookEvent.Unsubscribed;

        /// <summary>
        /// Details specific to the `unsubscribed` event.
        /// </summary>
        public UnsubscribedDetails Details { get; set; }
    }
}
