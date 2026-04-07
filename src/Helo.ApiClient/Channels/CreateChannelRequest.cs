namespace Helo.ApiClient.Channels
{
    public class CreateChannelRequest
    {
        public string Name { get; set; }
        public DeliveryType DeliveryType { get; set; }
        public ChannelTracking Tracking { get; set; }
    }
}
