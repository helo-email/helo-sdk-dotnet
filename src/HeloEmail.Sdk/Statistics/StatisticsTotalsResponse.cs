namespace HeloEmail.Sdk.Statistics
{
    public class StatisticsTotalsResponse
    {
        public DeliveryStats Transactional { get; set; }
        public DeliveryStats Broadcast { get; set; }
    }
}
