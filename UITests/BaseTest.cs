using Core;
using Microsoft.Playwright;
using NUnit.Framework;
using Pages.Pages;
using UITests.Builders;
using UITests.Helpers;
using UITests.Models;

namespace UITests;

public abstract class BaseTest
{
    protected PlaywrightDriver Driver { get; private set; } = null!;
    protected IBrowserContext Context { get; private set; } = null!;
    protected IPage Page { get; private set; } = null!;
    protected HomePage HomePage { get; private set; } = null!;
    protected LoginPage LoginPage { get; private set; } = null!;
    protected SignUpPage SignUpPage { get; private set; } = null!;
    protected AccountPage AccountPage { get; private set; } = null!;
    protected AuthHelper AuthHelper { get; private set; } = null!;
    protected UserData User { get; private set; } = null!;

    [OneTimeSetUp]
    public async Task OneTimeSetupAsync()
    {
        Driver = await PlaywrightDriver.CreateAsync();
    }

    [SetUp]
    public async Task SetUpAsync()
    {
        Context = await Driver.CreateContextAsync();
        Page = await Context.NewPageAsync();
        HomePage = new HomePage(Page);
        LoginPage = new LoginPage(Page);
        SignUpPage = new SignUpPage(Page);
        AccountPage = new AccountPage(Page);
        User = new UserBuilder().Build();
        AuthHelper = new AuthHelper(HomePage, LoginPage, SignUpPage, AccountPage, User);

        await OnSetUpAsync();
    }

    [TearDown]
    public async Task TearDownAsync()
    {
        await OnTearDownAsync();
        await Context.CloseAsync();
    }

    [OneTimeTearDown]
    public async Task OneTimeTearDownAsync()
    {
        await Driver.DisposeAsync();
    }

    /// <summary>
    /// Переопределить для добавления дополнительных шагов в SetUp
    /// </summary>
    protected virtual Task OnSetUpAsync() => Task.CompletedTask;

    /// <summary>
    /// Переопределить для добавления дополнительных шагов в TearDown
    /// </summary>
    protected virtual Task OnTearDownAsync() => Task.CompletedTask;
}
