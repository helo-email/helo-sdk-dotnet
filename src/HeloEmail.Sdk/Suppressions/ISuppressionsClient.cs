using System.Threading.Tasks;

namespace HeloEmail.Sdk.Suppressions
{
    public interface ISuppressionsClient
    {
        /// <summary>
        /// List suppressions
        /// </summary>
        Task<PaginatedResponseOfSuppressionResponse> List(
            string channelId,
            MailType mailType,
            SuppressionReason? reason = null,
            string email = null,
            int? limit = null,
            int? offset = null);

        /// <summary>
        /// Create suppressions
        /// </summary>
        Task<CreateSuppressionsResponse> Create(CreateSuppressionsRequest request);

        /// <summary>
        /// Remove suppressions
        /// </summary>
        Task<RemoveSuppressionsResponse> Remove(RemoveSuppressionsRequest request);
    }
}
