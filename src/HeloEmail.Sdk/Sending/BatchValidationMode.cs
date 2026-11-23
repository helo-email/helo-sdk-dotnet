using System.Text.Json.Serialization;

namespace HeloEmail.Sdk.Sending
{
    /// <summary>
    /// How the batch handles messages that fail validation. `strict` — any validation failure rejects the whole batch with a 422, and no messages are sent. `relaxed` — valid messages are sent, and each invalid message is returned as `failed`. Suppressed recipients and the send limit are not validation, so they fail only the affected message in either mode.
    /// </summary>
    [JsonConverter(typeof(ApiEnumConverter<BatchValidationMode>))]
    public enum BatchValidationMode
    {
        Strict,
        Relaxed,

        /// <summary>
        /// A value this version of the SDK doesn't know, which the API added after it was released.
        /// It can't be sent; upgrade the SDK to use the new value.
        /// </summary>
        // Last, so the members above keep their numbers and an unset property its default.
        [UnknownEnumValue]
        Unknown,
    }
}
