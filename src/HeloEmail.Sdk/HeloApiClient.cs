using HeloEmail.Sdk.Activity;
using HeloEmail.Sdk.Broadcasts;
using HeloEmail.Sdk.Channels;
using HeloEmail.Sdk.Domains;
using HeloEmail.Sdk.Sending;
using HeloEmail.Sdk.Statistics;
using HeloEmail.Sdk.Suppressions;
using HeloEmail.Sdk.Webhooks;

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
            IWebhooksClient webhooks)
        {
            Activity = activity;
            Broadcasts = broadcasts;
            Channels = channels;
            Domains = domains;
            Sending = sending;
            Statistics = statistics;
            Suppressions = suppressions;
            Webhooks = webhooks;
        }

        public IActivityClient Activity { get; }
        public IBroadcastsClient Broadcasts { get; }
        public IChannelsClient Channels { get; }
        public IDomainsClient Domains { get; }
        public ISendingClient Sending { get; }
        public IStatisticsClient Statistics { get; }
        public ISuppressionsClient Suppressions { get; }
        public IWebhooksClient Webhooks { get; }
    }
}
