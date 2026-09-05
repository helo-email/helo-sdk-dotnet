using HeloEmail.Sdk.Webhooks;

namespace HeloEmail.Sdk.Tests.Webhooks;

public class WebhookParserTests
{
    [Theory]
    [InlineData("message-accepted", typeof(MessageAcceptedWebhookPayload))]
    [InlineData("message-processed", typeof(MessageProcessedWebhookPayload))]
    [InlineData("email-delivered", typeof(EmailDeliveredWebhookPayload))]
    [InlineData("email-bounced", typeof(EmailBouncedWebhookPayload))]
    [InlineData("email-opened", typeof(EmailOpenedWebhookPayload))]
    [InlineData("link-clicked", typeof(LinkClickedWebhookPayload))]
    [InlineData("recipient-complained", typeof(RecipientComplainedWebhookPayload))]
    [InlineData("recipient-unsubscribed", typeof(RecipientUnsubscribedWebhookPayload))]
    [InlineData("recipient-resubscribed", typeof(RecipientResubscribedWebhookPayload))]
    [InlineData("domain-key-verified", typeof(DomainKeyVerifiedPayload))]
    [InlineData("domain-key-verification-failed", typeof(DomainKeyVerificationFailedPayload))]
    [InlineData("return-path-domain-verified", typeof(ReturnPathDomainVerifiedPayload))]
    [InlineData("return-path-domain-verification-failed", typeof(ReturnPathDomainVerificationFailedPayload))]
    public void WebhookParser_Parse_DeserializesPayloadForEvent(string eventType, Type webhookType)
    {
        var json =
            $$"""
              {
                "eventType": "{{eventType}}"
              }
              """;

        var parsed = WebhookParser.Parse(json);

        Assert.IsType(webhookType, parsed);
    }
}
