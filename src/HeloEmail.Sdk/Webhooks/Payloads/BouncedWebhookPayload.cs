namespace HeloEmail.Sdk.Webhooks.Payloads
{
    /// <summary>
    /// Payload delivered for the `bounced` event.
    /// </summary>
    public class BouncedWebhookPayload : RecipientWebhookPayload
    {
        public override WebhookEvent EventType => WebhookEvent.Bounced;

        /// <summary>
        /// Details specific to the `bounced` event.
        /// </summary>
        public BouncedDetails Details { get; set; }
    }
}
