namespace HeloEmail.Sdk.Tests;

public class BaseFixture
{
    protected const string BaseAddress = "https://api.example.test";

    protected static (HttpClient HttpClient, StubHandler Handler) CreateHttpClient()
    {
        var handler = new StubHandler();
        var httpClient = new HttpClient(handler) { BaseAddress = new Uri(BaseAddress) };
        return (httpClient, handler);
    }
}
