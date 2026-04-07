using System;
using System.Collections.Generic;

namespace Helo.ApiClient.Statistics
{
    public class StatisticsHourlyResponse
    {
        public List<HourlyStatisticsResult> Results { get; set; }
    }

    public class HourlyStatisticsResult
    {
        public DateTimeOffset Timestamp { get; set; }
        public DeliveryStats Transactional { get; set; }
        public DeliveryStats Broadcast { get; set; }
    }
}
