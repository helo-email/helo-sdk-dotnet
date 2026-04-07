using System.Collections.Generic;
using System.Threading.Tasks;

namespace Helo.ApiClient.Statistics
{
    public interface IStatisticsClient
    {
        Task<StatisticsHourlyResponse> RetrieveHourly(string from, string to, string channelId = null, IEnumerable<string> tags = null);
        Task<StatisticsDailyResponse> RetrieveDaily(string from, string to, string timezone, string channelId = null, IEnumerable<string> tags = null);
        Task<StatisticsTotalsResponse> RetrieveTotals(string from, string to, string channelId = null, IEnumerable<string> tags = null);
    }
}
