namespace HeloEmail.Sdk.Webhooks
{
    /// <summary>
    /// Details of a `complained` event.
    /// </summary>
    public class ComplainedDetails
    {
        /// <summary>
        /// Complaint feedback type reported by the receiving server.
        /// </summary>
        public string Type { get; set; }
    }
}
