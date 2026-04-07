using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Helo.ApiClient.Statistics
{
    public class StatisticsClient : BaseClient, IStatisticsClient
    {
        public StatisticsClient([FromKeyedServices(KeyedServices.HeloApiClientName)] HttpClient httpClient,
            ILogger<StatisticsClient> logger) :
            base(httpClient, logger)
        {
        }

        public Task<StatisticsHourlyResponse> RetrieveHourly(string from, string to, string channelId = null,
            IEnumerable<string> tags = null)
        {
            var url = BuildUrl("/activity/statistics/hourly", from, to, channelId: channelId, tags: tags);
            return Get<StatisticsHourlyResponse>(url);
        }

        public Task<StatisticsDailyResponse> RetrieveDaily(string from, string to, string timezone,
            string channelId = null, IEnumerable<string> tags = null)
        {
            var url = BuildUrl("/activity/statistics/daily", from, to, timezone: timezone, channelId: channelId,
                tags: tags);
            return Get<StatisticsDailyResponse>(url);
        }

        public Task<StatisticsTotalsResponse> RetrieveTotals(string from, string to, string channelId = null,
            IEnumerable<string> tags = null)
        {
            var url = BuildUrl("/activity/statistics/totals", from, to, channelId: channelId, tags: tags);
            return Get<StatisticsTotalsResponse>(url);
        }

        private static string BuildUrl(string path, string from, string to, string timezone = null,
            string channelId = null, IEnumerable<string> tags = null)
        {
            var sb = new StringBuilder(path);
            sb.Append($"?from={Uri.EscapeDataString(from)}&to={Uri.EscapeDataString(to)}");
            if (timezone != null)
                sb.Append($"&timezone={Uri.EscapeDataString(timezone)}");
            if (channelId != null)
                sb.Append($"&channelId={Uri.EscapeDataString(channelId)}");
            if (tags != null)
                foreach (var tag in tags)
                    sb.Append($"&tags={Uri.EscapeDataString(tag)}");
            return sb.ToString();
        }
    }
}