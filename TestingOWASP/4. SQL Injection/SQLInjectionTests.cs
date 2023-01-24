using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework.Internal;

namespace TestProject1._4._SQL_Injection
{
    /// <summary>
    /// Lab: SQL injection vulnerability in WHERE clause allowing retrieval of hidden data
    /// Url: https://portswigger.net/web-security/sql-injection/lab-retrieve-hidden-data
    /// </summary>
    internal class SQLInjectionTests : PageTest
    {
        private const string YourUniqueLabId = "<your-unique-lab-id>";

        private string target = null;

        [SetUp]
        public void Setup()
        {
            target = $"https://{YourUniqueLabId}.web-security-academy.net/";
        }

        [Test]
        public async Task ValidateItemsOnHomePage()
        {
            var defaultCount = 12;

            await Page.GotoAsync(target);
            var childeren = await Page.Locator(".container-list-tiles").Locator("div").AllAsync();

            Assert.That(childeren.Count(), Is.EqualTo(defaultCount));
        }

        [Test]
        public async Task ValidateCategoryFilter()
        {
            var filteredCount = 3;

            await Page.GotoAsync(target + "filter?category=Clothing%2c+shoes+and+accessories", new PageGotoOptions
            {
                WaitUntil = WaitUntilState.NetworkIdle
            });

            var categoryChilderen = await Page.Locator(".container-list-tiles").Locator("div").AllAsync();

            Assert.That(categoryChilderen.Count(), Is.EqualTo(filteredCount));
        }

        [Test]
        public async Task ValidateSQLInjection()
        {
            var injectedCount = 20;

            //= Your code here =//
        }
    }
}