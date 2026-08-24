namespace HeloEmail.Sdk.Webhooks.Payloads
{
    /// <summary>
    /// Payload delivered when a return path domain verification fails.
    /// </summary>
    public class ReturnPathDomainVerificationFailedWebhookPayload : DomainWebhookPayload
    {
        public override WebhookEvent EventType => WebhookEvent.ReturnPathDomainVerificationFailed;
    }
}
