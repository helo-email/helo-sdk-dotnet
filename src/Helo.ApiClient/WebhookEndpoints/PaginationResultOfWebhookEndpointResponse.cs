using System.Collections.Generic;

namespace Helo.ApiClient.WebhookEndpoints
{
    public class PaginationResultOfWebhookEndpointResponse
    {
        public int TotalCount { get; set; }
        public List<WebhookEndpointResponse> Results { get; set; }
    }
}
