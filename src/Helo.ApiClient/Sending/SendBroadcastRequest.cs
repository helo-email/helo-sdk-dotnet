using System.Collections.Generic;

namespace Helo.ApiClient.Sending
{
    public class SendBroadcastRequest
    {
        public MailAddress From { get; set; }
        public List<MailAddress> ReplyTo { get; set; }
        public MessageTemplate Template { get; set; }
        public SendTracking Tracking { get; set; }
        public List<Attachment> Attachments { get; set; }
        public List<string> Tags { get; set; }
        public object Headers { get; set; }
        public object Metadata { get; set; }
        public List<BroadcastMessage> Messages { get; set; }
    }

    public class BroadcastMessage
    {
        public List<MailAddress> To { get; set; }
        public List<MailAddress> Cc { get; set; }
        public List<MailAddress> Bcc { get; set; }
        public List<string> Tags { get; set; }
        public object Headers { get; set; }
        public object Metadata { get; set; }
        public object Data { get; set; }
    }
}
