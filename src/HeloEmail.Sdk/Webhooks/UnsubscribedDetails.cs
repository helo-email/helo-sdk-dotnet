namespace HeloEmail.Sdk.Webhooks
{
    /// <summary>
    /// Details of an `unsubscribed` event.
    /// </summary>
    public class UnsubscribedDetails
    {
        /// <summary>
        /// IP address the unsubscribe was made from.
        /// </summary>
        public string Ip { get; set; }
    }
}
