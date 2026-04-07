using System.Collections.Generic;

namespace Helo.ApiClient.Sending
{
    /// <summary>
    /// Represents either an accepted/delayed response (MessageId populated) or a failed response
    /// (ErrorCode/ErrorMessage populated), discriminated by the Status field.
    /// </summary>
    public class SendMessageResponse
    {
        public string Status { get; set; }
        public string MessageId { get; set; }
        public List<string> Suppressions { get; set; }
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
    }
}
