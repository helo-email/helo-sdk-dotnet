using System.Collections.Generic;

namespace Helo.ApiClient.Activity
{
    public class PaginatedMessagesResponse
    {
        public long? After { get; set; }
        public double TotalCount { get; set; }
        public List<Message> Results { get; set; }
    }
}
