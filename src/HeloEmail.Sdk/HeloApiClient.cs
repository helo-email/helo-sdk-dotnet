using HeloEmail.Sdk.Activity;
using HeloEmail.Sdk.Broadcasts;
using HeloEmail.Sdk.Channels;
using HeloEmail.Sdk.Domains;
using HeloEmail.Sdk.Sending;
using HeloEmail.Sdk.Statistics;
using HeloEmail.Sdk.Suppressions;
using HeloEmail.Sdk.WebhookEndpoints;

namespace HeloEmail.Sdk
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
            ISuppressionsClient suppressions,
            IWebhookEndpointsClient webhookEndpoints)
        {
            Activity = activity;
            Broadcasts = broadcasts;
            Channels = channels;
            Domains = domains;
            Sending = sending;
            Statistics = statistics;
            Suppressions = suppressions;
            WebhookEndpoints = webhookEndpoints;
        }

        public IActivityClient Activity { get; }
        public IBroadcastsClient Broadcasts { get; }
        public IChannelsClient Channels { get; }
        public IDomainsClient Domains { get; }
        public ISendingClient Sending { get; }
        public IStatisticsClient Statistics { get; }
        public ISuppressionsClient Suppressions { get; }
        public IWebhookEndpointsClient WebhookEndpoints { get; }
    }
}
