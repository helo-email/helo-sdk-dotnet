using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace HeloEmail.Sdk.Webhooks
{
    public class WebhookParser
    {
        private static readonly Dictionary<WebhookEvent, Type> PayloadTypes = new Dictionary<WebhookEvent, Type>
        {
            [WebhookEvent.MessageAccepted] = typeof(MessageAcceptedWebhookPayload),
            [WebhookEvent.MessageProcessed] = typeof(MessageProcessedWebhookPayload),
            [WebhookEvent.EmailDelivered] = typeof(EmailDeliveredWebhookPayload),
            [WebhookEvent.EmailBounced] = typeof(EmailBouncedWebhookPayload),
            [WebhookEvent.EmailOpened] = typeof(EmailOpenedWebhookPayload),
            [WebhookEvent.LinkClicked] = typeof(LinkClickedWebhookPayload),
            [WebhookEvent.RecipientComplained] = typeof(RecipientComplainedWebhookPayload),
            [WebhookEvent.RecipientUnsubscribed] = typeof(RecipientUnsubscribedWebhookPayload),
            [WebhookEvent.RecipientResubscribed] = typeof(RecipientResubscribedWebhookPayload),
            [WebhookEvent.DomainKeyVerified] = typeof(DomainKeyVerifiedPayload),
            [WebhookEvent.DomainKeyVerificationFailed] = typeof(DomainKeyVerificationFailedPayload),
            [WebhookEvent.ReturnPathDomainVerified] = typeof(ReturnPathDomainVerifiedPayload),
            [WebhookEvent.ReturnPathDomainVerificationFailed] = typeof(ReturnPathDomainVerificationFailedPayload),
        };

        public static object Parse(string webhookPayload)
        {
            var jsonNode = JsonNode.Parse(webhookPayload);
            var eventTypeStr = jsonNode?["eventType"]?.GetValue<string>();
            if (eventTypeStr == null)
            {
                return null;
            }

            var eventParsed = ParseKebabCase(eventTypeStr);

            return !PayloadTypes.TryGetValue(eventParsed, out var payloadType)
                ? throw new ArgumentOutOfRangeException(nameof(webhookPayload), eventParsed,
                    "Unrecognized webhook event type.")
                : jsonNode.Deserialize(payloadType);
        }

        private static WebhookEvent ParseKebabCase(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new JsonException($"Cannot convert null/empty string to {nameof(WebhookEvent)}.");
            }

            var pascalCase = string.Concat(value.Split('-').Select(CapitalizeFirst));
            return !Enum.TryParse(pascalCase, ignoreCase: true, out WebhookEvent result)
                ? throw new JsonException($"Unable to parse \"{value}\" as {nameof(WebhookEvent)}.")
                : result;
        }

        private static string CapitalizeFirst(string s)
            => string.IsNullOrEmpty(s) ? s : char.ToUpperInvariant(s[0]) + s.Substring(1);
    }
}
