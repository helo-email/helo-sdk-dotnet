namespace HeloEmail.Sdk.Webhooks
{
    /// <summary>
    /// Details of a `resubscribed` event.
    /// </summary>
    public class ResubscribedDetails
    {
        /// <summary>
        /// IP address the resubscribe was made from.
        /// </summary>
        public string Ip { get; set; }
    }
}
