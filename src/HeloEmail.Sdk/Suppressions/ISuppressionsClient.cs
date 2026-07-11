using System.Threading.Tasks;
using HeloEmail.Sdk.Activity;

namespace HeloEmail.Sdk.Suppressions
{
    public interface ISuppressionsClient
    {
        Task<PaginatedResponseOfSuppressionResponse> List(string channelId, MailType mailType, SuppressionReason? reason = null, string email = null, int? limit = null, int? offset = null);
        Task<CreateSuppressionsResponse> Create(CreateSuppressionsRequest request);
        Task<RemoveSuppressionsResponse> Remove(RemoveSuppressionsRequest request);
    }
}
