using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;
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

        /// <summary>
        /// List suppressions
        /// </summary>
        public Task<PaginatedResponseOfSuppressionResponse> List(
            string channelId,
            MailType mailType,
            SuppressionReason? reason = null,
            string email = null,
            int? limit = null,
            int? offset = null)
        {
            var query = new List<(string, string)>
            {
                ("channelId", channelId),
                ("mailType", ToQueryValue(mailType)),
                ("reason", ToQueryValue(reason)),
                ("email", email),
                ("limit", limit?.ToString(CultureInfo.InvariantCulture)),
                ("offset", offset?.ToString(CultureInfo.InvariantCulture)),
            };

            return Get<PaginatedResponseOfSuppressionResponse>(BuildUrl("/suppressions", query));
        }

        /// <summary>
        /// Create suppressions
        /// </summary>
        public Task<CreateSuppressionsResponse> Create(CreateSuppressionsRequest request) =>
            Post<CreateSuppressionsRequest, CreateSuppressionsResponse>("/suppressions", request);

        /// <summary>
        /// Remove suppressions
        /// </summary>
        public Task<RemoveSuppressionsResponse> Remove(RemoveSuppressionsRequest request) =>
            Post<RemoveSuppressionsRequest, RemoveSuppressionsResponse>("/suppressions/remove", request);
    }
}
