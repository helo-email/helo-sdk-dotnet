using System.Collections.Generic;

namespace Helo.ApiClient.Channels
{
    public class PaginationResultOfChannelBasicResponse
    {
        public int TotalCount { get; set; }
        public List<ChannelBasicResponse> Results { get; set; }
    }
}
