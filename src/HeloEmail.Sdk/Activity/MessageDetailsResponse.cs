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
        public MessageStatus Status { get; set; }
        public string Subject { get; set; }
        public MailAddress From { get; set; }
        public List<MailAddress> To { get; set; }
        public List<MailAddress> Cc { get; set; }
        public List<MailAddress> Bcc { get; set; }
        public List<MailAddress> ReplyTo { get; set; }
        public string Text { get; set; }
        public string Html { get; set; }
        public string Body { get; set; }
        public List<string> Tags { get; set; }
        public object Headers { get; set; }
        public object Metadata { get; set; }
        public List<string> Attachments { get; set; }
        public MessageTracking Tracking { get; set; }
        public List<MessageEvent> Events { get; set; }
    }

    public class MessageTracking
    {
        public bool Links { get; set; }
        public bool Opens { get; set; }
    }

    public class MessageEvent
    {
        public EventType EventType { get; set; }
        public DateTimeOffset Timestamp { get; set; }
        public string Recipient { get; set; }
        public object Details { get; set; }
    }
}
