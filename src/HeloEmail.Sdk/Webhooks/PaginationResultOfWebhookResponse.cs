using System.Collections.Generic;

namespace HeloEmail.Sdk.Webhooks
{
    public class PaginationResultOfWebhookResponse
    {
        public int TotalCount { get; set; }
        public List<WebhookResponse> Results { get; set; }
    }
}
