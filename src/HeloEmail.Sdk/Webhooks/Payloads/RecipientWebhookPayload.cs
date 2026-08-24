namespace HeloEmail.Sdk.Webhooks.Payloads
{
    /// <summary>
    /// Fields shared by per-recipient events (delivered, bounced, opened, clicked,
    /// complained, unsubscribed, resubscribed).
    /// </summary>
    public abstract class RecipientWebhookPayload : DeliveryWebhookPayload
    {
        /// <summary>
        /// The single recipient the event relates to.
        /// </summary>
        public string Recipient { get; set; }

        /// <summary>
        /// Unstructured, event-specific details. The shape depends on the event type.
        /// </summary>
        public object Details { get; set; }
    }
}
