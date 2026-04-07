using Helo.ApiClient.Errors;
using Helo.ApiClient.Sending;
using Meziantou.Extensions.Logging.Xunit.v3;

namespace Helo.ApiClient.Tests.Sending;

public class SendingTests(ITestOutputHelper outputHelper) : BaseFixture
{
    private static readonly MailAddress TestAddress = new() { Email = "testing@helohq.com", Name = "Test" };

    private static SendingClient CreateClient() =>
        new(HttpClient, XUnitLogger.CreateLogger<SendingClient>());

    [Fact]
    public async Task SendTransactional_DoesNotThrow()
    {
        try
        {
            await CreateClient().Transactional(new SendMessageRequest
            {
                From = TestAddress,
                To = [TestAddress],
                Subject = "Test",
                Html = "<h1>Test</h1>",
            }, channelId: "241efbe3-3e50-4192-ab69-f8c9ccb10ae1");
        }
        catch (ApiErrorException ex)
        {
            outputHelper.WriteLine(ex.ResponseContent);
            throw;
        }
    }

    [Fact]
    public async Task SendTransactional_WithOptionalFields_DoesNotThrow()
    {
        try
        {
            await CreateClient().Transactional(new SendMessageRequest
            {
                From = TestAddress,
                To = [TestAddress],
                Cc = [TestAddress],
                ReplyTo = [TestAddress],
                Subject = "Test with optional fields",
                Html = "<h1>Test</h1>",
                Text = "Test",
                Tags = ["tag1", "tag2"],
            }, channelId: "241efbe3-3e50-4192-ab69-f8c9ccb10ae1");
        }
        catch (ApiErrorException ex)
        {
            outputHelper.WriteLine(ex.ResponseContent);
            throw;
        }
    }

    [Fact]
    public async Task SendTransactional_WithIdempotencyKey_DoesNotThrow()
    {
        try
        {
            await CreateClient().Transactional(new SendMessageRequest
            {
                From = TestAddress,
                To = [TestAddress],
                Subject = "Test with idempotency key",
                Html = "<h1>Test</h1>",
            }, idempotencyKey: Guid.NewGuid().ToString(), channelId: "241efbe3-3e50-4192-ab69-f8c9ccb10ae1");
        }
        catch (ApiErrorException ex)
        {
            outputHelper.WriteLine(ex.ResponseContent);
            throw;
        }
    }

    [Fact]
    public async Task SendTransactionalBatch_DoesNotThrow()
    {
        try
        {
            await CreateClient().TransactionalBatch(new SendMessageBatchRequest
            {
                Requests =
                [
                    new SendMessageRequest
                    {
                        From = TestAddress,
                        To = [TestAddress],
                        Subject = "Batch message 1",
                        Html = "<h1>Batch 1</h1>",
                    },
                    new SendMessageRequest
                    {
                        From = TestAddress,
                        To = [TestAddress],
                        Subject = "Batch message 2",
                        Html = "<h1>Batch 2</h1>",
                    },
                ],
            }, channelId: "241efbe3-3e50-4192-ab69-f8c9ccb10ae1");
        }
        catch (ApiErrorException ex)
        {
            outputHelper.WriteLine(ex.ResponseContent);
            throw;
        }
    }

    [Fact]
    public async Task SendBroadcast_DoesNotThrow()
    {
        try
        {
            await CreateClient().Broadcast(new SendBroadcastRequest
            {
                From = TestAddress,
                Messages =
                [
                    new BroadcastMessage { To = [TestAddress] },
                ],
                Template = new MessageTemplate
                {
                    Subject = "Broadcast test",
                    Html = "<h1>Broadcast</h1>",
                },
            }, channelId: "241efbe3-3e50-4192-ab69-f8c9ccb10ae1");
        }
        catch (ApiErrorException ex)
        {
            outputHelper.WriteLine(ex.ResponseContent);
            throw;
        }
    }

    [Fact]
    public async Task SendBroadcastMessage_DoesNotThrow()
    {
        try
        {
            await CreateClient().BroadcastMessage(new SendMessageRequest
            {
                From = TestAddress,
                To = [TestAddress],
                Subject = "Broadcast message test",
                Html = "<h1>Broadcast message</h1>",
            }, channelId: "241efbe3-3e50-4192-ab69-f8c9ccb10ae1");
        }
        catch (ApiErrorException ex)
        {
            outputHelper.WriteLine(ex.ResponseContent);
            throw;
        }
    }
}