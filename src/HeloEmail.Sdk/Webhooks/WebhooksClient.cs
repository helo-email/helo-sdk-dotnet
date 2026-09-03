using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace HeloEmail.Sdk.Webhooks
{
    public class WebhooksClient : BaseClient, IWebhooksClient
    {
        public WebhooksClient([FromKeyedServices(KeyedServices.HeloApiClientName)] HttpClient httpClient,
            ILogger<WebhooksClient> logger) :
            base(httpClient, logger)
        {
        }

        /// <summary>
        /// List all webhooks
        /// </summary>
        public Task<PaginationResultOfWebhookResponse> List(
            int? limit = null,
            int? offset = null,
            IEnumerable<string> channelIds = null)
        {
            var query = new List<(string, string)>
            {
                ("limit", limit?.ToString(CultureInfo.InvariantCulture)),
                ("offset", offset?.ToString(CultureInfo.InvariantCulture)),
            };

            if (channelIds != null)
                query.AddRange(channelIds.Select(x => ("channelIds", x)));

            return Get<PaginationResultOfWebhookResponse>(BuildUrl("/webhooks", query));
        }

        /// <summary>
        /// Create a webhook
        /// </summary>
        public Task<WebhookResponse> Create(CreateWebhookRequest request) =>
            Post<CreateWebhookRequest, WebhookResponse>("/webhooks", request);

        /// <summary>
        /// Retrieve a webhook
        /// </summary>
        public Task<WebhookResponse> Retrieve(string id) =>
            Get<WebhookResponse>($"/webhooks/{Uri.EscapeDataString(id)}");

        /// <summary>
        /// Update a webhook
        /// </summary>
        public Task<WebhookResponse> Update(string id, UpdateWebhookRequest request) =>
            Patch<UpdateWebhookRequest, WebhookResponse>($"/webhooks/{Uri.EscapeDataString(id)}", request);

        /// <summary>
        /// Delete a webhook
        /// </summary>
        public new Task Delete(string id) =>
            base.Delete($"/webhooks/{Uri.EscapeDataString(id)}");

        /// <summary>
        /// Regenerate webhook signing key
        /// </summary>
        public Task<WebhookResponse> RegenerateSigningKey(string id) =>
            Post<WebhookResponse>($"/webhooks/{Uri.EscapeDataString(id)}/regenerate-signing-key");
    }
}
