using System.Collections.Generic;

namespace HeloEmail.Sdk.Channels
{
    public class PaginationResultOfChannelBasicResponse
    {
        public int TotalCount { get; set; }
        public List<ChannelBasicResponse> Results { get; set; }
    }
}
