using Helo.ApiClient.Statistics;

namespace Helo.ApiClient
{
    public class HeloApiClient : IHeloApiClient
    {
        public HeloApiClient(IHeloStatisticsClient statistics)
        {
            Statistics = statistics;
        }

        public IHeloStatisticsClient Statistics { get; }
    }
}