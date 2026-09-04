using HeloEmail.Sdk.Domains;
using Microsoft.Extensions.Logging.Abstractions;

namespace HeloEmail.Sdk.Tests.Domains;

public class DomainsTests : BaseFixture
{
    private static (DomainsClient Client, StubHandler Handler) CreateClient()
    {
        var (httpClient, handler) = CreateHttpClient();
        return (new DomainsClient(httpClient, NullLogger<DomainsClient>.Instance), handler);
    }

    [Fact]
    public async Task List_SendsExpectedRequest()
    {
        var (client, handler) = CreateClient();

        var result = await client.List();

        Assert.NotNull(result);
        Assert.Equal("GET", handler.Request!.Method.Method);
        Assert.Equal("/domains", handler.Request!.RequestUri!.AbsolutePath);
    }

    [Fact]
    public async Task Create_SendsExpectedRequest()
    {
        var (client, handler) = CreateClient();

        var result = await client.Create(new CreateDomainRequest
        {
            Name = "test-name",
            ChannelIds = ["550e8400-e29b-41d4-a716-446655440000"],
        });

        Assert.NotNull(result);
        Assert.Equal("POST", handler.Request!.Method.Method);
        Assert.Equal("/domains", handler.Request!.RequestUri!.AbsolutePath);
    }

    [Fact]
    public async Task Retrieve_SendsExpectedRequest()
    {
        var (client, handler) = CreateClient();

        var result = await client.Retrieve("550e8400-e29b-41d4-a716-446655440000");

        Assert.NotNull(result);
        Assert.Equal("GET", handler.Request!.Method.Method);
        Assert.Equal("/domains/550e8400-e29b-41d4-a716-446655440000", handler.Request!.RequestUri!.AbsolutePath);
    }

    [Fact]
    public async Task Update_SendsExpectedRequest()
    {
        var (client, handler) = CreateClient();

        var result = await client.Update("550e8400-e29b-41d4-a716-446655440000", new UpdateDomainRequest
        {
            ChannelIds = ["550e8400-e29b-41d4-a716-446655440000"],
        });

        Assert.NotNull(result);
        Assert.Equal("PATCH", handler.Request!.Method.Method);
        Assert.Equal("/domains/550e8400-e29b-41d4-a716-446655440000", handler.Request!.RequestUri!.AbsolutePath);
    }

    [Fact]
    public async Task Delete_SendsExpectedRequest()
    {
        var (client, handler) = CreateClient();

        await client.Delete("550e8400-e29b-41d4-a716-446655440000");

        Assert.Equal("DELETE", handler.Request!.Method.Method);
        Assert.Equal("/domains/550e8400-e29b-41d4-a716-446655440000", handler.Request!.RequestUri!.AbsolutePath);
    }

    [Fact]
    public async Task Verify_SendsExpectedRequest()
    {
        var (client, handler) = CreateClient();

        var result = await client.Verify("550e8400-e29b-41d4-a716-446655440000");

        Assert.NotNull(result);
        Assert.Equal("POST", handler.Request!.Method.Method);
        Assert.Equal("/domains/550e8400-e29b-41d4-a716-446655440000/verify", handler.Request!.RequestUri!.AbsolutePath);
    }

    [Fact]
    public async Task RotateKey_SendsExpectedRequest()
    {
        var (client, handler) = CreateClient();

        var result = await client.RotateKey("550e8400-e29b-41d4-a716-446655440000");

        Assert.NotNull(result);
        Assert.Equal("POST", handler.Request!.Method.Method);
        Assert.Equal("/domains/550e8400-e29b-41d4-a716-446655440000/rotate-key", handler.Request!.RequestUri!.AbsolutePath);
    }
}
