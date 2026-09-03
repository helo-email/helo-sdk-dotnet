using System.Collections.Generic;
using System.Threading.Tasks;

namespace HeloEmail.Sdk.Webhooks
{
    public interface IWebhooksClient
    {
        /// <summary>
        /// List all webhooks
        /// </summary>
        Task<PaginationResultOfWebhookResponse> List(
            int? limit = null,
            int? offset = null,
            IEnumerable<string> channelIds = null);

        /// <summary>
        /// Create a webhook
        /// </summary>
        Task<WebhookResponse> Create(CreateWebhookRequest request);

        /// <summary>
        /// Retrieve a webhook
        /// </summary>
        Task<WebhookResponse> Retrieve(string id);

        /// <summary>
        /// Update a webhook
        /// </summary>
        Task<WebhookResponse> Update(string id, UpdateWebhookRequest request);

        /// <summary>
        /// Delete a webhook
        /// </summary>
        Task Delete(string id);

        /// <summary>
        /// Regenerate webhook signing key
        /// </summary>
        Task<WebhookResponse> RegenerateSigningKey(string id);
    }
}
