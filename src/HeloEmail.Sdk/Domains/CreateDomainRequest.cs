using System.Collections.Generic;

namespace HeloEmail.Sdk.Domains
{
    public class CreateDomainRequest
    {
        public string Name { get; set; }
        public List<string> ChannelIds { get; set; }

        /// <summary>
        /// Enable a custom tracking domain. When enabled, a tracking DNS record is added to the domain, and the tracking domain is used once that record is verified. Defaults to false.
        /// </summary>
        public bool? EnableCustomTracking { get; set; }
    }
}
