using System;
using Helo.ApiClient.Statistics;
using Microsoft.Extensions.DependencyInjection;

namespace Helo.ApiClient
{
    public static class ServiceCollectionExtensions
    {
        public static void RegisterHeloApiClients(this IServiceCollection services, string baseUrl)
        {
            services.AddTransient<IHeloStatisticsClient, HeloStatisticsClient>();
            services.AddTransient<HeloApiClient>();
        }

        public static void RegisterHeloHttpClient(this IServiceCollection services, string baseUrl)
        {
            var baseUri = new Uri(baseUrl);
            services
                .AddHttpClient(KeyedServices.HeloApiClientName, c => c.BaseAddress = baseUri)
                .AddAsKeyed();
        }
    }
}