namespace HeloEmail.Sdk.Webhooks.Payloads
{
    /// <summary>
    /// Fields shared by per-recipient events (delivered, bounced, opened, clicked,
    /// complained, unsubscribed, resubscribed). Each concrete payload declares its own
    /// <c>Details</c> property, typed to that event.
    /// </summary>
    public abstract class RecipientWebhookPayload : DeliveryWebhookPayload
    {
        /// <summary>
        /// The single recipient the event relates to.
        /// </summary>
        public string Recipient { get; set; }
    }
}
