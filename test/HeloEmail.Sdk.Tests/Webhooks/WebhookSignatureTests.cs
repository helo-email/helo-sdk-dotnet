using System.Security.Cryptography;
using System.Text;
using HeloEmail.Sdk.Webhooks;

namespace HeloEmail.Sdk.Tests.Webhooks;

public class WebhookSignatureTests
{
    private const string SigningKey = "whsec_test";
    private const string Body = """{"event":"message.delivered"}""";

    [Fact]
    public void Verify_AcceptsAValidSignature()
    {
        var header = ValidHeader();

        WebhookSignature.Verify(header, Body, SigningKey);

        Assert.True(WebhookSignature.IsValid(header, Body, SigningKey));
        Assert.True(WebhookSignature.IsValid(header, Encoding.UTF8.GetBytes(Body), SigningKey));
    }

    // A sender rolling out a new signing scheme emits every version at once. This SDK must
    // keep verifying the versions it knows and ignore the rest, otherwise the rollout breaks
    // every receiver that has not upgraded yet.
    [Theory]
    [InlineData("t={0},v1={1},v2=8badf00d")]
    [InlineData("t={0},v2=8badf00d,v1={1}")]
    [InlineData("t={0},v1={1},alg=sha512")]
    [InlineData("t={0}, v1={1}")]
    [InlineData("v1={1},t={0}")]
    [InlineData("t={0},v1=8badf00d,v1={1}")]
    public void Verify_IgnoresUnknownVersionsAndElements(string headerFormat)
    {
        var timestamp = Now();
        var header = string.Format(headerFormat, timestamp,
            WebhookSignature.Generate(Body, SigningKey, timestamp));

        Assert.True(WebhookSignature.IsValid(header, Body, SigningKey));
    }

    [Fact]
    public void Verify_RejectsASignatureFromADifferentKey()
    {
        var header = ValidHeader();

        var error = Assert.Throws<WebhookSignatureException>(
            () => WebhookSignature.Verify(header, Body, "wrong-key"));

        Assert.Equal(WebhookSignatureError.SignatureMismatch, error.Error);
        Assert.False(WebhookSignature.IsValid(header, Body, "wrong-key"));
    }

    [Fact]
    public void Verify_RejectsATamperedBody()
    {
        var error = Assert.Throws<WebhookSignatureException>(
            () => WebhookSignature.Verify(ValidHeader(), """{"event":"message.bounced"}""", SigningKey));

        Assert.Equal(WebhookSignatureError.SignatureMismatch, error.Error);
    }

    [Fact]
    public void Verify_RejectsAStaleTimestamp()
    {
        var timestamp = (DateTimeOffset.UtcNow.ToUnixTimeSeconds() - 600).ToString();
        var header = $"t={timestamp},v1={WebhookSignature.Generate(Body, SigningKey, timestamp)}";

        var error = Assert.Throws<WebhookSignatureException>(
            () => WebhookSignature.Verify(header, Body, SigningKey));

        Assert.Equal(WebhookSignatureError.TimestampSkew, error.Error);
        Assert.Contains("tolerance", error.Message);
    }

    [Fact]
    public void Verify_RejectsAHeaderCarryingOnlyUnknownVersions()
    {
        var timestamp = Now();
        var header = $"t={timestamp},v2={WebhookSignature.Generate(Body, SigningKey, timestamp)}";

        var error = Assert.Throws<WebhookSignatureException>(
            () => WebhookSignature.Verify(header, Body, SigningKey));

        Assert.Equal(WebhookSignatureError.UnsupportedVersion, error.Error);
        Assert.Contains("v2", error.Message);
    }

    [Theory]
    [InlineData("garbage")]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("t={0},v1=ABCDEF")]
    [InlineData("t={0}")]
    [InlineData("v1={1}")]
    [InlineData("t=yesterday,v1={1}")]
    public void Verify_RejectsMalformedHeaders(string? headerFormat)
    {
        var timestamp = Now();
        var header = headerFormat == null
            ? null
            : string.Format(headerFormat, timestamp, WebhookSignature.Generate(Body, SigningKey, timestamp));

        var error = Assert.Throws<WebhookSignatureException>(
            () => WebhookSignature.Verify(header!, Body, SigningKey));

        Assert.Equal(WebhookSignatureError.MalformedHeader, error.Error);
    }

    [Fact]
    public void Verify_TreatsAShortHexSignatureAsAMismatchNotMalformed()
    {
        var error = Assert.Throws<WebhookSignatureException>(
            () => WebhookSignature.Verify($"t={Now()},v1=abc", Body, SigningKey));

        Assert.Equal(WebhookSignatureError.SignatureMismatch, error.Error);
    }

    [Fact]
    public void Generate_MatchesTheDocumentedScheme()
    {
        // HMAC-SHA256("whsec_test", "1700000000." + Body), hex-encoded.
        using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(SigningKey));
        var expected = Convert.ToHexString(hmac.ComputeHash(Encoding.UTF8.GetBytes("1700000000." + Body)))
            .ToLowerInvariant();

        Assert.Equal(expected, WebhookSignature.Generate(Body, SigningKey, "1700000000"));
    }

    private static string Now() => DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString();

    private static string ValidHeader()
    {
        var timestamp = Now();
        return $"t={timestamp},v1={WebhookSignature.Generate(Body, SigningKey, timestamp)}";
    }
}
