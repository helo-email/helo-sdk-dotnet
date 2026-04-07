using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Helo.ApiClient.Domains
{
    public class HeloDomainsClient : HeloBaseClient, IHeloDomainsClient
    {
        public HeloDomainsClient([FromKeyedServices(KeyedServices.HeloApiClientName)] HttpClient httpClient,
            ILogger<HeloDomainsClient> logger) :
            base(httpClient, logger)
        {
        }

        public Task<DomainWithDnsResponse> Create(CreateDomainRequest request) =>
            Post<CreateDomainRequest, DomainWithDnsResponse>("/domains", request);

        public Task<PaginatedResponseOfDomainResponse> List(int? limit = null, int? offset = null,
            string name = null, IEnumerable<string> channelIds = null)
        {
            var query = new List<(string, string)>
            {
                ("limit", limit?.ToString()),
                ("offset", offset?.ToString()),
                ("name", name),
            };
            if (channelIds != null)
                query.AddRange(channelIds.Select(id => ("channelIds", id)));
            return Get<PaginatedResponseOfDomainResponse>(BuildUrl("/domains", query));
        }

        public Task<DomainWithDnsResponse> Retrieve(string id) =>
            Get<DomainWithDnsResponse>($"/domains/{Uri.EscapeDataString(id)}");

        public Task<DomainResponse> Update(string id, UpdateDomainRequest request) =>
            Patch<UpdateDomainRequest, DomainResponse>($"/domains/{Uri.EscapeDataString(id)}", request);

        public new Task Delete(string id) =>
            base.Delete($"/domains/{Uri.EscapeDataString(id)}");

        public Task<DnsRecordsResponse> Verify(string id) =>
            Post<DnsRecordsResponse>($"/domains/{Uri.EscapeDataString(id)}/verify");

        public Task<DnsRecordResponse> RotateKey(string id) =>
            Post<DnsRecordResponse>($"/domains/{Uri.EscapeDataString(id)}/rotate-key");

    }
}
