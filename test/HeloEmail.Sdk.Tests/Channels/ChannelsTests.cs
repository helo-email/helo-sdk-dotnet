using HeloEmail.Sdk.Channels;
using HeloEmail.Sdk.Errors;
using Meziantou.Extensions.Logging.Xunit.v3;

namespace HeloEmail.Sdk.Tests.Channels;

public class ChannelsTests(ITestOutputHelper outputHelper) : BaseFixture
{
    private static ChannelsClient CreateClient() =>
        new(HttpClient, XUnitLogger.CreateLogger<ChannelsClient>());

    [Fact]
    public async Task List_DoesNotThrow()
    {
        try
        {
            var result = await CreateClient().List();
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
            var result = await CreateClient().List(limit: 10, offset: 0, deliveryType: DeliveryType.Live);
            Assert.NotNull(result);
        }
        catch (ApiErrorException ex)
        {
            outputHelper.WriteLine(ex.ResponseContent);
            throw;
        }
    }

    [Fact]
    public async Task CreateRetrieveUpdateDelete_DoesNotThrow()
    {
        var client = CreateClient();
        ChannelDetailsResponse? created = null;
        try
        {
            created = await client.Create(new CreateChannelRequest
            {
                Name = $"test-channel-{Guid.NewGuid():N}",
                DeliveryType = DeliveryType.Live,
            });
            Assert.NotNull(created);
            Assert.NotNull(created.Id);

            var retrieved = await client.Retrieve(created.Id);
            Assert.Equal(created.Id, retrieved.Id);

            var updated = await client.Update(created.Id, new UpdateChannelRequest
            {
                Name = $"test-channel-updated-{Guid.NewGuid():N}",
            });
            Assert.Equal(created.Id, updated.Id);
        }
        catch (ApiErrorException ex)
        {
            outputHelper.WriteLine(ex.ResponseContent);
            throw;
        }
        finally
        {
            if (created != null)
                await client.Delete(created.Id);
        }
    }
}
