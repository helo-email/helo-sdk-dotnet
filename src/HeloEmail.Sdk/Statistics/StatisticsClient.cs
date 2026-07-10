using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace HeloEmail.Sdk.Statistics
{
    public class StatisticsClient : BaseClient, IStatisticsClient
    {
        public StatisticsClient([FromKeyedServices(KeyedServices.HeloApiClientName)] HttpClient httpClient,
            ILogger<StatisticsClient> logger) :
            base(httpClient, logger)
        {
        }

        public Task<StatisticsHourlyResponse> RetrieveHourly(DateTimeOffset from, DateTimeOffset to,
            string channelId = null, IEnumerable<string> tags = null)
        {
            var url = BuildUrl("/activity/statistics/hourly", from, to, channelId: channelId, tags: tags);
            return Get<StatisticsHourlyResponse>(url);
        }

        public Task<StatisticsDailyResponse> RetrieveDaily(DateTimeOffset from, DateTimeOffset to, string timezone,
            string channelId = null, IEnumerable<string> tags = null)
        {
            var url = BuildUrl("/activity/statistics/daily", from, to, timezone: timezone, channelId: channelId,
                tags: tags, dateOnly: true);
            return Get<StatisticsDailyResponse>(url);
        }

        public Task<StatisticsTotalsResponse> RetrieveTotals(DateTimeOffset from, DateTimeOffset to,
            string channelId = null, IEnumerable<string> tags = null)
        {
            var url = BuildUrl("/activity/statistics/totals", from, to, channelId: channelId, tags: tags);
            return Get<StatisticsTotalsResponse>(url);
        }

        private static string BuildUrl(string path, DateTimeOffset from, DateTimeOffset to, string timezone = null,
            string channelId = null, IEnumerable<string> tags = null, bool dateOnly = false)
        {
            var sb = new StringBuilder(path);

            var fromFormatted = Uri.EscapeDataString(from.ToString("O"));
            if (dateOnly)
            {
                fromFormatted = fromFormatted.Split('T').First();
            }

            var toFormatted = Uri.EscapeDataString(to.ToString("O"));
            if (dateOnly)
            {
                toFormatted = toFormatted.Split('T').First();
            }

            sb.Append($"?from={fromFormatted}&to={toFormatted}");
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