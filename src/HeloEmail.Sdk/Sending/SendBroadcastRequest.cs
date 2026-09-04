using System.Collections.Generic;

namespace HeloEmail.Sdk.Sending
{
    public class SendBroadcastRequest
    {
        public MailAddress From { get; set; }

        /// <summary>
        /// Reply-to addresses.
        /// </summary>
        public List<MailAddress> ReplyTo { get; set; }

        /// <summary>
        /// Email template applied to every message in the broadcast. At least one of `html` or `text` is required.
        /// </summary>
        public SendBroadcastRequestTemplate Template { get; set; }

        /// <summary>
        /// Override channel-level open and link tracking settings.
        /// </summary>
        public SendBroadcastRequestTracking Tracking { get; set; }

        /// <summary>
        /// File attachments included with every message in the broadcast. Executable and potentially harmful file types are blocked.
        /// </summary>
        public List<Attachment> Attachments { get; set; }

        /// <summary>
        /// Up to 5 tags for filtering and analytics. Allowed characters are letters, numbers, hyphens, and underscores. Max 100 characters per tag.
        /// </summary>
        public List<string> Tags { get; set; }

        /// <summary>
        /// Custom email headers applied to every message. Total size of all headers combined must not exceed 5,000 characters.
        /// </summary>
        public Dictionary<string, string> Headers { get; set; }

        /// <summary>
        /// Custom key/value metadata stored with the broadcast. Max 10 fields; keys max 50 characters, values max 100 characters.
        /// </summary>
        public Dictionary<string, string> Metadata { get; set; }

        /// <summary>
        /// One entry per outbound email. At least one message is required.
        /// </summary>
        public List<SendBroadcastRequestMessage> Messages { get; set; }
    }

    /// <summary>
    /// Email template applied to every message in the broadcast. At least one of `html` or `text` is required.
    /// </summary>
    public class SendBroadcastRequestTemplate
    {
        /// <summary>
        /// Subject line. Max 256 characters. Supports `{{variable}}` syntax.
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// HTML body. Supports `{{variable}}` syntax.
        /// </summary>
        public string Html { get; set; }

        /// <summary>
        /// Plain-text fallback body. Supports `{{variable}}` syntax.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// When true, CSS styles are inlined into the HTML before sending.
        /// </summary>
        public bool? InlineStyles { get; set; }

        /// <summary>
        /// Default template variables applied to every message. Message-level `data` values override these defaults.
        /// </summary>
        public object Data { get; set; }
    }

    /// <summary>
    /// Override channel-level open and link tracking settings.
    /// </summary>
    public class SendBroadcastRequestTracking
    {
        /// <summary>
        /// Track email opens. Defaults to the channel setting when not provided.
        /// </summary>
        public bool? Opens { get; set; }

        /// <summary>
        /// Track link clicks. Defaults to the channel setting when not provided.
        /// </summary>
        public bool? Links { get; set; }
    }

    public class SendBroadcastRequestMessage
    {
        /// <summary>
        /// Primary recipients. At least one required. Combined with `cc` and `bcc`, total recipients per message must not exceed 50.
        /// </summary>
        public List<MailAddress> To { get; set; }

        /// <summary>
        /// Carbon copy recipients.
        /// </summary>
        public List<MailAddress> Cc { get; set; }

        /// <summary>
        /// Blind carbon copy recipients.
        /// </summary>
        public List<MailAddress> Bcc { get; set; }

        /// <summary>
        /// Message-specific tags. Merged with broadcast-level tags.
        /// </summary>
        public List<string> Tags { get; set; }

        /// <summary>
        /// Message-specific headers. Merged with broadcast-level headers.
        /// </summary>
        public Dictionary<string, string> Headers { get; set; }

        /// <summary>
        /// Message-specific metadata. Merged with broadcast-level metadata.
        /// </summary>
        public Dictionary<string, string> Metadata { get; set; }

        /// <summary>
        /// Message-specific template variables. Merged with and overrides template-level `data`.
        /// </summary>
        public object Data { get; set; }
    }
}
