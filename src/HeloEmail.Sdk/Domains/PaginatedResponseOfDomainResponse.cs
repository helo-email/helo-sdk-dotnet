using System.Collections.Generic;

namespace HeloEmail.Sdk.Domains
{
    public class PaginatedResponseOfDomainResponse
    {
        public int TotalCount { get; set; }
        public List<DomainResponse> Results { get; set; }
    }
}
