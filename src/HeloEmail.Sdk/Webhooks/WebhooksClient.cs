using System;
using System.Collections.Generic;
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

        public Task<WebhookResponse> Create(CreateWebhookRequest request) =>
            Post<CreateWebhookRequest, WebhookResponse>("/webhooks", request);

        public Task<PaginationResultOfWebhookResponse> List(int? limit = null, int? offset = null,
            IEnumerable<string> channelIds = null)
        {
            var query = new List<(string, string)>
            {
                ("limit", limit?.ToString()),
                ("offset", offset?.ToString()),
            };
            if (channelIds != null)
                query.AddRange(channelIds.Select(id => ("channelIds", id)));
            return Get<PaginationResultOfWebhookResponse>(BuildUrl("/webhooks", query));
        }

        public Task<WebhookResponse> Retrieve(string id) =>
            Get<WebhookResponse>($"/webhooks/{Uri.EscapeDataString(id)}");

        public Task<WebhookResponse> Update(string id, UpdateWebhookRequest request) =>
            Patch<UpdateWebhookRequest, WebhookResponse>(
                $"/webhooks/{Uri.EscapeDataString(id)}", request);

        public new Task Delete(string id) =>
            base.Delete($"/webhooks/{Uri.EscapeDataString(id)}");

        public Task<WebhookResponse> RegenerateSigningKey(string id) =>
            Post<WebhookResponse>($"/webhooks/{Uri.EscapeDataString(id)}/regenerate-signing-key");

    }
}