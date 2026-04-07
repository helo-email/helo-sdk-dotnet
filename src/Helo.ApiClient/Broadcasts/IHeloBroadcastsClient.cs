using System.Threading.Tasks;

namespace Helo.ApiClient.Broadcasts
{
    public interface IHeloBroadcastsClient
    {
        Task<PaginatedResponseOfBroadcast> List(string channelId, BroadcastStatus? status = null, string subject = null, int? limit = null, int? offset = null);
        Task<BroadcastDetailsResponse> Retrieve(string id);
        Task<PaginatedResponseOfBroadcastFailure> ListFailures(string id);
        Task<PaginatedResponseOfBroadcastSuppression> ListSuppressions(string id);
    }
}
