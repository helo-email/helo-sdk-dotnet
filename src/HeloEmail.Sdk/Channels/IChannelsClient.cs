using System.Collections.Generic;
using System.Threading.Tasks;

namespace HeloEmail.Sdk.Channels
{
    public interface IChannelsClient
    {
        /// <summary>
        /// List all channels
        /// </summary>
        Task<PaginationResultOfChannelBasicResponse> List(
            int? limit = null,
            int? offset = null,
            string name = null,
            IEnumerable<string> channelIds = null,
            DeliveryType? deliveryType = null);

        /// <summary>
        /// Create a channel
        /// </summary>
        Task<ChannelDetailsResponse> Create(CreateChannelRequest request);

        /// <summary>
        /// Retrieve a channel
        /// </summary>
        Task<ChannelDetailsResponse> Retrieve(string id);

        /// <summary>
        /// Update a channel
        /// </summary>
        Task<ChannelDetailsResponse> Update(string id, UpdateChannelRequest request);

        /// <summary>
        /// Delete a channel
        /// </summary>
        Task Delete(string id);
    }
}
