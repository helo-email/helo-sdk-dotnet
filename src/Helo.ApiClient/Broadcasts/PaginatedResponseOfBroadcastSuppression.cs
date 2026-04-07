using System.Collections.Generic;

namespace Helo.ApiClient.Broadcasts
{
    public class PaginatedResponseOfBroadcastSuppression
    {
        public int TotalCount { get; set; }
        public List<string> Results { get; set; }
    }
}
