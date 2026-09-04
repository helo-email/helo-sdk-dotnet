using HeloEmail.Sdk;
using HeloEmail.Sdk.Suppressions;
using Microsoft.Extensions.Logging.Abstractions;

namespace HeloEmail.Sdk.Tests.Suppressions;

public class SuppressionsTests : BaseFixture
{
    private static (SuppressionsClient Client, StubHandler Handler) CreateClient()
    {
        var (httpClient, handler) = CreateHttpClient();
        return (new SuppressionsClient(httpClient, NullLogger<SuppressionsClient>.Instance), handler);
    }

    [Fact]
    public async Task List_SendsExpectedRequest()
    {
        var (client, handler) = CreateClient();

        var result = await client.List("550e8400-e29b-41d4-a716-446655440000", MailType.Transactional);

        Assert.NotNull(result);
        Assert.Equal("GET", handler.Request!.Method.Method);
        Assert.Equal("/suppressions", handler.Request!.RequestUri!.AbsolutePath);
    }

    [Fact]
    public async Task Create_SendsExpectedRequest()
    {
        var (client, handler) = CreateClient();

        var result = await client.Create(new CreateSuppressionsRequest
        {
            ChannelId = "550e8400-e29b-41d4-a716-446655440000",
            MailType = MailType.Transactional,
            Emails = ["test@example.com"],
        });

        Assert.NotNull(result);
        Assert.Equal("POST", handler.Request!.Method.Method);
        Assert.Equal("/suppressions", handler.Request!.RequestUri!.AbsolutePath);
    }

    [Fact]
    public async Task Remove_SendsExpectedRequest()
    {
        var (client, handler) = CreateClient();

        var result = await client.Remove(new RemoveSuppressionsRequest
        {
            ChannelId = "550e8400-e29b-41d4-a716-446655440000",
            MailType = MailType.Transactional,
            Emails = ["test@example.com"],
        });

        Assert.NotNull(result);
        Assert.Equal("POST", handler.Request!.Method.Method);
        Assert.Equal("/suppressions/remove", handler.Request!.RequestUri!.AbsolutePath);
    }
}
