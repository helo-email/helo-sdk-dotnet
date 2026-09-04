namespace HeloEmail.Sdk.Channels
{
    public class UpdateChannelRequest
    {
        /// <summary>
        /// The new display name for the channel. Omit to keep the current value.
        /// </summary>
        public string Name { get; set; }
        public DeliveryType? DeliveryType { get; set; }
        public UpdateChannelTracking Tracking { get; set; }
    }
}
