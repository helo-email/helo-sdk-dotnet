using System.Threading.Tasks;

namespace HeloEmail.Sdk.Broadcasts
{
    public interface IBroadcastsClient
    {
        /// <summary>
        /// List broadcasts
        /// </summary>
        Task<PaginatedResponseOfBroadcast> List(
            string channelId,
            BroadcastStatus? status = null,
            string subject = null,
            int? limit = null,
            int? offset = null);

        /// <summary>
        /// Retrieve a broadcast
        /// </summary>
        Task<BroadcastDetailsResponse> Retrieve(string id);

        /// <summary>
        /// List failed broadcast messages
        /// </summary>
        Task<PaginatedResponseOfBroadcastFailure> ListFailures(string id, int? limit = null, int? offset = null);

        /// <summary>
        /// List broadcast suppressed recipients
        /// </summary>
        Task<PaginatedResponseOfBroadcastSuppression> ListSuppressions(
            string id,
            int? limit = null,
            int? offset = null);
    }
}
