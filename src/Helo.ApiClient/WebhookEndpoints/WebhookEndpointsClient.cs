using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Helo.ApiClient.WebhookEndpoints
{
    public class WebhookEndpointsClient : BaseClient, IWebhookEndpointsClient
    {
        public WebhookEndpointsClient([FromKeyedServices(KeyedServices.HeloApiClientName)] HttpClient httpClient,
            ILogger<WebhookEndpointsClient> logger) :
            base(httpClient, logger)
        {
        }

        public Task<WebhookEndpointResponse> Create(CreateWebhookEndpointRequest request) =>
            Post<CreateWebhookEndpointRequest, WebhookEndpointResponse>("/webhook-endpoints", request);

        public Task<PaginationResultOfWebhookEndpointResponse> List(int? limit = null, int? offset = null,
            IEnumerable<string> channelIds = null)
        {
            var query = new List<(string, string)>
            {
                ("limit", limit?.ToString()),
                ("offset", offset?.ToString()),
            };
            if (channelIds != null)
                query.AddRange(channelIds.Select(id => ("channelIds", id)));
            return Get<PaginationResultOfWebhookEndpointResponse>(BuildUrl("/webhook-endpoints", query));
        }

        public Task<WebhookEndpointResponse> Retrieve(string id) =>
            Get<WebhookEndpointResponse>($"/webhook-endpoints/{Uri.EscapeDataString(id)}");

        public Task<WebhookEndpointResponse> Update(string id, UpdateWebhookEndpointRequest request) =>
            Patch<UpdateWebhookEndpointRequest, WebhookEndpointResponse>(
                $"/webhook-endpoints/{Uri.EscapeDataString(id)}", request);

        public new Task Delete(string id) =>
            base.Delete($"/webhook-endpoints/{Uri.EscapeDataString(id)}");

        public Task<WebhookEndpointResponse> RegenerateSigningKey(string id) =>
            Post<WebhookEndpointResponse>($"/webhook-endpoints/{Uri.EscapeDataString(id)}/regenerate-signing-key");

    }
}