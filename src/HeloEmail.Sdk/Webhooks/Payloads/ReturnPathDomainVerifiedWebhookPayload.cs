namespace HeloEmail.Sdk.Webhooks.Payloads
{
    /// <summary>
    /// Payload delivered when a return path domain is verified.
    /// </summary>
    public class ReturnPathDomainVerifiedWebhookPayload : DomainWebhookPayload
    {
        public override WebhookEvent EventType => WebhookEvent.ReturnPathDomainVerified;
    }
}
