using System.Collections.Generic;

namespace HeloEmail.Sdk.Broadcasts
{
    public class BroadcastFailureResponse
    {
        public RecipientHeaders Recipients { get; set; }
        public int MessageIndex { get; set; }
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
    }

    public class RecipientHeaders
    {
        public List<MailAddress> To { get; set; }
        public List<MailAddress> Cc { get; set; }
        public List<MailAddress> Bcc { get; set; }
    }
}
