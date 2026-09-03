using System.Threading.Tasks;

namespace HeloEmail.Sdk.Sending
{
    public interface ISendingClient
    {
        /// <summary>
        /// Send a transactional email
        /// </summary>
        Task<SendMessageAcceptedResponse> Transactional(
            SendMessageRequest request,
            string channelId = null,
            string idempotencyKey = null);

        /// <summary>
        /// Send transactional emails in batch
        /// </summary>
        Task<SendMessageBatchResponse> TransactionalBatch(
            SendMessageBatchRequest request,
            string channelId = null,
            string idempotencyKey = null);

        /// <summary>
        /// Send a broadcast email
        /// </summary>
        Task<SendBroadcastResponse> Broadcast(
            SendBroadcastRequest request,
            string channelId = null,
            string idempotencyKey = null);

        /// <summary>
        /// Send a single broadcast email
        /// </summary>
        Task<SendMessageAcceptedResponse> BroadcastMessage(
            SendMessageRequest request,
            string channelId = null,
            string idempotencyKey = null);
    }
}
