using System.Collections.Generic;

namespace Helo.ApiClient.Domains
{
    public class CreateDomainRequest
    {
        public string Name { get; set; }
        public List<string> ChannelIds { get; set; }
    }
}
