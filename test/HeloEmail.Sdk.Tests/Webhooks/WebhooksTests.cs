using HeloEmail.Sdk.Webhooks;
using Microsoft.Extensions.Logging.Abstractions;

namespace HeloEmail.Sdk.Tests.Webhooks;

public class WebhooksTests : BaseFixture
{
    private static (WebhooksClient Client, StubHandler Handler) CreateClient()
    {
        var (httpClient, handler) = CreateHttpClient();
        return (new WebhooksClient(httpClient, NullLogger<WebhooksClient>.Instance), handler);
    }

    [Fact]
    public async Task List_SendsExpectedRequest()
    {
        var (client, handler) = CreateClient();

        var result = await client.List();

        Assert.NotNull(result);
        Assert.Equal("GET", handler.Request!.Method.Method);
        Assert.Equal("/webhooks", handler.Request!.RequestUri!.AbsolutePath);
    }

    [Fact]
    public async Task Create_SendsExpectedRequest()
    {
        var (client, handler) = CreateClient();

        var result = await client.Create(new CreateWebhookRequest
        {
            Url = "test-url",
            Events = [WebhookEvent.Accepted],
            ChannelId = "550e8400-e29b-41d4-a716-446655440000",
            Enabled = true,
        });

        Assert.NotNull(result);
        Assert.Equal("POST", handler.Request!.Method.Method);
        Assert.Equal("/webhooks", handler.Request!.RequestUri!.AbsolutePath);
    }

    [Fact]
    public async Task Retrieve_SendsExpectedRequest()
    {
        var (client, handler) = CreateClient();

        var result = await client.Retrieve("550e8400-e29b-41d4-a716-446655440000");

        Assert.NotNull(result);
        Assert.Equal("GET", handler.Request!.Method.Method);
        Assert.Equal("/webhooks/550e8400-e29b-41d4-a716-446655440000", handler.Request!.RequestUri!.AbsolutePath);
    }

    [Fact]
    public async Task Update_SendsExpectedRequest()
    {
        var (client, handler) = CreateClient();

        var result = await client.Update("550e8400-e29b-41d4-a716-446655440000", new UpdateWebhookRequest
        {
            Url = "test-url",
            Events = [WebhookEvent.Accepted],
            ChannelId = "550e8400-e29b-41d4-a716-446655440000",
            Enabled = true,
        });

        Assert.NotNull(result);
        Assert.Equal("PATCH", handler.Request!.Method.Method);
        Assert.Equal("/webhooks/550e8400-e29b-41d4-a716-446655440000", handler.Request!.RequestUri!.AbsolutePath);
    }

    [Fact]
    public async Task Delete_SendsExpectedRequest()
    {
        var (client, handler) = CreateClient();

        await client.Delete("550e8400-e29b-41d4-a716-446655440000");

        Assert.Equal("DELETE", handler.Request!.Method.Method);
        Assert.Equal("/webhooks/550e8400-e29b-41d4-a716-446655440000", handler.Request!.RequestUri!.AbsolutePath);
    }

    [Fact]
    public async Task RegenerateSigningKey_SendsExpectedRequest()
    {
        var (client, handler) = CreateClient();

        var result = await client.RegenerateSigningKey("550e8400-e29b-41d4-a716-446655440000");

        Assert.NotNull(result);
        Assert.Equal("POST", handler.Request!.Method.Method);
        Assert.Equal("/webhooks/550e8400-e29b-41d4-a716-446655440000/regenerate-signing-key", handler.Request!.RequestUri!.AbsolutePath);
    }
}
