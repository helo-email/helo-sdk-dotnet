using Helo.ApiClient.Activity;
using Helo.ApiClient.Broadcasts;
using Helo.ApiClient.Channels;
using Helo.ApiClient.Domains;
using Helo.ApiClient.Sending;
using Helo.ApiClient.Statistics;
using Helo.ApiClient.WebhookEndpoints;

namespace Helo.ApiClient
{
    public interface IHeloApiClient
    {
        IActivityClient Activity { get; }
        IBroadcastsClient Broadcasts { get; }
        IChannelsClient Channels { get; }
        IDomainsClient Domains { get; }
        ISendingClient Sending { get; }
        IStatisticsClient Statistics { get; }
        IWebhookEndpointsClient WebhookEndpoints { get; }
    }
}
