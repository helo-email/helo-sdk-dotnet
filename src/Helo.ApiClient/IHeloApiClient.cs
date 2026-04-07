using Helo.ApiClient.Activity;
using Helo.ApiClient.Statistics;

namespace Helo.ApiClient
{
    public interface IHeloApiClient
    {
        IHeloActivityClient Activity { get; }
        IHeloStatisticsClient Statistics { get; }
    }
}