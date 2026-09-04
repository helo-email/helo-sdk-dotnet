namespace HeloEmail.Sdk.Webhooks
{
    /// <summary>
    /// Details of a `clicked` event.
    /// </summary>
    public class ClickedDetails
    {
        /// <summary>
        /// The URL that was clicked.
        /// </summary>
        public string Link { get; set; }

        /// <summary>
        /// IP address the recipient engaged from.
        /// </summary>
        public string Ip { get; set; }

        /// <summary>
        /// Country the recipient engaged from.
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// ISO 3166-1 alpha-2 country code the recipient engaged from.
        /// </summary>
        public string CountryCode { get; set; }
        public ClientDetails Client { get; set; }
        public DeviceDetails Device { get; set; }
    }
}
