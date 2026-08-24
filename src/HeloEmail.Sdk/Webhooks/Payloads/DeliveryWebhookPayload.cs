using System.Collections.Generic;
using HeloEmail.Sdk.Activity;

namespace HeloEmail.Sdk.Webhooks.Payloads
{
    /// <summary>
    /// Fields common to every delivery webhook payload.
    /// </summary>
    public abstract class DeliveryWebhookPayload : WebhookPayload
    {
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
    }
}
