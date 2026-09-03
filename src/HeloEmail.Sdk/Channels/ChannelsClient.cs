using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace HeloEmail.Sdk.Channels
{
    public class ChannelsClient : BaseClient, IChannelsClient
    {
        public ChannelsClient([FromKeyedServices(KeyedServices.HeloApiClientName)] HttpClient httpClient,
            ILogger<ChannelsClient> logger) :
            base(httpClient, logger)
        {
        }

        /// <summary>
        /// List all channels
        /// </summary>
        public Task<PaginationResultOfChannelBasicResponse> List(
            int? limit = null,
            int? offset = null,
            string name = null,
            IEnumerable<string> channelIds = null,
            DeliveryType? deliveryType = null)
        {
            var query = new List<(string, string)>
            {
                ("limit", limit?.ToString(CultureInfo.InvariantCulture)),
                ("offset", offset?.ToString(CultureInfo.InvariantCulture)),
                ("name", name),
                ("deliveryType", ToQueryValue(deliveryType)),
            };

            if (channelIds != null)
                query.AddRange(channelIds.Select(x => ("channelIds", x)));

            return Get<PaginationResultOfChannelBasicResponse>(BuildUrl("/channels", query));
        }

        /// <summary>
        /// Create a channel
        /// </summary>
        public Task<ChannelDetailsResponse> Create(CreateChannelRequest request) =>
            Post<CreateChannelRequest, ChannelDetailsResponse>("/channels", request);

        /// <summary>
        /// Retrieve a channel
        /// </summary>
        public Task<ChannelDetailsResponse> Retrieve(string id) =>
            Get<ChannelDetailsResponse>($"/channels/{Uri.EscapeDataString(id)}");

        /// <summary>
        /// Update a channel
        /// </summary>
        public Task<ChannelDetailsResponse> Update(string id, UpdateChannelRequest request) =>
            Patch<UpdateChannelRequest, ChannelDetailsResponse>($"/channels/{Uri.EscapeDataString(id)}", request);

        /// <summary>
        /// Delete a channel
        /// </summary>
        public new Task Delete(string id) =>
            base.Delete($"/channels/{Uri.EscapeDataString(id)}");
    }
}
