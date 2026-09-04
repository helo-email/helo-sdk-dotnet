using System.Collections.Generic;

namespace HeloEmail.Sdk.Sending
{
    public class SendMessageRequest
    {
        public MailAddress From { get; set; }
        public List<MailAddress> To { get; set; }
        public List<MailAddress> Cc { get; set; }
        public List<MailAddress> Bcc { get; set; }
        public List<MailAddress> ReplyTo { get; set; }
        public string Subject { get; set; }
        public string Html { get; set; }
        public string Text { get; set; }
        public SendMessageRequestTemplate Template { get; set; }
        public SendMessageRequestTracking Tracking { get; set; }
        public List<Attachment> Attachments { get; set; }
        public List<string> Tags { get; set; }
        public Dictionary<string, string> Headers { get; set; }
        public Dictionary<string, string> Metadata { get; set; }
    }

    public class SendMessageRequestTemplate
    {
        public string Subject { get; set; }
        public string Html { get; set; }
        public string Text { get; set; }
        public bool? InlineStyles { get; set; }

        /// <summary>
        /// Model used for template placeholder replacement
        /// </summary>
        public object Data { get; set; }
    }

    public class SendMessageRequestTracking
    {
        public bool? Opens { get; set; }
        public bool? Links { get; set; }
    }
}
