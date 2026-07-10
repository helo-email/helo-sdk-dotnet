using System.Collections.Generic;
using System.Threading.Tasks;

namespace HeloEmail.Sdk.Domains
{
    public interface IDomainsClient
    {
        Task<DomainWithDnsResponse> Create(CreateDomainRequest request);
        Task<PaginatedResponseOfDomainResponse> List(int? limit = null, int? offset = null, string name = null, IEnumerable<string> channelIds = null);
        Task<DomainWithDnsResponse> Retrieve(string id);
        Task<DomainResponse> Update(string id, UpdateDomainRequest request);
        Task Delete(string id);
        Task<DnsRecordsResponse> Verify(string id);
        Task<DnsRecordResponse> RotateKey(string id);
    }
}
