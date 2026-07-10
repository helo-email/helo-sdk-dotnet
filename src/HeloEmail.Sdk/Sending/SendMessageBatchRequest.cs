using System.Collections.Generic;

namespace HeloEmail.Sdk.Sending
{
    public class SendMessageBatchRequest
    {
        public List<SendMessageRequest> Requests { get; set; }
    }
}
