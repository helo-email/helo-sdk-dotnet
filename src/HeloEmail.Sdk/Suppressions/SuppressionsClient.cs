using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using HeloEmail.Sdk.Activity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace HeloEmail.Sdk.Suppressions
{
    public class SuppressionsClient : BaseClient, ISuppressionsClient
    {
        public SuppressionsClient([FromKeyedServices(KeyedServices.HeloApiClientName)] HttpClient httpClient,
            ILogger<SuppressionsClient> logger) :
            base(httpClient, logger)
        {
        }

        public Task<PaginatedResponseOfSuppressionResponse> List(string channelId, MailType mailType,
            SuppressionReason? reason = null, string email = null, int? limit = null, int? offset = null)
        {
            var query = new List<(string, string)>
            {
                ("channelId", channelId),
                ("mailType", mailType.ToString().ToLower()),
                ("reason", reason?.ToString().ToLower()),
                ("email", email),
                ("limit", limit?.ToString()),
                ("offset", offset?.ToString()),
            };
            return Get<PaginatedResponseOfSuppressionResponse>(BuildUrl("/suppressions", query));
        }

        public Task<CreateSuppressionsResponse> Create(CreateSuppressionsRequest request) =>
            Post<CreateSuppressionsRequest, CreateSuppressionsResponse>("/suppressions", request);

        public Task<RemoveSuppressionsResponse> Remove(RemoveSuppressionsRequest request) =>
            Post<RemoveSuppressionsRequest, RemoveSuppressionsResponse>("/suppressions/remove", request);
    }
}
