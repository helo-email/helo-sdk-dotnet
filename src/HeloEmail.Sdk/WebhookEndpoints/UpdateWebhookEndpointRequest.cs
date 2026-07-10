using System.Collections.Generic;

namespace HeloEmail.Sdk.WebhookEndpoints
{
    public class UpdateWebhookEndpointRequest
    {
        public string Url { get; set; }
        public List<WebhookEvent> Events { get; set; }
        public string ChannelId { get; set; }
        public List<WebhookHeader> AdditionalHeaders { get; set; }
        public bool? Enabled { get; set; }
    }
}
