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
        Assert.That(await HomePage.IsSliderVisibleAsync(), Is.True, "Слайдер на главной странице не отобразился, а должен был");

        await HomePage.ClickSignupLoginAsync();
        Assert.That(await LoginPage.IsLoginHeadingVisibleAsync(), Is.True, "Заголовок 'Login to your account' не отобразился, а должен был");

        await LoginPage.LoginAsync(User.Email, User.Password);
        Assert.That(await HomePage.IsLoggedInAsync(), Is.True, "Индикатор 'Logged in as' не отобразился — пользователь не авторизован");
    }

    [Test]
    [Description("3 - Вход с некорректными данными")]
    public async Task Login_WithIncorrectCredentials_ErrorIsShown()
    {
        await HomePage.OpenAsync();
        Assert.That(await HomePage.IsSliderVisibleAsync(), Is.True, "Слайдер на главной странице не отобразился, а должен был");

        await HomePage.ClickSignupLoginAsync();
        Assert.That(await LoginPage.IsLoginHeadingVisibleAsync(), Is.True, "Заголовок 'Login to your account' не отобразился, а должен был");

        var loginData = new LoginDataBuilder()
            .WithEmail("wrong@email.com")
            .WithPassword("wrongpassword")
            .Build();
        await LoginPage.LoginAsync(loginData.Email, loginData.Password);
        Assert.That(await LoginPage.IsLoginErrorVisibleAsync(), Is.True, "Сообщение об ошибке логина не отобразилось, а должно было");
    }

    [Test]
    [Description("4 - Выход из аккаунта")]
    public async Task Logout_LoggedInUser_RedirectedToLoginPage()
    {
        await HomePage.OpenAsync();
        Assert.That(await HomePage.IsSliderVisibleAsync(), Is.True, "Слайдер на главной странице не отобразился, а должен был");

        await HomePage.ClickSignupLoginAsync();
        Assert.That(await LoginPage.IsLoginHeadingVisibleAsync(), Is.True, "Заголовок 'Login to your account' не отобразился, а должен был");

        await LoginPage.LoginAsync(User.Email, User.Password);
        Assert.That(await HomePage.IsLoggedInAsync(), Is.True, "Индикатор 'Logged in as' не отобразился — пользователь не авторизован");

        await HomePage.ClickLogoutAsync();
        Assert.That(await LoginPage.IsLoginHeadingVisibleAsync(), Is.True, "После logout страница логина не отобразилась, а должна была");
    }
}
