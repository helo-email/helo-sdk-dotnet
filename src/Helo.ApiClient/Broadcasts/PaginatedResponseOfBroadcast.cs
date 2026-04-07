using System.Collections.Generic;

namespace Helo.ApiClient.Broadcasts
{
    public class PaginatedResponseOfBroadcast
    {
        public int TotalCount { get; set; }
        public List<BroadcastResponse> Results { get; set; }
    }
}
