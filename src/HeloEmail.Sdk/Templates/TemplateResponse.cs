using System;
using System.Collections.Generic;

namespace HeloEmail.Sdk.Templates
{
    public class TemplateResponse
    {
        public string Id { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }

        /// <summary>
        /// Opaque version, changing on every update. Pass it back as the version of an update to have the update rejected if the template has been modified in the meantime.
        /// </summary>
        public long Version { get; set; }
        public string Name { get; set; }
        public string Subject { get; set; }
        public string Html { get; set; }
        public string Mjml { get; set; }
        public string Text { get; set; }
        public List<string> ChannelIds { get; set; }
    }
}
