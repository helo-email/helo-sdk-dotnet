namespace HeloEmail.Sdk.Webhooks
{
    /// <summary>
    /// Email lifecycle event type that can trigger a webhook delivery.
    /// </summary>
    public enum WebhookEvent
    {
        Accepted,
        Processed,
        Bounced,
        Delivered,
        Opened,
        Clicked,
        Complained,
        Unsubscribed,
        Resubscribed,
        DomainKeyVerified,
        DomainKeyVerificationFailed,
        ReturnPathDomainVerified,
        ReturnPathDomainVerificationFailed,
    }
}
