using System;
using System.Collections.Generic;

namespace Helo.ApiClient.Activity
{
    public class ActivityEvent
    {
        public string MessageId { get; set; }
        public string ChannelId { get; set; }
        public MailType MailType { get; set; }
        public MailSource? MailSource { get; set; }
        public EventType EventType { get; set; }
        public DateTimeOffset Timestamp { get; set; }
        public string Subject { get; set; }
        public List<string> Recipients { get; set; }
        public List<string> Tags { get; set; }
        public object Metadata { get; set; }
        public object Details { get; set; }
    }
}
