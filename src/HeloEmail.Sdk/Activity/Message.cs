using System;
using System.Collections.Generic;

namespace HeloEmail.Sdk.Activity
{
    public class Message
    {
        public string MessageId { get; set; }
        public string ChannelId { get; set; }
        public DateTimeOffset Timestamp { get; set; }
        public MailType MailType { get; set; }
        public MailSource MailSource { get; set; }
        public DeliveryType DeliveryType { get; set; }
        public MessageStatus Status { get; set; }
        public string Subject { get; set; }
        public List<string> Recipients { get; set; }
    }
}
