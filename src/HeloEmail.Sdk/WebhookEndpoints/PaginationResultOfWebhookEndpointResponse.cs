using System.Collections.Generic;

namespace HeloEmail.Sdk.WebhookEndpoints
{
    public class PaginationResultOfWebhookEndpointResponse
    {
        public int TotalCount { get; set; }
        public List<WebhookEndpointResponse> Results { get; set; }
    }
}
