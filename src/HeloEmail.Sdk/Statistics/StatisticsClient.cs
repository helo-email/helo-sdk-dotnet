using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

        /// <summary>
        /// Retrieve hourly statistics
        /// </summary>
        public Task<StatisticsHourlyResponse> RetrieveHourly(
            DateTimeOffset from,
            DateTimeOffset to,
            string channelId = null,
            IEnumerable<string> tags = null)
        {
            var query = new List<(string, string)>
            {
                ("from", from.ToString("O")),
                ("to", to.ToString("O")),
                ("channelId", channelId),
            };

            if (tags != null)
                query.AddRange(tags.Select(x => ("tags", x)));

            return Get<StatisticsHourlyResponse>(BuildUrl("/statistics/hourly", query));
        }

        /// <summary>
        /// Retrieve daily statistics
        /// </summary>
        public Task<StatisticsDailyResponse> RetrieveDaily(
            DateTimeOffset from,
            DateTimeOffset to,
            string timezone,
            string channelId = null,
            IEnumerable<string> tags = null)
        {
            var query = new List<(string, string)>
            {
                ("from", from.ToString("yyyy-MM-dd")),
                ("to", to.ToString("yyyy-MM-dd")),
                ("timezone", timezone),
                ("channelId", channelId),
            };

            if (tags != null)
                query.AddRange(tags.Select(x => ("tags", x)));

            return Get<StatisticsDailyResponse>(BuildUrl("/statistics/daily", query));
        }

        /// <summary>
        /// Retrieve all time statistics
        /// </summary>
        public Task<StatisticsTotalsResponse> RetrieveTotals(
            DateTimeOffset from,
            DateTimeOffset to,
            string channelId = null,
            IEnumerable<string> tags = null)
        {
            var query = new List<(string, string)>
            {
                ("from", from.ToString("O")),
                ("to", to.ToString("O")),
                ("channelId", channelId),
            };

            if (tags != null)
                query.AddRange(tags.Select(x => ("tags", x)));

            return Get<StatisticsTotalsResponse>(BuildUrl("/statistics/totals", query));
        }
    }
}
