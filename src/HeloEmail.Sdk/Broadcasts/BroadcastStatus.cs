using System.Text.Json.Serialization;

namespace HeloEmail.Sdk.Broadcasts
{
    /// <summary>
    /// Current processing state of a broadcast. `accepted` — queued, waiting for the channel to become available. `processing` — actively sending messages. `completed` — all messages have been processed. `canceled` — stopped before completing.
    /// </summary>
    [JsonConverter(typeof(ApiEnumConverter<BroadcastStatus>))]
    public enum BroadcastStatus
    {
        Accepted,
        Processing,
        Completed,
        Canceled,

        /// <summary>
        /// A value this version of the SDK doesn't know, which the API added after it was released.
        /// It can't be sent; upgrade the SDK to use the new value.
        /// </summary>
        // Last, so the members above keep their numbers and an unset property its default.
        [UnknownEnumValue]
        Unknown,
    }
}
