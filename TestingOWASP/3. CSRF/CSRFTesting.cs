using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;

namespace TestProject1._3._CSRF;

/// <summary>
/// Lab: Stealing OAuth access tokens via an open endpoint
/// Url: https://portswigger.net/web-security/oauth/lab-oauth-authentication-bypass-via-oauth-implicit-flow
/// Comments: Does not include step 7 and 8
/// </summary>
internal class CSRFTesting : PageTest
{
    private const string YourUniqueLabId = "<your-unique-lab-id>";

    private const string Username_A = "wiener";
    private const string Password_A = "peter";

    private const string Username_B = "carlos";
    private const string Password_B = "montoya";

    private const string targetEmail = "random@local.dev";

    private string target = null;
    private IBrowserContext wienterContext;
    private IBrowserContext carlosContext;

    [SetUp]
    public async Task Setup()
    {
        target = $"https://{YourUniqueLabId}.web-security-academy.net/";

        wienterContext = await Browser.NewContextAsync();
        carlosContext = await Browser.NewContextAsync();
    }

    [Test]
    public async Task CSRFProofTokenNotAllowsRandomContent()
    {
        await Page.GotoAsync(target);

        await LoginToAccount(Page, Username_A, Password_A);

        //= Your code here =//

        var errorMessage = await Page.Locator("xpath=/html/body/pre").InnerTextAsync();

        Assert.That(errorMessage, Contains.Substring("Invalid CSRF token"));
    }

    [Test]
    public async Task CSRFProofTokenNotLinkedToSession()
    {
        var wienerPage = await wienterContext.NewPageAsync();
        var carlosPage = await carlosContext.NewPageAsync();

        await wienerPage.GotoAsync(target);
        await carlosPage.GotoAsync(target);

        await LoginToAccount(wienerPage, Username_A, Password_A);
        await LoginToAccount(carlosPage, Username_B, Password_B);

        //= Your code here =//

        var setEmail = await carlosPage.Locator("xpath=/html/body/div[2]/section/div/div/p[2]").InnerTextAsync();

        Assert.That(setEmail, Contains.Substring(targetEmail));
    }

    private async Task LoginToAccount(IPage page, string username, string password)
    {
        await page.ClickAsync("xpath=/html/body/div[2]/section/div/header[1]/section/a[2]");
        await page.Locator("[name=username]").FillAsync(username);
        await page.Locator("[name=password]").FillAsync(password);
        await page.ClickAsync("xpath=/html/body/div[2]/section/div/section/form/button");
    }
}