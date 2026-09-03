using System;

namespace HeloEmail.Sdk.Channels
{
    public class ChannelDetailsResponse
    {
        /// <summary>
        /// The unique channel ID.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The display name of the channel.
        /// </summary>
        public string Name { get; set; }
        public DeliveryType? DeliveryType { get; set; }

        /// <summary>
        /// When the channel was created.
        /// </summary>
        public DateTimeOffset? CreatedAt { get; set; }

        /// <summary>
        /// When the channel was last updated.
        /// </summary>
        public DateTimeOffset? UpdatedAt { get; set; }
        public ChannelTracking Tracking { get; set; }
    }
}
