using System.Collections.Generic;

namespace Helo.ApiClient.Domains
{
    public class PaginatedResponseOfDomainResponse
    {
        public int TotalCount { get; set; }
        public List<DomainResponse> Results { get; set; }
    }
}
