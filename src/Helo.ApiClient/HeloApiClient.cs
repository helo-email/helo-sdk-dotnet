using Helo.ApiClient.Activity;
using Helo.ApiClient.Statistics;

namespace Helo.ApiClient
{
    public class HeloApiClient : IHeloApiClient
    {
        public HeloApiClient(IHeloActivityClient activity, IHeloStatisticsClient statistics)
        {
            Activity = activity;
            Statistics = statistics;
        }

        public IHeloActivityClient Activity { get; }
        public IHeloStatisticsClient Statistics { get; }
    }
}