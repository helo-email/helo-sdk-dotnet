namespace HeloEmail.Sdk.Webhooks
{
    /// <summary>
    /// Details of an `email-bounced` event.
    /// </summary>
    public class BouncedDetails
    {
        /// <summary>
        /// Bounce classification, e.g. `Permanent` or `Transient`.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// More specific bounce classification, e.g. `General` or `Suppressed`.
        /// </summary>
        public string SubType { get; set; }

        /// <summary>
        /// Diagnostic code returned by the receiving server.
        /// </summary>
        public string Code { get; set; }
    }
}
