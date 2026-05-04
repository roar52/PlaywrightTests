using NUnit.Framework;
using UITests.Builders;

namespace UITests;

public class LoginTests : BaseTest
{
    protected override async Task OnSetUpAsync()
    {
        await AuthHelper.RegisterAsync();
    }

    protected override async Task OnTearDownAsync()
    {
        await AuthHelper.DeleteAccountAsync();
    }

    [Test]
    [Description("2 - Вход с корректными данными")]
    public async Task Login_WithCorrectCredentials_UserIsLoggedIn()
    {
        await HomePage.OpenAsync();
        Assert.That(await HomePage.IsSliderVisibleAsync(), Is.True);

        await HomePage.ClickSignupLoginAsync();
        Assert.That(await LoginPage.IsLoginHeadingVisibleAsync(), Is.True);

        await LoginPage.LoginAsync(User.Email, User.Password);
        Assert.That(await HomePage.IsLoggedInAsync(), Is.True);
    }

    [Test]
    [Description("3 - Вход с некорректными данными")]
    public async Task Login_WithIncorrectCredentials_ErrorIsShown()
    {
        await HomePage.OpenAsync();
        Assert.That(await HomePage.IsSliderVisibleAsync(), Is.True);

        await HomePage.ClickSignupLoginAsync();
        Assert.That(await LoginPage.IsLoginHeadingVisibleAsync(), Is.True);

        var loginData = new LoginDataBuilder()
            .WithEmail("wrong@email.com")
            .WithPassword("wrongpassword")
            .Build();
        await LoginPage.LoginAsync(loginData.Email, loginData.Password);
        Assert.That(await LoginPage.IsLoginErrorVisibleAsync(), Is.True);
    }

    [Test]
    [Description("4 - Выход из аккаунта")]
    public async Task Logout_LoggedInUser_RedirectedToLoginPage()
    {
        await HomePage.OpenAsync();
        Assert.That(await HomePage.IsSliderVisibleAsync(), Is.True);

        await HomePage.ClickSignupLoginAsync();
        Assert.That(await LoginPage.IsLoginHeadingVisibleAsync(), Is.True);

        await LoginPage.LoginAsync(User.Email, User.Password);
        Assert.That(await HomePage.IsLoggedInAsync(), Is.True);

        await HomePage.ClickLogoutAsync();
        Assert.That(await LoginPage.IsLoginHeadingVisibleAsync(), Is.True);
    }
}
