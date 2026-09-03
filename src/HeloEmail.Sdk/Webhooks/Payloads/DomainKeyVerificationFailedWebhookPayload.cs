namespace HeloEmail.Sdk.Webhooks.Payloads
{
    /// <summary>
    /// Payload delivered when a domain key verification fails.
    /// </summary>
    public class DomainKeyVerificationFailedWebhookPayload : DomainWebhookPayload
    {
        public override WebhookEvent EventType => WebhookEvent.DomainKeyVerificationFailed;
    }
}
