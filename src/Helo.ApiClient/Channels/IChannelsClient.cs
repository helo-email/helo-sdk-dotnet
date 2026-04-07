using System.Collections.Generic;
using System.Threading.Tasks;

namespace Helo.ApiClient.Channels
{
    public interface IChannelsClient
    {
        Task<ChannelDetailsResponse> Create(CreateChannelRequest request);
        Task<PaginationResultOfChannelBasicResponse> List(int? limit = null, int? offset = null, string name = null, IEnumerable<string> channelIds = null, DeliveryType? deliveryType = null);
        Task<ChannelDetailsResponse> Retrieve(string id);
        Task<ChannelDetailsResponse> Update(string id, UpdateChannelRequest request);
        Task Delete(string id);
    }
}
