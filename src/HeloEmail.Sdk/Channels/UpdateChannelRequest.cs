namespace HeloEmail.Sdk.Channels
{
    public class UpdateChannelRequest
    {
        public string Name { get; set; }
        public DeliveryType? DeliveryType { get; set; }
        public ChannelTracking Tracking { get; set; }
    }
}
