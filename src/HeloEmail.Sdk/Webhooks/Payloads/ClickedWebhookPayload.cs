namespace HeloEmail.Sdk.Webhooks.Payloads
{
    /// <summary>
    /// Payload delivered for the `clicked` event.
    /// </summary>
    public class ClickedWebhookPayload : RecipientWebhookPayload
    {
        public override WebhookEvent EventType => WebhookEvent.Clicked;

        /// <summary>
        /// Details specific to the `clicked` event.
        /// </summary>
        public ClickedDetails Details { get; set; }
    }
}
