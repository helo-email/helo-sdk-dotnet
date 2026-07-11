using HeloEmail.Sdk.Activity;
using HeloEmail.Sdk.Errors;
using HeloEmail.Sdk.Suppressions;
using Meziantou.Extensions.Logging.Xunit.v3;

namespace HeloEmail.Sdk.Tests.Suppressions;

public class SuppressionsTests(ITestOutputHelper outputHelper) : BaseFixture
{
    private const string ChannelId = "241efbe3-3e50-4192-ab69-f8c9ccb10ae1";

    private static SuppressionsClient CreateClient() =>
        new(HttpClient, XUnitLogger.CreateLogger<SuppressionsClient>());

    [Fact]
    public async Task List_DoesNotThrow()
    {
        try
        {
            var result = await CreateClient().List(ChannelId, MailType.Transactional);
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
            var result = await CreateClient().List(ChannelId, MailType.Transactional,
                reason: SuppressionReason.Bounce, limit: 10, offset: 0);
            Assert.NotNull(result);
        }
        catch (ApiErrorException ex)
        {
            outputHelper.WriteLine(ex.ResponseContent);
            throw;
        }
    }

    [Fact]
    public async Task CreateRemove_DoesNotThrow()
    {
        var client = CreateClient();
        var email = $"suppressed-{Guid.NewGuid():N}@example.com";
        try
        {
            var created = await client.Create(new CreateSuppressionsRequest
            {
                ChannelId = ChannelId,
                MailType = MailType.Transactional,
                Emails = [email],
            });
            Assert.NotNull(created);
            Assert.NotNull(created.Results);

            var removed = await client.Remove(new RemoveSuppressionsRequest
            {
                ChannelId = ChannelId,
                MailType = MailType.Transactional,
                Emails = [email],
            });
            Assert.NotNull(removed);
            Assert.NotNull(removed.Results);
        }
        catch (ApiErrorException ex)
        {
            outputHelper.WriteLine(ex.ResponseContent);
            throw;
        }
    }
}
