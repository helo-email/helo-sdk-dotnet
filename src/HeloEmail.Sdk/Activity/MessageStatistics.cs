namespace HeloEmail.Sdk.Activity
{
    public class MessageStatistics
    {
        public int Delivered { get; set; }
        public int Bounced { get; set; }
        public int Opened { get; set; }
        public int Clicked { get; set; }
        public int Complained { get; set; }
        public int Unsubscribed { get; set; }
    }
}
