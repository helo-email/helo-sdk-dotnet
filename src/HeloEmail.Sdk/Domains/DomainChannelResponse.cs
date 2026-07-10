namespace HeloEmail.Sdk.Domains
{
    public class DomainChannelResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public DeliveryType DeliveryType { get; set; }
        public bool? Deleted { get; set; }
    }
}
