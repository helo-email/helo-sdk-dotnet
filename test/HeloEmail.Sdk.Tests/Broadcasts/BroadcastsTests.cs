using HeloEmail.Sdk.Broadcasts;
using Microsoft.Extensions.Logging.Abstractions;

namespace HeloEmail.Sdk.Tests.Broadcasts;

public class BroadcastsTests : BaseFixture
{
    private static (BroadcastsClient Client, StubHandler Handler) CreateClient()
    {
        var (httpClient, handler) = CreateHttpClient();
        return (new BroadcastsClient(httpClient, NullLogger<BroadcastsClient>.Instance), handler);
    }

    [Fact]
    public async Task List_SendsExpectedRequest()
    {
        var (client, handler) = CreateClient();

        var result = await client.List("550e8400-e29b-41d4-a716-446655440000");

        Assert.NotNull(result);
        Assert.Equal("GET", handler.Request!.Method.Method);
        Assert.Equal("/broadcasts", handler.Request!.RequestUri!.AbsolutePath);
    }

    [Fact]
    public async Task Retrieve_SendsExpectedRequest()
    {
        var (client, handler) = CreateClient();

        var result = await client.Retrieve("550e8400-e29b-41d4-a716-446655440000");

        Assert.NotNull(result);
        Assert.Equal("GET", handler.Request!.Method.Method);
        Assert.Equal("/broadcasts/550e8400-e29b-41d4-a716-446655440000", handler.Request!.RequestUri!.AbsolutePath);
    }

    [Fact]
    public async Task ListFailures_SendsExpectedRequest()
    {
        var (client, handler) = CreateClient();

        var result = await client.ListFailures("550e8400-e29b-41d4-a716-446655440000");

        Assert.NotNull(result);
        Assert.Equal("GET", handler.Request!.Method.Method);
        Assert.Equal("/broadcasts/550e8400-e29b-41d4-a716-446655440000/failures", handler.Request!.RequestUri!.AbsolutePath);
    }

    [Fact]
    public async Task ListSuppressions_SendsExpectedRequest()
    {
        var (client, handler) = CreateClient();

        var result = await client.ListSuppressions("550e8400-e29b-41d4-a716-446655440000");

        Assert.NotNull(result);
        Assert.Equal("GET", handler.Request!.Method.Method);
        Assert.Equal("/broadcasts/550e8400-e29b-41d4-a716-446655440000/suppressions", handler.Request!.RequestUri!.AbsolutePath);
    }
}
