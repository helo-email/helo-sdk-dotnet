using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Helo.ApiClient.Broadcasts
{
    public class BroadcastsClient : BaseClient, IBroadcastsClient
    {
        public BroadcastsClient([FromKeyedServices(KeyedServices.HeloApiClientName)] HttpClient httpClient,
            ILogger<BroadcastsClient> logger) :
            base(httpClient, logger)
        {
        }

        public Task<PaginatedResponseOfBroadcast> List(string channelId, BroadcastStatus? status = null,
            string subject = null, int? limit = null, int? offset = null)
        {
            var query = new List<(string, string)>
            {
                ("channelId", channelId),
                ("status", status?.ToString().ToLower()),
                ("subject", subject),
                ("limit", limit?.ToString()),
                ("offset", offset?.ToString()),
            };
            return Get<PaginatedResponseOfBroadcast>(BuildUrl("/broadcasts", query));
        }

        public Task<BroadcastDetailsResponse> Retrieve(string id) =>
            Get<BroadcastDetailsResponse>($"/broadcasts/{Uri.EscapeDataString(id)}");

        public Task<PaginatedResponseOfBroadcastFailure> ListFailures(string id) =>
            Get<PaginatedResponseOfBroadcastFailure>($"/broadcasts/{Uri.EscapeDataString(id)}/failures");

        public Task<PaginatedResponseOfBroadcastSuppression> ListSuppressions(string id) =>
            Get<PaginatedResponseOfBroadcastSuppression>($"/broadcasts/{Uri.EscapeDataString(id)}/suppressions");

    }
}
