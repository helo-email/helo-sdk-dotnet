using HeloEmail.Sdk;
using HeloEmail.Sdk.Sending;
using Microsoft.Extensions.Logging.Abstractions;

namespace HeloEmail.Sdk.Tests.Sending;

public class SendingTests : BaseFixture
{
    private static (SendingClient Client, StubHandler Handler) CreateClient()
    {
        var (httpClient, handler) = CreateHttpClient();
        return (new SendingClient(httpClient, NullLogger<SendingClient>.Instance), handler);
    }

    [Fact]
    public async Task Transactional_SendsExpectedRequest()
    {
        var (client, handler) = CreateClient();

        var result = await client.Transactional(new SendMessageRequest
        {
            From = new MailAddress { Email = "from@yourdomain.com", Name = "From name" },
            To = [new MailAddress { Email = "to@example.com", Name = "To name" }],
            Subject = "Hello from Helo",
            Html = "<html><body><h1>Hi there, new friend.</h1><p>This is a test message, delivered with <3 by Helo. </p></body></html>",
            Text = "This is a test message, delivered with <3 by Helo.",
            Tags = ["welcome", "onboarding"],
        }, channelId: "550e8400-e29b-41d4-a716-446655440000", idempotencyKey: "test-idempotencyKey");

        Assert.NotNull(result);
        Assert.Equal("POST", handler.Request!.Method.Method);
        Assert.Equal("/send/transactional", handler.Request!.RequestUri!.AbsolutePath);
        Assert.True(handler.Request!.Headers.Contains("X-Helo-Channel-Id"));
        Assert.True(handler.Request!.Headers.Contains("X-Helo-Idempotency-Key"));
    }

    [Fact]
    public async Task TransactionalBatch_SendsExpectedRequest()
    {
        var (client, handler) = CreateClient();

        var result = await client.TransactionalBatch(new SendMessageBatchRequest
        {
            Requests = [
                new SendMessageRequest
                {
                    From = new MailAddress { Email = "from@yourdomain.com", Name = "From name" },
                    To = [new MailAddress { Email = "to@example.com", Name = "To name" }],
                    Subject = "Hello from Helo",
                    Html = "<html><body><h1>Hi there, new friend.</h1><p>This is a test message, delivered with <3 by Helo. </p></body></html>",
                    Text = "This is a test message, delivered with <3 by Helo.",
                    Tags = ["welcome", "onboarding"],
                },
            ],
        }, channelId: "550e8400-e29b-41d4-a716-446655440000", idempotencyKey: "test-idempotencyKey");

        Assert.NotNull(result);
        Assert.Equal("POST", handler.Request!.Method.Method);
        Assert.Equal("/send/transactional/batch", handler.Request!.RequestUri!.AbsolutePath);
        Assert.True(handler.Request!.Headers.Contains("X-Helo-Channel-Id"));
        Assert.True(handler.Request!.Headers.Contains("X-Helo-Idempotency-Key"));
    }

    [Fact]
    public async Task Broadcast_SendsExpectedRequest()
    {
        var (client, handler) = CreateClient();

        var result = await client.Broadcast(new SendBroadcastRequest
        {
            From = new MailAddress { Email = "test@example.com", Name = "test-name" },
            Template = new SendBroadcastRequestTemplate
            {
                Subject = "test-subject",
                Html = "test-html",
                Text = "test-text",
                InlineStyles = true,
            },
            Tags = ["test-tag"],
            Messages = [
                new SendBroadcastRequestMessage
                {
                    To = [new MailAddress { Email = "test@example.com", Name = "test-name" }],
                    Tags = ["test-tag"],
                },
            ],
        }, channelId: "550e8400-e29b-41d4-a716-446655440000", idempotencyKey: "test-idempotencyKey");

        Assert.NotNull(result);
        Assert.Equal("POST", handler.Request!.Method.Method);
        Assert.Equal("/send/broadcast", handler.Request!.RequestUri!.AbsolutePath);
        Assert.True(handler.Request!.Headers.Contains("X-Helo-Channel-Id"));
        Assert.True(handler.Request!.Headers.Contains("X-Helo-Idempotency-Key"));
    }

    [Fact]
    public async Task BroadcastMessage_SendsExpectedRequest()
    {
        var (client, handler) = CreateClient();

        var result = await client.BroadcastMessage(new SendMessageRequest
        {
            From = new MailAddress { Email = "from@yourdomain.com", Name = "From name" },
            To = [new MailAddress { Email = "to@example.com", Name = "To name" }],
            Subject = "Hello from Helo",
            Html = "<html><body><h1>Hi there, new friend.</h1><p>This is a test message, delivered with <3 by Helo. </p></body></html>",
            Text = "This is a test message, delivered with <3 by Helo.",
            Tags = ["welcome", "onboarding"],
        }, channelId: "550e8400-e29b-41d4-a716-446655440000", idempotencyKey: "test-idempotencyKey");

        Assert.NotNull(result);
        Assert.Equal("POST", handler.Request!.Method.Method);
        Assert.Equal("/send/broadcast/message", handler.Request!.RequestUri!.AbsolutePath);
        Assert.True(handler.Request!.Headers.Contains("X-Helo-Channel-Id"));
        Assert.True(handler.Request!.Headers.Contains("X-Helo-Idempotency-Key"));
    }
}
