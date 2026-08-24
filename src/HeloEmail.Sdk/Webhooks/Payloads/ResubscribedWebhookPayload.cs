namespace HeloEmail.Sdk.Webhooks.Payloads
{
    /// <summary>
    /// Payload delivered for the `resubscribed` event.
    /// </summary>
    public class ResubscribedWebhookPayload : RecipientWebhookPayload
    {
        public override WebhookEvent EventType => WebhookEvent.Resubscribed;
    }
}
