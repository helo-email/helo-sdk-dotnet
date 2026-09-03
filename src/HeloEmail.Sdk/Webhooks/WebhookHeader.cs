namespace HeloEmail.Sdk.Webhooks
{
    /// <summary>
    /// A custom HTTP header to include in webhook deliveries.
    /// </summary>
    public class WebhookHeader
    {
        /// <summary>
        /// Header name, without the X-Customer- prefix.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Header value.
        /// </summary>
        public string Value { get; set; }
    }
}
