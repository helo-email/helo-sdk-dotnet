using HeloEmail.Sdk;
using HeloEmail.Sdk.Channels;
using Microsoft.Extensions.Logging.Abstractions;

namespace HeloEmail.Sdk.Tests.Channels;

public class ChannelsTests : BaseFixture
{
    private static (ChannelsClient Client, StubHandler Handler) CreateClient()
    {
        var (httpClient, handler) = CreateHttpClient();
        return (new ChannelsClient(httpClient, NullLogger<ChannelsClient>.Instance), handler);
    }

    [Fact]
    public async Task List_SendsExpectedRequest()
    {
        var (client, handler) = CreateClient();

        var result = await client.List();

        Assert.NotNull(result);
        Assert.Equal("GET", handler.Request!.Method.Method);
        Assert.Equal("/channels", handler.Request!.RequestUri!.AbsolutePath);
    }

    [Fact]
    public async Task Create_SendsExpectedRequest()
    {
        var (client, handler) = CreateClient();

        var result = await client.Create(new CreateChannelRequest
        {
            Name = "test-name",
            DeliveryType = DeliveryType.Live,
        });

        Assert.NotNull(result);
        Assert.Equal("POST", handler.Request!.Method.Method);
        Assert.Equal("/channels", handler.Request!.RequestUri!.AbsolutePath);
    }

    [Fact]
    public async Task Retrieve_SendsExpectedRequest()
    {
        var (client, handler) = CreateClient();

        var result = await client.Retrieve("550e8400-e29b-41d4-a716-446655440000");

        Assert.NotNull(result);
        Assert.Equal("GET", handler.Request!.Method.Method);
        Assert.Equal("/channels/550e8400-e29b-41d4-a716-446655440000", handler.Request!.RequestUri!.AbsolutePath);
    }

    [Fact]
    public async Task Update_SendsExpectedRequest()
    {
        var (client, handler) = CreateClient();

        var result = await client.Update("550e8400-e29b-41d4-a716-446655440000", new UpdateChannelRequest
        {
            Name = "test-name",
            DeliveryType = DeliveryType.Live,
        });

        Assert.NotNull(result);
        Assert.Equal("PATCH", handler.Request!.Method.Method);
        Assert.Equal("/channels/550e8400-e29b-41d4-a716-446655440000", handler.Request!.RequestUri!.AbsolutePath);
    }

    [Fact]
    public async Task Delete_SendsExpectedRequest()
    {
        var (client, handler) = CreateClient();

        await client.Delete("550e8400-e29b-41d4-a716-446655440000");

        Assert.Equal("DELETE", handler.Request!.Method.Method);
        Assert.Equal("/channels/550e8400-e29b-41d4-a716-446655440000", handler.Request!.RequestUri!.AbsolutePath);
    }
}
