using HeloEmail.Sdk.Broadcasts;
using HeloEmail.Sdk.Errors;
using Meziantou.Extensions.Logging.Xunit.v3;

namespace HeloEmail.Sdk.Tests.Broadcasts;

public class BroadcastsTests(ITestOutputHelper outputHelper) : BaseFixture
{
    private static BroadcastsClient CreateClient() =>
        new(HttpClient, XUnitLogger.CreateLogger<BroadcastsClient>());

    [Fact]
    public async Task List_DoesNotThrow()
    {
        try
        {
            var result = await CreateClient().List(channelId: "241efbe3-3e50-4192-ab69-f8c9ccb10ae1");
            Assert.NotNull(result);
        }
        catch (ApiErrorException ex)
        {
            outputHelper.WriteLine(ex.ResponseContent);
            throw;
        }
    }

    [Fact]
    public async Task List_WithFilters_DoesNotThrow()
    {
        try
        {
            var result = await CreateClient().List(channelId: "241efbe3-3e50-4192-ab69-f8c9ccb10ae1", limit: 10, offset: 0);
            Assert.NotNull(result);
        }
        catch (ApiErrorException ex)
        {
            outputHelper.WriteLine(ex.ResponseContent);
            throw;
        }
    }
}
