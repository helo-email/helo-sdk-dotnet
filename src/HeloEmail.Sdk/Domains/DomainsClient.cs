using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace HeloEmail.Sdk.Domains
{
    public class DomainsClient : BaseClient, IDomainsClient
    {
        public DomainsClient([FromKeyedServices(KeyedServices.HeloApiClientName)] HttpClient httpClient,
            ILogger<DomainsClient> logger) :
            base(httpClient, logger)
        {
        }

        /// <summary>
        /// List all domains
        /// </summary>
        public Task<PaginatedResponseOfDomainResponse> List(
            int? limit = null,
            int? offset = null,
            string name = null,
            IEnumerable<string> channelIds = null)
        {
            var query = new List<(string, string)>
            {
                ("limit", limit?.ToString(CultureInfo.InvariantCulture)),
                ("offset", offset?.ToString(CultureInfo.InvariantCulture)),
                ("name", name),
            };

            if (channelIds != null)
                query.AddRange(channelIds.Select(x => ("channelIds", x)));

            return Get<PaginatedResponseOfDomainResponse>(BuildUrl("/domains", query));
        }

        /// <summary>
        /// Create a domain
        /// </summary>
        public Task<DomainWithDnsResponse> Create(CreateDomainRequest request) =>
            Post<CreateDomainRequest, DomainWithDnsResponse>("/domains", request);

        /// <summary>
        /// Retrieve a domain
        /// </summary>
        public Task<DomainWithDnsResponse> Retrieve(string id) =>
            Get<DomainWithDnsResponse>($"/domains/{Uri.EscapeDataString(id)}");

        /// <summary>
        /// Update a domain
        /// </summary>
        public Task<DomainResponse> Update(string id, UpdateDomainRequest request) =>
            Patch<UpdateDomainRequest, DomainResponse>($"/domains/{Uri.EscapeDataString(id)}", request);

        /// <summary>
        /// Delete a domain
        /// </summary>
        public new Task Delete(string id) =>
            base.Delete($"/domains/{Uri.EscapeDataString(id)}");

        /// <summary>
        /// Verify a domain
        /// </summary>
        public Task<DnsRecordsResponse> Verify(string id) =>
            Post<DnsRecordsResponse>($"/domains/{Uri.EscapeDataString(id)}/verify");

        /// <summary>
        /// Rotate a domain key
        /// </summary>
        public Task<DnsRecordResponse> RotateKey(string id) =>
            Post<DnsRecordResponse>($"/domains/{Uri.EscapeDataString(id)}/rotate-key");
    }
}
