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

        /// <summary>
        /// Deserializes a webhook payload into the class for its event type. Returns null when the
        /// payload has no event type, or one this version of the SDK doesn't know (an event the API
        /// added later), so a receiver can acknowledge events it doesn't handle.
        /// </summary>
        public static object Parse(string webhookPayload)
        {
            var jsonNode = JsonNode.Parse(webhookPayload);
            var eventTypeStr = jsonNode?["eventType"]?.GetValue<string>();
            if (eventTypeStr == null)
            {
                return null;
            }

            // ApiEnumConverter reads an event type the SDK doesn't know as its unknown member.
            var eventParsed = jsonNode["eventType"].Deserialize<WebhookEvent>();

            return PayloadTypes.TryGetValue(eventParsed, out var payloadType)
                ? jsonNode.Deserialize(payloadType)
                : null;
        }
    }
}
