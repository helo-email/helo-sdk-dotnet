using System;
using System.Collections.Generic;

namespace HeloEmail.Sdk.Activity
{
    public class MessageDetailsResponse
    {
        public string MessageId { get; set; }
        public string ChannelId { get; set; }
        public DateTimeOffset Timestamp { get; set; }
        public MailType MailType { get; set; }
        public MailSource MailSource { get; set; }
        public DeliveryType DeliveryType { get; set; }
        public Status Status { get; set; }
        public string Subject { get; set; }
        public ActivityMailAddress From { get; set; }
        public List<ActivityMailAddress> To { get; set; }
        public List<ActivityMailAddress> Cc { get; set; }
        public List<ActivityMailAddress> Bcc { get; set; }
        public List<ActivityMailAddress> ReplyTo { get; set; }
        public string Text { get; set; }
        public string Html { get; set; }
        public string Body { get; set; }
        public List<string> Tags { get; set; }
        public Dictionary<string, string> Headers { get; set; }
        public Dictionary<string, string> Metadata { get; set; }
        public List<MessageDetailsResponseAttachment> Attachments { get; set; }
        public MessageDetailsResponseTracking Tracking { get; set; }
        public List<MessageDetailsResponseEvent> Events { get; set; }
        public MessageStatistics Statistics { get; set; }
    }

    public class MessageDetailsResponseAttachment
    {
        public string FileName { get; set; }
        public AttachmentDisposition Disposition { get; set; }
        public double Size { get; set; }
    }

    public class MessageDetailsResponseTracking
    {
        public bool Links { get; set; }
        public bool Opens { get; set; }
    }

    public class MessageDetailsResponseEvent
    {
        public EventType EventType { get; set; }
        public DateTimeOffset Timestamp { get; set; }
        public List<string> Recipients { get; set; }
        public object Details { get; set; }
    }
}
