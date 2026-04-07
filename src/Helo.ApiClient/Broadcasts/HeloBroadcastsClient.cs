using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Helo.ApiClient.Broadcasts
{
    public class HeloBroadcastsClient : HeloBaseClient, IHeloBroadcastsClient
    {
        public HeloBroadcastsClient([FromKeyedServices(KeyedServices.HeloApiClientName)] HttpClient httpClient,
            ILogger<HeloBroadcastsClient> logger) :
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

        private static string BuildUrl(string path, List<(string Key, string Value)> parameters)
        {
            var sb = new StringBuilder(path);
            var first = true;
            foreach (var (key, value) in parameters)
            {
                if (value == null) continue;
                sb.Append(first ? '?' : '&');
                sb.Append($"{Uri.EscapeDataString(key)}={Uri.EscapeDataString(value)}");
                first = false;
            }
            return sb.ToString();
        }
    }
}
