using System;
using HeloEmail.Sdk.Activity;
using HeloEmail.Sdk.Broadcasts;
using HeloEmail.Sdk.Channels;
using HeloEmail.Sdk.Domains;
using HeloEmail.Sdk.Sending;
using HeloEmail.Sdk.Statistics;
using HeloEmail.Sdk.WebhookEndpoints;
using Microsoft.Extensions.DependencyInjection;

namespace HeloEmail.Sdk
{
    public static class ServiceCollectionExtensions
    {
        public static void AddHelo(this IServiceCollection services, string apiKey,
            string baseUrl = "https://api.helohq.com")
        {
            services.AddHeloApiClients();
            services.AddHeloHttpClient(apiKey, baseUrl);
        }

        public static void AddHeloApiClients(this IServiceCollection services)
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

        public static void AddHeloHttpClient(this IServiceCollection services, string apiKey,
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