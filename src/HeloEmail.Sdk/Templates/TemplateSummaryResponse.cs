using System;
using System.Collections.Generic;

namespace HeloEmail.Sdk.Templates
{
    public class TemplateSummaryResponse
    {
        public string Id { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
        public string Name { get; set; }
        public string Subject { get; set; }
        public List<string> ChannelIds { get; set; }
    }
}
