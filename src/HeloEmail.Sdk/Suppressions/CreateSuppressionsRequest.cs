using System.Collections.Generic;

namespace HeloEmail.Sdk.Suppressions
{
    public class CreateSuppressionsRequest
    {
        public string ChannelId { get; set; }
        public MailType MailType { get; set; }
        public List<string> Emails { get; set; }
    }
}
