using System.Collections.Generic;

namespace HeloEmail.Sdk.Domains
{
    public class CreateDomainRequest
    {
        public string Name { get; set; }
        public List<string> ChannelIds { get; set; }
    }
}
