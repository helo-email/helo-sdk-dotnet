using System.Text.Json.Serialization;

namespace HeloEmail.Sdk
{
    /// <summary>
    /// How the attachment is presented. Use `attachment` for downloadable files and `inline` for embedded content (e.g. images referenced in HTML).
    /// </summary>
    [JsonConverter(typeof(ApiEnumConverter<AttachmentDisposition>))]
    public enum AttachmentDisposition
    {
        Attachment,
        Inline,

        /// <summary>
        /// A value this version of the SDK doesn't know, which the API added after it was released.
        /// It can't be sent; upgrade the SDK to use the new value.
        /// </summary>
        // Last, so the members above keep their numbers and an unset property its default.
        [UnknownEnumValue]
        Unknown,
    }
}
