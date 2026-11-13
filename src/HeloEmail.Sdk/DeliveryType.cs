using System.Text.Json.Serialization;

namespace HeloEmail.Sdk
{
    /// <summary>
    /// Delivery mode for a channel. `live` sends real emails, whereas `sandbox`
    /// accepts and processes messages without delivering them to recipients.
    /// </summary>
    [JsonConverter(typeof(ApiEnumConverter<DeliveryType>))]
    public enum DeliveryType
    {
        Live,
        Sandbox,

        /// <summary>
        /// A value this version of the SDK doesn't know, which the API added after it was released.
        /// It can't be sent; upgrade the SDK to use the new value.
        /// </summary>
        // Last, so the members above keep their numbers and an unset property its default.
        [UnknownEnumValue]
        Unknown,
    }
}
