namespace HeloEmail.Sdk.Tests;

public class BaseFixture
{
    protected static readonly HttpClient HttpClient = new()
    {
        BaseAddress = new Uri(Environment.GetEnvironmentVariable("HeloUrl") ?? "http://localhost:8000"),
        DefaultRequestHeaders = { { "Authorization", $"Bearer {Environment.GetEnvironmentVariable("HeloApiKey")}" } }
    };
}