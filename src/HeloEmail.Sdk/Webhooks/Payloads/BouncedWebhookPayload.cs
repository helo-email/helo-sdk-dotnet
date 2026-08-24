namespace HeloEmail.Sdk.Webhooks.Payloads
{
    /// <summary>
    /// Payload delivered for the `bounced` event.
    /// </summary>
    public class BouncedWebhookPayload : RecipientWebhookPayload
    {
        public override WebhookEvent EventType => WebhookEvent.Bounced;
    }
}
