using System.Collections.Generic;

namespace Helo.ApiClient.Sending
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
        public MessageTemplate Template { get; set; }
        public SendTracking Tracking { get; set; }
        public List<Attachment> Attachments { get; set; }
        public List<string> Tags { get; set; }
        public object Headers { get; set; }
        public object Metadata { get; set; }
    }
}
