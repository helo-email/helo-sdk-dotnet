using System.Collections.Generic;
using System.Threading.Tasks;

namespace Helo.ApiClient.WebhookEndpoints
{
    public interface IHeloWebhookEndpointsClient
    {
        Task<WebhookEndpointResponse> Create(CreateWebhookEndpointRequest request);
        Task<PaginationResultOfWebhookEndpointResponse> List(int? limit = null, int? offset = null, IEnumerable<string> channelIds = null);
        Task<WebhookEndpointResponse> Retrieve(string id);
        Task<WebhookEndpointResponse> Update(string id, UpdateWebhookEndpointRequest request);
        Task Delete(string id);
        Task<WebhookEndpointResponse> RegenerateSigningKey(string id);
    }
}
