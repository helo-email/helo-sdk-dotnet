using Helo.ApiClient.Activity;
using Helo.ApiClient.Broadcasts;
using Helo.ApiClient.Channels;
using Helo.ApiClient.Domains;
using Helo.ApiClient.Sending;
using Helo.ApiClient.Statistics;
using Helo.ApiClient.WebhookEndpoints;

namespace Helo.ApiClient
{
    public class HeloApiClient : IHeloApiClient
    {
        public HeloApiClient(
            IHeloActivityClient activity,
            IHeloBroadcastsClient broadcasts,
            IHeloChannelsClient channels,
            IHeloDomainsClient domains,
            IHeloSendingClient sending,
            IHeloStatisticsClient statistics,
            IHeloWebhookEndpointsClient webhookEndpoints)
        {
            Activity = activity;
            Broadcasts = broadcasts;
            Channels = channels;
            Domains = domains;
            Sending = sending;
            Statistics = statistics;
            WebhookEndpoints = webhookEndpoints;
        }

        public IHeloActivityClient Activity { get; }
        public IHeloBroadcastsClient Broadcasts { get; }
        public IHeloChannelsClient Channels { get; }
        public IHeloDomainsClient Domains { get; }
        public IHeloSendingClient Sending { get; }
        public IHeloStatisticsClient Statistics { get; }
        public IHeloWebhookEndpointsClient WebhookEndpoints { get; }
    }
}
