using Helo.ApiClient.Statistics;

namespace Helo.ApiClient
{
    public interface IHeloApiClient
    {
        IHeloStatisticsClient Statistics { get; }
    }
}