using System.Collections.Generic;

namespace HeloEmail.Sdk.Suppressions
{
    public class PaginatedResponseOfSuppressionResponse
    {
        public int TotalCount { get; set; }
        public List<SuppressionResponse> Results { get; set; }
    }
}
