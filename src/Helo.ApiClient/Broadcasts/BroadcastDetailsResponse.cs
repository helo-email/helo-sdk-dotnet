using System;
using System.Collections.Generic;

namespace Helo.ApiClient.Broadcasts
{
    public class BroadcastDetailsResponse
    {
        public string Id { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public BroadcastStatus Status { get; set; }
        public string Subject { get; set; }
        public string Completion { get; set; }
        public int Messages { get; set; }
        public int Failed { get; set; }
        public int Suppressed { get; set; }
        public BroadcastContent Content { get; set; }
        public BroadcastTracking Tracking { get; set; }
        public BroadcastStatistics Statistics { get; set; }
    }

    public class BroadcastContent
    {
        public MailAddress From { get; set; }
        public List<MailAddress> ReplyTo { get; set; }
        public BroadcastContentTemplate Template { get; set; }
        public List<BroadcastContentAttachment> Attachments { get; set; }
        public List<string> Tags { get; set; }
        public object Headers { get; set; }
        public object Metadata { get; set; }
    }

    public class BroadcastContentTemplate
    {
        public string Subject { get; set; }
        public string Html { get; set; }
        public string Text { get; set; }
    }

    public class BroadcastContentAttachment
    {
        public string FileName { get; set; }
        public AttachmentDisposition Disposition { get; set; }
        public int Size { get; set; }
    }

    public class BroadcastTracking
    {
        public bool Opens { get; set; }
        public bool Links { get; set; }
    }

    public class BroadcastStatistics
    {
        public int Sent { get; set; }
        public int Delivered { get; set; }
        public int Bounced { get; set; }
        public int Opened { get; set; }
        public int Clicked { get; set; }
        public int Complained { get; set; }
        public int Unsubscribed { get; set; }
    }
}
