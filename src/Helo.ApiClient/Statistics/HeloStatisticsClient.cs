using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Helo.ApiClient.Statistics
{
    public class HeloStatisticsClient : HeloBaseClient, IHeloStatisticsClient
    {
        public HeloStatisticsClient([FromKeyedServices] HttpClient httpClient, ILogger<HeloStatisticsClient> logger) :
            base(httpClient, logger)
        {
        }
    }
}