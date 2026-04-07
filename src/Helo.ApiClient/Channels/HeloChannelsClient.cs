using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Helo.ApiClient.Channels
{
    public class HeloChannelsClient : HeloBaseClient, IHeloChannelsClient
    {
        public HeloChannelsClient([FromKeyedServices(KeyedServices.HeloApiClientName)] HttpClient httpClient,
            ILogger<HeloChannelsClient> logger) :
            base(httpClient, logger)
        {
        }

        public Task<ChannelDetailsResponse> Create(CreateChannelRequest request) =>
            Post<CreateChannelRequest, ChannelDetailsResponse>("/channels", request);

        public Task<PaginationResultOfChannelBasicResponse> List(int? limit = null, int? offset = null,
            string name = null, IEnumerable<string> channelIds = null, DeliveryType? deliveryType = null)
        {
            var query = new List<(string, string)>
            {
                ("limit", limit?.ToString()),
                ("offset", offset?.ToString()),
                ("name", name),
                ("deliveryType", deliveryType?.ToString().ToLower()),
            };
            if (channelIds != null)
                query.AddRange(channelIds.Select(id => ("channelIds", id)));
            return Get<PaginationResultOfChannelBasicResponse>(BuildUrl("/channels", query));
        }

        public Task<ChannelDetailsResponse> Retrieve(string id) =>
            Get<ChannelDetailsResponse>($"/channels/{Uri.EscapeDataString(id)}");

        public Task<ChannelDetailsResponse> Update(string id, UpdateChannelRequest request) =>
            Patch<UpdateChannelRequest, ChannelDetailsResponse>($"/channels/{Uri.EscapeDataString(id)}", request);

        public new Task Delete(string id) =>
            base.Delete($"/channels/{Uri.EscapeDataString(id)}");

        private static string BuildUrl(string path, List<(string Key, string Value)> parameters)
        {
            var sb = new StringBuilder(path);
            var first = true;
            foreach (var (key, value) in parameters)
            {
                if (value == null) continue;
                sb.Append(first ? '?' : '&');
                sb.Append($"{Uri.EscapeDataString(key)}={Uri.EscapeDataString(value)}");
                first = false;
            }
            return sb.ToString();
        }
    }
}
