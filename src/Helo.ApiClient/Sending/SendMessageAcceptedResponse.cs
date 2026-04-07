using System.Collections.Generic;

namespace Helo.ApiClient.Sending
{
    public class SendMessageAcceptedResponse
    {
        public string Status { get; set; }
        public string MessageId { get; set; }
        public List<string> Suppressions { get; set; }
    }
}
