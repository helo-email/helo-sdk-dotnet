using System.Collections.Generic;

namespace Helo.ApiClient.Sending
{
    public class SendMessageBatchRequest
    {
        public List<SendMessageRequest> Requests { get; set; }
    }
}
