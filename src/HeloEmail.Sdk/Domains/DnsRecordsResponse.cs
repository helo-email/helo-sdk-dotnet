using System.Collections.Generic;

namespace HeloEmail.Sdk.Domains
{
    public class DnsRecordsResponse
    {
        public DnsRecordResponse DomainKeyActive { get; set; }
        public DnsRecordResponse DomainKeyPending { get; set; }
        public List<DnsRecordResponse> ReturnPath { get; set; }
    }
}
