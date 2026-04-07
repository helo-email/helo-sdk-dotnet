using System.Collections.Generic;

namespace Helo.ApiClient.WebhookEndpoints
{
    public class CreateWebhookEndpointRequest
    {
        public string Url { get; set; }
        public List<WebhookEvent> Events { get; set; }
        public string ChannelId { get; set; }
        public List<WebhookHeader> AdditionalHeaders { get; set; }
        public bool? Enabled { get; set; }
    }
}
