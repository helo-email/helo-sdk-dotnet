using System;
using System.Collections.Generic;

namespace HeloEmail.Sdk.Webhooks
{
    /// <summary>
    /// Payload delivered for the `recipient-complained` event.
    /// </summary>
    public class RecipientComplainedWebhookPayload
    {
        public WebhookEvent EventType { get; set; }
        public ComplainedDetails Details { get; set; }

        /// <summary>
        /// The single recipient the event relates to.
        /// </summary>
        public string Recipient { get; set; }

        /// <summary>
        /// Unique identifier of the message the event relates to.
        /// </summary>
        public string MessageId { get; set; }

        /// <summary>
        /// Unique identifier of the channel the message was sent from.
        /// </summary>
        public string ChannelId { get; set; }
        public MailType MailType { get; set; }

        /// <summary>
        /// Subject line of the message.
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// Tags associated with the message.
        /// </summary>
        public List<string> Tags { get; set; }

        /// <summary>
        /// Arbitrary key/value metadata associated with the message.
        /// </summary>
        public Dictionary<string, string> Metadata { get; set; }

        /// <summary>
        /// When the event occurred (ISO 8601 / RFC 3339).
        /// </summary>
        public DateTimeOffset Timestamp { get; set; }
    }
}
