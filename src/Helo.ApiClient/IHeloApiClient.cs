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
        IHeloActivityClient Activity { get; }
        IHeloBroadcastsClient Broadcasts { get; }
        IHeloChannelsClient Channels { get; }
        IHeloDomainsClient Domains { get; }
        IHeloSendingClient Sending { get; }
        IHeloStatisticsClient Statistics { get; }
        IHeloWebhookEndpointsClient WebhookEndpoints { get; }
    }
}
