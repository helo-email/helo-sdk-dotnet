using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace HeloEmail.Sdk.Webhooks
{
    /// <summary>
    /// Request body for updating a webhook. Only provided fields are changed.
    /// </summary>
    public class UpdateWebhookRequest : IHasNullFields
    {
        /// <summary>
        /// New delivery URL. Must be HTTPS.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Replaces the full list of event type subscriptions.
        /// </summary>
        public List<WebhookEvent> Events { get; set; }

        /// <summary>
        /// Set to a UUID to scope to a channel, or null to remove channel scoping.
        /// </summary>
        public string ChannelId { get; set; }

        /// <summary>
        /// Replaces all custom headers. Set to null to remove all custom headers.
        /// </summary>
        public List<WebhookHeader> AdditionalHeaders { get; set; }

        /// <summary>
        /// Enables or disables the webhook.
        /// </summary>
        public bool? Enabled { get; set; }

        /// <summary>
        /// Properties to send as null, which clears them: <c>ChannelId</c>, <c>AdditionalHeaders</c>.
        /// Leaving a property null leaves the field as it is.
        /// </summary>
        [JsonIgnore]
        public List<string> NullFields { get; } = new List<string>();

        private static readonly IReadOnlyDictionary<string, string> ClearableFields = new Dictionary<string, string>
        {
            ["ChannelId"] = "channelId",
            ["AdditionalHeaders"] = "additionalHeaders",
        };

        IReadOnlyDictionary<string, string> IHasNullFields.NullableFields => ClearableFields;
    }
}
