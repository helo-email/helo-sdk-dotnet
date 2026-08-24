namespace HeloEmail.Sdk.Webhooks
{
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
