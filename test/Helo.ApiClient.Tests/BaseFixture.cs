namespace Helo.ApiClient.Tests;

public class BaseFixture
{
    protected static readonly HttpClient HttpClient = new()
    {
        BaseAddress = new Uri("http://localhost:8000"),
        DefaultRequestHeaders = { { "Authorization", $"Bearer {Settings.DevApiKey}" } }
    };
}