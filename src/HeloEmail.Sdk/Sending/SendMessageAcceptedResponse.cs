using System.Collections.Generic;

namespace HeloEmail.Sdk.Sending
{
    public class SendMessageAcceptedResponse
    {
        public string Status { get; set; }
        public string MessageId { get; set; }
        public List<string> Suppressions { get; set; }
    }
}
