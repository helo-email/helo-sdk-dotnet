using System.Collections.Generic;

namespace Helo.ApiClient.Broadcasts
{
    public class PaginatedResponseOfBroadcastFailure
    {
        public int TotalCount { get; set; }
        public List<BroadcastFailureResponse> Results { get; set; }
    }
}
