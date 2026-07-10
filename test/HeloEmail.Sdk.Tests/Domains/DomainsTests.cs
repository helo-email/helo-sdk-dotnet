using HeloEmail.Sdk.Domains;
using HeloEmail.Sdk.Errors;
using Meziantou.Extensions.Logging.Xunit.v3;

namespace HeloEmail.Sdk.Tests.Domains;

public class DomainsTests(ITestOutputHelper outputHelper) : BaseFixture
{
    private static DomainsClient CreateClient() =>
        new(HttpClient, XUnitLogger.CreateLogger<DomainsClient>());

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
            var result = await CreateClient().List(limit: 10, offset: 0);
            Assert.NotNull(result);
        }
        catch (ApiErrorException ex)
        {
            outputHelper.WriteLine(ex.ResponseContent);
            throw;
        }
    }

    [Fact]
    public async Task CreateAndRetrieve_DoesNotThrow()
    {
        var client = CreateClient();
        try
        {
            var created = await client.Create(new CreateDomainRequest
            {
                Name = $"test-{Guid.NewGuid():N}.example.com",
            });
            Assert.NotNull(created);
            Assert.NotNull(created.Id);

            var retrieved = await client.Retrieve(created.Id);
            Assert.Equal(created.Id, retrieved.Id);
        }
        catch (ApiErrorException ex)
        {
            outputHelper.WriteLine(ex.ResponseContent);
            throw;
        }
    }
}
