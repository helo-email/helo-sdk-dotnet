namespace HeloEmail.Sdk.Channels
{
    public class CreateChannelRequest
    {
        /// <summary>
        /// The display name for the channel.
        /// </summary>
        public string Name { get; set; }
        public DeliveryType DeliveryType { get; set; }
        public CreateChannelTracking Tracking { get; set; }
    }
}
