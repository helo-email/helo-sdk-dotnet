namespace HeloEmail.Sdk.Webhooks
{
    /// <summary>
    /// Details of a `delivered` event.
    /// </summary>
    public class DeliveredDetails
    {
        /// <summary>
        /// SMTP response returned by the receiving server.
        /// </summary>
        public string Response { get; set; }
    }
}
