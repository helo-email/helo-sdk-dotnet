using System.Text.Json.Serialization;

namespace HeloEmail.Sdk.Suppressions
{
    [JsonConverter(typeof(ApiEnumConverter<SuppressionReason>))]
    public enum SuppressionReason
    {
        Bounce,
        Complaint,
        Unsubscribe,
        Manual,

        /// <summary>
        /// A value this version of the SDK doesn't know, which the API added after it was released.
        /// It can't be sent; upgrade the SDK to use the new value.
        /// </summary>
        // Last, so the members above keep their numbers and an unset property its default.
        [UnknownEnumValue]
        Unknown,
    }
}
