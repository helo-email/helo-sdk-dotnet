using System.Collections.Generic;

namespace HeloEmail.Sdk.Broadcasts
{
    public class PaginatedResponseOfBroadcast
    {
        public int TotalCount { get; set; }
        public List<BroadcastResponse> Results { get; set; }
    }
}
