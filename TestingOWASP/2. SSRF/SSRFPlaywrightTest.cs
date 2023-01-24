using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;

namespace TestProject1._2._SSRF;

/// <summary>
/// Lab: Basic SSRF against another back-end system
/// Url: https://portswigger.net/web-security/ssrf/lab-basic-ssrf-against-backend-system
/// </summary>
internal class SSRFPlaywrightTest : PlaywrightTest
{
    private const string YourUniqueLabId = "<your-unique-lab-id>";

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
        // This test can result in a unexpected return from the API, see the ignore tag above. Please considere implementing this using the native HTTP client.
        //= Your code here =//
    }
}

