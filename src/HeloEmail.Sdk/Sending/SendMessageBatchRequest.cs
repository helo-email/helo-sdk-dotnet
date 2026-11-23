using System.Collections.Generic;

namespace HeloEmail.Sdk.Sending
{
    public class SendMessageBatchRequest
    {
        public BatchValidationMode? Validation { get; set; }
        public List<SendMessageRequest> Requests { get; set; }
    }
}
