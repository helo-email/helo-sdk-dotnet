using System.Collections.Generic;

namespace HeloEmail.Sdk.Broadcasts
{
    public class PaginatedResponseOfBroadcastFailure
    {
        public int TotalCount { get; set; }
        public List<BroadcastFailureResponse> Results { get; set; }
    }
}
