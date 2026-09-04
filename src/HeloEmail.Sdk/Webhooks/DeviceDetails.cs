namespace HeloEmail.Sdk.Webhooks
{
    /// <summary>
    /// The device the recipient engaged with the message from.
    /// </summary>
    public class DeviceDetails
    {
        public string Brand { get; set; }
        public string Family { get; set; }
        public string Model { get; set; }
    }
}
