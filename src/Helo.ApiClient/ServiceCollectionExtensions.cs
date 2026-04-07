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
            services.AddTransient<IHeloActivityClient, HeloActivityClient>();
            services.AddTransient<IHeloBroadcastsClient, HeloBroadcastsClient>();
            services.AddTransient<IHeloChannelsClient, HeloChannelsClient>();
            services.AddTransient<IHeloDomainsClient, HeloDomainsClient>();
            services.AddTransient<IHeloSendingClient, HeloSendingClient>();
            services.AddTransient<IHeloStatisticsClient, HeloStatisticsClient>();
            services.AddTransient<IHeloWebhookEndpointsClient, HeloWebhookEndpointsClient>();
            services.AddTransient<IHeloApiClient, HeloApiClient>();
        }

        public static void RegisterHeloHttpClient(this IServiceCollection services,
            string baseUrl = "https://api.helohq.com")
        {
            var baseUri = new Uri(baseUrl);
            services
                .AddHttpClient(KeyedServices.HeloApiClientName, c => c.BaseAddress = baseUri)
                .AddAsKeyed();
        }
    }
}