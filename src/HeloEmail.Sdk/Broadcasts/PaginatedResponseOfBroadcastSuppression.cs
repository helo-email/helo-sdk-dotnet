using System.Collections.Generic;

namespace HeloEmail.Sdk.Broadcasts
{
    public class PaginatedResponseOfBroadcastSuppression
    {
        public int TotalCount { get; set; }
        public List<string> Results { get; set; }
    }
}
