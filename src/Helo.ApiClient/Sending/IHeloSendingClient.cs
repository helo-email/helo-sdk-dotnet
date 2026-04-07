using System.Threading.Tasks;

namespace Helo.ApiClient.Sending
{
    public interface IHeloSendingClient
    {
        Task<SendMessageAcceptedResponse> Transactional(SendMessageRequest request, string channelId = null, string idempotencyKey = null);
        Task<SendMessageBatchResponse> TransactionalBatch(SendMessageBatchRequest request, string channelId = null, string idempotencyKey = null);
        Task<SendBroadcastResponse> Broadcast(SendBroadcastRequest request, string channelId = null, string idempotencyKey = null);
        Task<SendMessageAcceptedResponse> BroadcastMessage(SendMessageRequest request, string channelId = null, string idempotencyKey = null);
    }
}
