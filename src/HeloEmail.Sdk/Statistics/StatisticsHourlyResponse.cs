using System;
using System.Collections.Generic;

namespace HeloEmail.Sdk.Statistics
{
    public class StatisticsHourlyResponse
    {
        public List<StatisticsHourlyResponseResult> Results { get; set; }
    }

    public class StatisticsHourlyResponseResult
    {
        public DateTimeOffset? Timestamp { get; set; }
        public DeliveryStats Transactional { get; set; }
        public DeliveryStats Broadcast { get; set; }
    }
}
