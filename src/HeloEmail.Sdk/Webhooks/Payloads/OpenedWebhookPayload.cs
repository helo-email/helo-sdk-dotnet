namespace HeloEmail.Sdk.Webhooks.Payloads
{
    /// <summary>
    /// Payload delivered for the `opened` event.
    /// </summary>
    public class OpenedWebhookPayload : RecipientWebhookPayload
    {
        public override WebhookEvent EventType => WebhookEvent.Opened;
    }
}
