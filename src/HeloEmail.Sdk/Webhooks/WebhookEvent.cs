namespace HeloEmail.Sdk.Webhooks
{
    /// <summary>
    /// Email lifecycle event type that can trigger a webhook delivery.
    /// </summary>
    public enum WebhookEvent
    {
        MessageAccepted,
        MessageProcessed,
        EmailDelivered,
        EmailBounced,
        EmailOpened,
        LinkClicked,
        RecipientComplained,
        RecipientUnsubscribed,
        RecipientResubscribed,
        DomainKeyVerified,
        DomainKeyVerificationFailed,
        ReturnPathDomainVerified,
        ReturnPathDomainVerificationFailed,
    }
}
