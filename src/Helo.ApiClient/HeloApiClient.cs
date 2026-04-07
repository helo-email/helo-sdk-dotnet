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
            IActivityClient activity,
            IBroadcastsClient broadcasts,
            IChannelsClient channels,
            IDomainsClient domains,
            ISendingClient sending,
            IStatisticsClient statistics,
            IWebhookEndpointsClient webhookEndpoints)
        {
            Activity = activity;
            Broadcasts = broadcasts;
            Channels = channels;
            Domains = domains;
            Sending = sending;
            Statistics = statistics;
            WebhookEndpoints = webhookEndpoints;
        }

        public IActivityClient Activity { get; }
        public IBroadcastsClient Broadcasts { get; }
        public IChannelsClient Channels { get; }
        public IDomainsClient Domains { get; }
        public ISendingClient Sending { get; }
        public IStatisticsClient Statistics { get; }
        public IWebhookEndpointsClient WebhookEndpoints { get; }
    }
}
