using System.Collections.Generic;

namespace HeloEmail.Sdk.Statistics
{
    public class StatisticsDailyResponse
    {
        public List<StatisticsDailyResponseResult> Results { get; set; }
    }

    public class StatisticsDailyResponseResult
    {
        public string Timestamp { get; set; }
        public DeliveryStats Transactional { get; set; }
        public DeliveryStats Broadcast { get; set; }
    }
}
