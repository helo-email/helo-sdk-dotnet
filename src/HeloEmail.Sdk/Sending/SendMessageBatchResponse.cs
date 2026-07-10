using System.Collections.Generic;

namespace HeloEmail.Sdk.Sending
{
    public class SendMessageBatchResponse
    {
        public List<SendMessageResponse> Responses { get; set; }
    }
}
