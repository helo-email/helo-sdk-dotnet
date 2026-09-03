using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HeloEmail.Sdk.Statistics
{
    public interface IStatisticsClient
    {
        /// <summary>
        /// Retrieve hourly statistics
        /// </summary>
        Task<StatisticsHourlyResponse> RetrieveHourly(
            DateTimeOffset from,
            DateTimeOffset to,
            string channelId = null,
            IEnumerable<string> tags = null);

        /// <summary>
        /// Retrieve daily statistics
        /// </summary>
        Task<StatisticsDailyResponse> RetrieveDaily(
            DateTimeOffset from,
            DateTimeOffset to,
            string timezone,
            string channelId = null,
            IEnumerable<string> tags = null);

        /// <summary>
        /// Retrieve all time statistics
        /// </summary>
        Task<StatisticsTotalsResponse> RetrieveTotals(
            DateTimeOffset from,
            DateTimeOffset to,
            string channelId = null,
            IEnumerable<string> tags = null);
    }
}
