namespace HeloEmail.Sdk.Statistics
{
    public class DeliveryStats
    {
        public int Sent { get; set; }
        public int Delivered { get; set; }
        public int Opened { get; set; }
        public int Clicked { get; set; }
        public int Bounced { get; set; }
        public int Unsubscribed { get; set; }
        public int Complained { get; set; }
    }
}
