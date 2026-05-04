using Microsoft.Playwright;

namespace Pages.Pages;

public class LoginPage:BasePage
{
    protected override string Url => "/login";
    #region Форма авторизации
    private ILocator LoginHeading => Page.Locator("div.login-form h2");
    private ILocator LoginEmail => Page.Locator("input[data-qa='login-email']");
    private ILocator LoginPassword => Page.Locator("input[data-qa='login-password']");
    private ILocator LoginButton => Page.Locator("button[data-qa='login-button']");
    private ILocator LoginError => Page.Locator("p:has-text('Your email or password is incorrect!')");
    #endregion

    #region Форма регистрации
    private ILocator SignupHeading => Page.Locator("div.signup-form h2");
    private ILocator SignupName => Page.Locator("input[data-qa='signup-name']");
    private ILocator SignupEmail => Page.Locator("input[data-qa='signup-email']");
    private ILocator SignupButton => Page.Locator("button[data-qa='signup-button']");
    private ILocator EmailAlreadyExistsError => Page.Locator("p:has-text('Email Address already exist!')");
    #endregion

    public LoginPage(IPage page) : base(page) { }

    /// <summary>
    /// Ввести email и пароль, нажать кнопку входа
    /// </summary>
    /// <param name="email">Адрес электронной почты</param>
    /// <param name="password">Пароль</param>
    public async Task LoginAsync(string email, string password)
    {
        await LoginEmail.FillAsync(email);
        await LoginPassword.FillAsync(password);
        await LoginButton.ClickAsync();
    }

    /// <summary>
    /// Получить состояние отображения заголовка "Login to your account"
    /// </summary>
    public Task<bool> IsLoginHeadingVisibleAsync() => LoginHeading.IsVisibleAsync();

    /// <summary>
    /// Получить состояние отображения ошибки "Your email or password is incorrect!"
    /// </summary>
    public Task<bool> IsLoginErrorVisibleAsync() => LoginError.IsVisibleAsync();

    /// <summary>
    /// Ввести имя и email, нажать кнопку "Signup"
    /// </summary>
    /// <param name="name">Имя пользователя</param>
    /// <param name="email">Адрес электронной почты</param>
    public async Task SignupAsync(string name, string email)
    {
        await SignupName.FillAsync(name);
        await SignupEmail.FillAsync(email);
        await SignupButton.ClickAsync();
    }

    /// <summary>
    /// Получить состояние отображения ошибки "Email Address already exist!"
    /// </summary>
    public Task<bool> IsEmailAlreadyExistsErrorVisibleAsync() => EmailAlreadyExistsError.IsVisibleAsync();

    /// <summary>
    /// Получить состояние отображения заголовка "New User Signup!"
    /// </summary>
    public Task<bool> IsNewUserSignupVisibleAsync() => SignupHeading.IsVisibleAsync();

    /// <summary>
    /// Дождаться появления страницы логина
    /// </summary>
    public Task WaitForLoginPageAsync() => LoginHeading.WaitForAsync();
}