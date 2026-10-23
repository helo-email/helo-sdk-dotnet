using System.Collections.Generic;

namespace HeloEmail.Sdk.Domains
{
    public class UpdateDomainRequest
    {
        public List<string> ChannelIds { get; set; }

        /// <summary>
        /// Enable or disable a custom tracking domain. When enabled, a tracking DNS record is added to the domain, and the tracking domain is used once that record is verified. When disabled, the tracking DNS record is removed. Custom tracking cannot be disabled once the tracking domain is in use, as links in sent emails depend on it.
        /// </summary>
        public bool? EnableCustomTracking { get; set; }
    }
}
