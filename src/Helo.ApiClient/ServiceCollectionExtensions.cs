using System;
using Helo.ApiClient.Activity;
using Helo.ApiClient.Broadcasts;
using Helo.ApiClient.Channels;
using Helo.ApiClient.Domains;
using Helo.ApiClient.Sending;
using Helo.ApiClient.Statistics;
using Helo.ApiClient.WebhookEndpoints;
using Microsoft.Extensions.DependencyInjection;

namespace Helo.ApiClient
{
    public static class ServiceCollectionExtensions
    {
        public static void RegisterHeloApiClients(this IServiceCollection services)
        {
            services.AddTransient<IActivityClient, ActivityClient>();
            services.AddTransient<IBroadcastsClient, BroadcastsClient>();
            services.AddTransient<IChannelsClient, ChannelsClient>();
            services.AddTransient<IDomainsClient, DomainsClient>();
            services.AddTransient<ISendingClient, SendingClient>();
            services.AddTransient<IStatisticsClient, StatisticsClient>();
            services.AddTransient<IWebhookEndpointsClient, WebhookEndpointsClient>();
            services.AddTransient<IHeloApiClient, HeloApiClient>();
        }

        public static void RegisterHeloHttpClient(this IServiceCollection services, string apiKey,
            string baseUrl = "https://api.helohq.com")
        {
            var baseUri = new Uri(baseUrl);
            services
                .AddHttpClient(KeyedServices.HeloApiClientName, c =>
                {
                    c.BaseAddress = baseUri;
                    c.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");
                })
                .AddAsKeyed();
        }
    }
}