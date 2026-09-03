namespace HeloEmail.Sdk.Webhooks.Payloads
{
    /// <summary>
    /// Details of the user agent a recipient engaged with the message from.
    /// </summary>
    public class ClientDetails
    {
        public string Family { get; set; }
        public string Version { get; set; }
    }

    /// <summary>
    /// Details of the device a recipient engaged with the message from.
    /// </summary>
    public class DeviceDetails
    {
        public string Brand { get; set; }
        public string Family { get; set; }
        public string Model { get; set; }
    }

    /// <summary>
    /// Details of a `delivered` event.
    /// </summary>
    public class DeliveredDetails
    {
        /// <summary>
        /// The SMTP response returned by the receiving server.
        /// </summary>
        public string Response { get; set; }
    }

    /// <summary>
    /// Details of a `bounced` event.
    /// </summary>
    public class BouncedDetails
    {
        public string Type { get; set; }
        public string SubType { get; set; }

        /// <summary>
        /// The diagnostic code returned by the receiving server.
        /// </summary>
        public string Code { get; set; }
    }

    /// <summary>
    /// Details of an `opened` event.
    /// </summary>
    public class OpenedDetails
    {
        public string Ip { get; set; }
        public string Country { get; set; }
        public string CountryCode { get; set; }
        public ClientDetails Client { get; set; }
        public DeviceDetails Device { get; set; }
    }

    /// <summary>
    /// Details of a `clicked` event.
    /// </summary>
    public class ClickedDetails
    {
        /// <summary>
        /// The URL that was clicked.
        /// </summary>
        public string Link { get; set; }

        public string Ip { get; set; }
        public string Country { get; set; }
        public string CountryCode { get; set; }
        public ClientDetails Client { get; set; }
        public DeviceDetails Device { get; set; }
    }

    /// <summary>
    /// Details of a `complained` event.
    /// </summary>
    public class ComplainedDetails
    {
        /// <summary>
        /// The complaint feedback type reported by the receiving server.
        /// </summary>
        public string Type { get; set; }
    }

    /// <summary>
    /// Details of an `unsubscribed` event.
    /// </summary>
    public class UnsubscribedDetails
    {
        public string Ip { get; set; }
    }

    /// <summary>
    /// Details of a `resubscribed` event.
    /// </summary>
    public class ResubscribedDetails
    {
        public string Ip { get; set; }
    }
}
