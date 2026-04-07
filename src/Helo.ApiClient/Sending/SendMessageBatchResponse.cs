using System.Collections.Generic;

namespace Helo.ApiClient.Sending
{
    public class SendMessageBatchResponse
    {
        public List<SendMessageResponse> Responses { get; set; }
    }
}
