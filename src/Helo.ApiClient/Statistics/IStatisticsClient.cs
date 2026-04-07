using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Helo.ApiClient.Statistics
{
    public interface IStatisticsClient
    {
        Task<StatisticsHourlyResponse> RetrieveHourly(DateTimeOffset from, DateTimeOffset to, string channelId = null, IEnumerable<string> tags = null);
        Task<StatisticsDailyResponse> RetrieveDaily(DateTimeOffset from, DateTimeOffset to, string timezone, string channelId = null, IEnumerable<string> tags = null);
        Task<StatisticsTotalsResponse> RetrieveTotals(DateTimeOffset from, DateTimeOffset to, string channelId = null, IEnumerable<string> tags = null);
    }
}
