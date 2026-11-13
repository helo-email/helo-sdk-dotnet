using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace HeloEmail.Sdk.Templates
{
    /// <summary>
    /// Only provided fields are changed - an omitted field leaves the existing value unchanged. Supplying both html and mjml in the same request is rejected, as is an update that would leave the template with no html or text content. Supply version to make the update conditional - without it, the last write wins.
    /// </summary>
    public class UpdateTemplateRequest : IHasNullFields
    {
        /// <summary>
        /// Renames the template. Cannot be cleared. Maximum 64 characters.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Cannot be cleared. Maximum 256 characters.
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// Replaces the html, and clears any stored mjml (switching the template back to plain HTML authoring). Set to null to remove the html - blank values are rejected. Maximum 500,000 characters.
        /// </summary>
        public string Html { get; set; }

        /// <summary>
        /// Replaces the mjml source, recompiling the html from it. Set to null to remove the mjml source while keeping the html already compiled from it - blank values are rejected. Maximum 500,000 characters, and the compiled html must also be within the html limit.
        /// </summary>
        public string Mjml { get; set; }

        /// <summary>
        /// Replaces the plain text content. Set to null to remove it - blank values are rejected. Maximum 100,000 characters.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Replaces the channels the template is scoped to. Set to null or an empty array to remove channel scoping, making the template available to every channel in the account.
        /// </summary>
        public List<string> ChannelIds { get; set; }

        /// <summary>
        /// The version of the template the update was based on, as returned by a previous read. If the template has since been modified the update is rejected with 409 Conflict, so concurrent edits aren't silently overwritten.
        /// </summary>
        public long? Version { get; set; }

        /// <summary>
        /// Properties to send as null, which clears them: <c>Html</c>, <c>Mjml</c>, <c>Text</c>, <c>ChannelIds</c>.
        /// Leaving a property null leaves the field as it is.
        /// </summary>
        [JsonIgnore]
        public List<string> NullFields { get; } = new List<string>();

        private static readonly IReadOnlyDictionary<string, string> ClearableFields = new Dictionary<string, string>
        {
            ["Html"] = "html",
            ["Mjml"] = "mjml",
            ["Text"] = "text",
            ["ChannelIds"] = "channelIds",
        };

        IReadOnlyDictionary<string, string> IHasNullFields.NullableFields => ClearableFields;
    }
}
