using System.Collections.Generic;

namespace Helo.ApiClient.Domains
{
    public class DnsRecordsResponse
    {
        public DnsRecordResponse DomainKeyActive { get; set; }
        public DnsRecordResponse DomainKeyPending { get; set; }
        public List<DnsRecordResponse> ReturnPath { get; set; }
        public DnsRecordResponse Tracking { get; set; }
        public DnsRecordResponse Unsubscribe { get; set; }
    }
}
