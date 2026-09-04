using System.Collections.Generic;

namespace HeloEmail.Sdk.Broadcasts
{
    public class RecipientHeaders
    {
        public List<MailAddress> To { get; set; }
        public List<MailAddress> Cc { get; set; }
        public List<MailAddress> Bcc { get; set; }
    }
}
