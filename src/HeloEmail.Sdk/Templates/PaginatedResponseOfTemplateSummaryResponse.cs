using System.Collections.Generic;

namespace HeloEmail.Sdk.Templates
{
    public class PaginatedResponseOfTemplateSummaryResponse
    {
        public int TotalCount { get; set; }
        public List<TemplateSummaryResponse> Results { get; set; }
    }
}
