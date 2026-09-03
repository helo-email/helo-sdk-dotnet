namespace HeloEmail.Sdk.Webhooks.Payloads
{
    /// <summary>
    /// Payload delivered when a domain key is verified.
    /// </summary>
    public class DomainKeyVerifiedWebhookPayload : DomainWebhookPayload
    {
        public override WebhookEvent EventType => WebhookEvent.DomainKeyVerified;
    }
}
