using System;

namespace HeloEmail.Sdk.Channels
{
    public class ChannelBasicResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public DeliveryType DeliveryType { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
    }
}
