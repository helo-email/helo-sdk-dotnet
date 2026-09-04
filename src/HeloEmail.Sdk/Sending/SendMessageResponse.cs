using System.Collections.Generic;

namespace HeloEmail.Sdk.Sending
{
    public class SendMessageResponse
    {
        public string Status { get; set; }
        public string MessageId { get; set; }
        public List<string> Suppressions { get; set; }
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
    }
}
