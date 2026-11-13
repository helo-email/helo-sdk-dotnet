using System;
using HeloEmail.Sdk.Activity;
using HeloEmail.Sdk.Broadcasts;
using HeloEmail.Sdk.Channels;
using HeloEmail.Sdk.Domains;
using HeloEmail.Sdk.Sending;
using HeloEmail.Sdk.Statistics;
using HeloEmail.Sdk.Suppressions;
using HeloEmail.Sdk.Templates;
using HeloEmail.Sdk.Webhooks;
using Microsoft.Extensions.DependencyInjection;

namespace HeloEmail.Sdk
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds the Helo SDK to your service collection. If <c>apiKey</c> is not provided, it will be
        /// pulled from the HELO_API_KEY environment variable.
        /// </summary>
        /// <param name="services">Service collection.</param>
        /// <param name="apiKey">Optional API key. Pulled from HELO_API_KEY environment variable otherwise.</param>
        /// <param name="baseUrl">Optional base URL.</param>
        /// <param name="maxRetries">How many times a failed request is retried; 0 disables retries.
        /// See <see cref="RetryHandler"/> for which requests are retried.</param>
        /// <param name="timeout">How long each attempt may take before it is abandoned and retried
        /// (default 30 seconds); <see cref="System.Threading.Timeout.InfiniteTimeSpan"/> disables it.</param>
        public static void AddHelo(this IServiceCollection services, string apiKey = null,
            string baseUrl = "https://api.helohq.com", int maxRetries = RetryHandler.DefaultMaxRetries,
            TimeSpan? timeout = null)
        {
            apiKey = apiKey ?? Environment.GetEnvironmentVariable("HELO_API_KEY");
            services.AddHeloApiClients();
            services.AddHeloHttpClient(apiKey, baseUrl, maxRetries, timeout);
        }

        /// <summary>
        /// Adds the Helo API client classes to your service collection. This method should only be
        /// used if you are adding an HttpClient separately. Otherwise, use the
        /// <c>AddHelo</c> method.
        /// </summary>
        /// <param name="services">Service collection.</param>
        public static void AddHeloApiClients(this IServiceCollection services)
        {
            services.AddTransient<IActivityClient, ActivityClient>();
            services.AddTransient<IBroadcastsClient, BroadcastsClient>();
            services.AddTransient<IChannelsClient, ChannelsClient>();
            services.AddTransient<IDomainsClient, DomainsClient>();
            services.AddTransient<ISendingClient, SendingClient>();
            services.AddTransient<IStatisticsClient, StatisticsClient>();
            services.AddTransient<ISuppressionsClient, SuppressionsClient>();
            services.AddTransient<ITemplatesClient, TemplatesClient>();
            services.AddTransient<IWebhooksClient, WebhooksClient>();
            services.AddTransient<IHeloApiClient, HeloApiClient>();
        }

        private static void AddHeloHttpClient(this IServiceCollection services, string apiKey,
            string baseUrl, int maxRetries, TimeSpan? timeout)
        {
            var baseUri = new Uri(baseUrl);
            services
                .AddHttpClient(KeyedServices.HeloApiClientName, c =>
                {
                    c.BaseAddress = baseUri;
                    c.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");
                    c.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", SdkUserAgent.Value);
                    // RetryHandler times each attempt; HttpClient's 100s would cover all of them together.
                    if (timeout != System.Threading.Timeout.InfiniteTimeSpan)
                        c.Timeout = System.Threading.Timeout.InfiniteTimeSpan;
                })
                .AddHttpMessageHandler(() => new RetryHandler(maxRetries, timeout))
                .AddAsKeyed();
        }
    }
}
