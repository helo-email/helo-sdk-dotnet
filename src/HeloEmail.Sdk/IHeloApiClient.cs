using HeloEmail.Sdk.Activity;
using HeloEmail.Sdk.Broadcasts;
using HeloEmail.Sdk.Channels;
using HeloEmail.Sdk.Domains;
using HeloEmail.Sdk.Sending;
using HeloEmail.Sdk.Statistics;
using HeloEmail.Sdk.WebhookEndpoints;

namespace HeloEmail.Sdk
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
