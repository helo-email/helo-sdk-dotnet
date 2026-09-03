namespace HeloEmail.Sdk
{
    /// <summary>
    /// Delivery mode for a channel. `live` sends real emails, whereas `sandbox`
    /// accepts and processes messages without delivering them to recipients.
    /// </summary>
    public enum DeliveryType
    {
        Live,
        Sandbox,
    }
}
