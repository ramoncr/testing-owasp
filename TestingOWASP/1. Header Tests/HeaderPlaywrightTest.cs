﻿using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;

namespace TestProject1._1._Headers;

/// <summary>
/// Validates Security Headers on Betabit.nl
/// </summary>
internal class HeaderPlaywrightTest : PlaywrightTest
{
    private IAPIRequestContext Request = null;

    [SetUp]
    public async Task Setup()
    {
        var target = $"https://betabit.nl";
        Request = await Playwright.APIRequest.NewContextAsync(new APIRequestNewContextOptions
        {
            BaseURL = target,
        });
    }

    [Test]
    [TestCase("strict-transport-security", "max-age=31536000; includeSubDomains")]
    [TestCase("x-frame-options", "SAMEORIGIN")]
    [TestCase("permissions-policy", "accelerometer=(), camera=(), geolocation=(), gyroscope=(), magnetometer=(), microphone=(), payment=(), usb=()")]
    [TestCase("content-security-policy", "default-src 'self' 'unsafe-inline' 'unsafe-eval' data: blob: https://tagmanager.google.com https://*.gstatic.com https://*.googleapis.com https://maps-api-ssl.google.com https://google-analytics.com/ https://www.google-analytics.com https://ajax.aspnetcdn.com/ https://www.googletagmanager.com/ https://www.youtube.com/embed/ https://www.youtube-nocookie.com/embed/ https://player.vimeo.com https://stats.g.doubleclick.net https://*.hotjar.com wss://*.hotjar.com https://www.google.com/recaptcha/ https://snap.licdn.com/ https://*.linkedin.com https://elearning.easygenerator.com https://*.osano.com https://www.buzzsprout.com;")]
    [TestCase("x-xss-protection", "1; mode=block")]
    public async Task ValidateHeaders(string headerName, string headerValue)
    {
        var response = await Request.GetAsync("/");

        Assert.That(response.Ok, Is.True);
        Assert.That(response.Headers.ContainsKey(headerName), Is.True);
        Assert.That(response.Headers.GetValueOrDefault(headerName), Is.EqualTo(headerValue));
    }
}

