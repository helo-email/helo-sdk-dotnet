using System.Collections.Generic;
using System.Threading.Tasks;

namespace HeloEmail.Sdk.Domains
{
    public interface IDomainsClient
    {
        /// <summary>
        /// List all domains
        /// </summary>
        Task<PaginatedResponseOfDomainResponse> List(
            int? limit = null,
            int? offset = null,
            string name = null,
            IEnumerable<string> channelIds = null);

        /// <summary>
        /// Create a domain
        /// </summary>
        Task<DomainWithDnsResponse> Create(CreateDomainRequest request);

        /// <summary>
        /// Retrieve a domain
        /// </summary>
        Task<DomainWithDnsResponse> Retrieve(string id);

        /// <summary>
        /// Update a domain
        /// </summary>
        Task<DomainResponse> Update(string id, UpdateDomainRequest request);

        /// <summary>
        /// Delete a domain
        /// </summary>
        Task Delete(string id);

        /// <summary>
        /// Verify a domain
        /// </summary>
        Task<DnsRecordsResponse> Verify(string id);

        /// <summary>
        /// Rotate a domain key
        /// </summary>
        Task<DnsRecordResponse> RotateKey(string id);
    }
}
