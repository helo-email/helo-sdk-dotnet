using HeloEmail.Sdk;
using HeloEmail.Sdk.Activity;
using Microsoft.Extensions.Logging.Abstractions;

namespace HeloEmail.Sdk.Tests.Activity;

public class ActivityTests : BaseFixture
{
    private static (ActivityClient Client, StubHandler Handler) CreateClient()
    {
        var (httpClient, handler) = CreateHttpClient();
        return (new ActivityClient(httpClient, NullLogger<ActivityClient>.Instance), handler);
    }

    [Fact]
    public async Task ListEvents_SendsExpectedRequest()
    {
        var (client, handler) = CreateClient();

        var result = await client.ListEvents();

        Assert.NotNull(result);
        Assert.Equal("GET", handler.Request!.Method.Method);
        Assert.Equal("/activity/events", handler.Request!.RequestUri!.AbsolutePath);
    }

    [Fact]
    public async Task ListMessages_SendsExpectedRequest()
    {
        var (client, handler) = CreateClient();

        var result = await client.ListMessages();

        Assert.NotNull(result);
        Assert.Equal("GET", handler.Request!.Method.Method);
        Assert.Equal("/activity/messages", handler.Request!.RequestUri!.AbsolutePath);
    }

    [Fact]
    public async Task RetrieveMessage_SendsExpectedRequest()
    {
        var (client, handler) = CreateClient();

        var result = await client.RetrieveMessage("550e8400-e29b-41d4-a716-446655440000");

        Assert.NotNull(result);
        Assert.Equal("GET", handler.Request!.Method.Method);
        Assert.Equal("/activity/messages/550e8400-e29b-41d4-a716-446655440000", handler.Request!.RequestUri!.AbsolutePath);
    }
}
