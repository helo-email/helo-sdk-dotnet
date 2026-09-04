using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace HeloEmail.Sdk.Broadcasts
{
    public class BroadcastsClient : BaseClient, IBroadcastsClient
    {
        public BroadcastsClient([FromKeyedServices(KeyedServices.HeloApiClientName)] HttpClient httpClient,
            ILogger<BroadcastsClient> logger) :
            base(httpClient, logger)
        {
        }

        /// <summary>
        /// List broadcasts
        /// </summary>
        public Task<PaginatedResponseOfBroadcast> List(
            string channelId,
            BroadcastStatus? status = null,
            string subject = null,
            int? limit = null,
            int? offset = null)
        {
            var query = new List<(string, string)>
            {
                ("channelId", channelId),
                ("status", ToQueryValue(status)),
                ("subject", subject),
                ("limit", limit?.ToString(CultureInfo.InvariantCulture)),
                ("offset", offset?.ToString(CultureInfo.InvariantCulture)),
            };

            return Get<PaginatedResponseOfBroadcast>(BuildUrl("/broadcasts", query));
        }

        /// <summary>
        /// Retrieve a broadcast
        /// </summary>
        public Task<BroadcastDetailsResponse> Retrieve(string id) =>
            Get<BroadcastDetailsResponse>($"/broadcasts/{Uri.EscapeDataString(id)}");

        /// <summary>
        /// List failed broadcast messages
        /// </summary>
        public Task<PaginatedResponseOfBroadcastFailure> ListFailures(
            string id,
            int? limit = null,
            int? offset = null)
        {
            var query = new List<(string, string)>
            {
                ("limit", limit?.ToString(CultureInfo.InvariantCulture)),
                ("offset", offset?.ToString(CultureInfo.InvariantCulture)),
            };

            return Get<PaginatedResponseOfBroadcastFailure>(BuildUrl($"/broadcasts/{Uri.EscapeDataString(id)}/failures", query));
        }

        /// <summary>
        /// List broadcast suppressed recipients
        /// </summary>
        public Task<PaginatedResponseOfBroadcastSuppression> ListSuppressions(
            string id,
            int? limit = null,
            int? offset = null)
        {
            var query = new List<(string, string)>
            {
                ("limit", limit?.ToString(CultureInfo.InvariantCulture)),
                ("offset", offset?.ToString(CultureInfo.InvariantCulture)),
            };

            return Get<PaginatedResponseOfBroadcastSuppression>(BuildUrl($"/broadcasts/{Uri.EscapeDataString(id)}/suppressions", query));
        }
    }
}
