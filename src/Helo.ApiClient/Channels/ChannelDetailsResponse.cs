using System;

namespace Helo.ApiClient.Channels
{
    public class ChannelDetailsResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public DeliveryType DeliveryType { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
        public ChannelTracking Tracking { get; set; }
    }
}
