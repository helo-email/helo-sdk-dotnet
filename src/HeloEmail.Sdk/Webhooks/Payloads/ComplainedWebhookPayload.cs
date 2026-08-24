namespace HeloEmail.Sdk.Webhooks.Payloads
{
    /// <summary>
    /// Payload delivered for the `complained` event.
    /// </summary>
    public class ComplainedWebhookPayload : RecipientWebhookPayload
    {
        public override WebhookEvent EventType => WebhookEvent.Complained;
    }
}
