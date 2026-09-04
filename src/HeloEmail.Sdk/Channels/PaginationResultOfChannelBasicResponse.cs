using System.Collections.Generic;

namespace HeloEmail.Sdk.Channels
{
    public class PaginationResultOfChannelBasicResponse
    {
        public List<ChannelBasicResponse> Results { get; set; }
        public int TotalCount { get; set; }
    }
}
