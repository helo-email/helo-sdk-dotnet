using System.Collections.Generic;

namespace HeloEmail.Sdk.WebhookEndpoints
{
    public class WebhookEndpointResponse
    {
        public string Id { get; set; }
        public string ChannelId { get; set; }
        public string Url { get; set; }
        public string PayloadSigningKey { get; set; }
        public bool Enabled { get; set; }
        public List<WebhookHeader> AdditionalHeaders { get; set; }
        public List<WebhookEvent> Events { get; set; }
    }
}
