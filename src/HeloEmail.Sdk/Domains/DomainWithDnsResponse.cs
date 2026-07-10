using System;
using System.Collections.Generic;

namespace HeloEmail.Sdk.Domains
{
    public class DomainWithDnsResponse
    {
        public string Id { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public string Name { get; set; }
        public bool Verified { get; set; }
        public List<DomainChannelResponse> Channels { get; set; }
        public DnsRecordsResponse DnsRecords { get; set; }
    }
}
