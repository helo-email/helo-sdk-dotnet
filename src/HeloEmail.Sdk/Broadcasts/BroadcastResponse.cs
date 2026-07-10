using System;

namespace HeloEmail.Sdk.Broadcasts
{
    public class BroadcastResponse
    {
        public string Id { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public BroadcastStatus Status { get; set; }
        public string Subject { get; set; }
        public string Completion { get; set; }
        public int Messages { get; set; }
    }
}
