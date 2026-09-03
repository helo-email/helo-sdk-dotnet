using System.Collections.Generic;
using System.Threading.Tasks;

namespace HeloEmail.Sdk.Webhooks
{
    public interface IWebhooksClient
    {
        Task<WebhookResponse> Create(CreateWebhookRequest request);
        Task<PaginationResultOfWebhookResponse> List(int? limit = null, int? offset = null, IEnumerable<string> channelIds = null);
        Task<WebhookResponse> Retrieve(string id);
        Task<WebhookResponse> Update(string id, UpdateWebhookRequest request);
        Task Delete(string id);
        Task<WebhookResponse> RegenerateSigningKey(string id);
    }
}
