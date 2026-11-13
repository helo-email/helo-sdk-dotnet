using System.Text.Json.Serialization;

namespace HeloEmail.Sdk.Webhooks
{
    /// <summary>
    /// Email lifecycle event type that can trigger a webhook delivery.
    /// </summary>
    [JsonConverter(typeof(ApiEnumConverter<WebhookEvent>))]
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

        /// <summary>
        /// A value this version of the SDK doesn't know, which the API added after it was released.
        /// It can't be sent; upgrade the SDK to use the new value.
        /// </summary>
        // Last, so the members above keep their numbers and an unset property its default.
        [UnknownEnumValue]
        Unknown,
    }
}
