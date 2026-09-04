using System.Collections.Generic;

namespace HeloEmail.Sdk.Broadcasts
{
    public class BroadcastContent
    {
        public MailAddress From { get; set; }

        /// <summary>
        /// Reply-to addresses.
        /// </summary>
        public List<MailAddress> ReplyTo { get; set; }
        public BroadcastContentTemplate Template { get; set; }

        /// <summary>
        /// File attachments included with the broadcast.
        /// </summary>
        public List<BroadcastContentAttachment> Attachments { get; set; }

        /// <summary>
        /// Tags associated with the broadcast.
        /// </summary>
        public List<string> Tags { get; set; }

        /// <summary>
        /// Custom email headers applied to every message.
        /// </summary>
        public Dictionary<string, string> Headers { get; set; }

        /// <summary>
        /// Custom key/value metadata stored with the broadcast.
        /// </summary>
        public Dictionary<string, string> Metadata { get; set; }
    }

    public class BroadcastContentTemplate
    {
        /// <summary>
        /// Subject line.
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// HTML body.
        /// </summary>
        public string Html { get; set; }

        /// <summary>
        /// Plain-text fallback body.
        /// </summary>
        public string Text { get; set; }
    }

    public class BroadcastContentAttachment
    {
        /// <summary>
        /// File name including extension.
        /// </summary>
        public string FileName { get; set; }
        public AttachmentDisposition Disposition { get; set; }

        /// <summary>
        /// File size in bytes.
        /// </summary>
        public int Size { get; set; }
    }
}
