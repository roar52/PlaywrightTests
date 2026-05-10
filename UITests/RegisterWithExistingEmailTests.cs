using NUnit.Framework;

namespace UITests;

public class RegisterWithExistingEmailTests : BaseTest
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
    [Description("5 - Регистрация с уже существующим email")]
    public async Task Register_WithExistingEmail_ErrorIsShown()
    {
        await HomePage.OpenAsync();
        Assert.That(await HomePage.IsSliderVisibleAsync(), Is.True, "Слайдер на главной странице не отобразился, а должен был");

        await HomePage.ClickSignupLoginAsync();
        Assert.That(await LoginPage.IsNewUserSignupVisibleAsync(), Is.True, "Форма 'New User Signup!' не отобразилась, а должна была");

        await LoginPage.SignupAsync(User.Name, User.Email);
        Assert.That(await LoginPage.IsEmailAlreadyExistsErrorVisibleAsync(), Is.True, "Ошибка 'Email Address already exist!' не отобразилась, а должна была");
    }
}
