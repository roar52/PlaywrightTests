using Microsoft.Playwright;

namespace Pages;

public class BasePage
{
    protected IPage Page { get; }
    protected virtual string? Url { get; }
    private ILocator HomeLink => Page.Locator("a[href='/']").First;
    private ILocator ProductsLink => Page.Locator("a[href='/products']");
    private ILocator CartLink => Page.Locator("a[href='/view_cart']").First;
    private ILocator SignupLoginLink => Page.Locator("a[href='/login']");
    private ILocator LogoutLink => Page.Locator("a[href='/logout']");
    private ILocator DeleteAccountLink => Page.Locator("a[href='/delete_account']");
    private ILocator ContactUsLink => Page.Locator("a[href='/contact_us']");
    private ILocator TestCasesLink => Page.Locator("a[href='/test_cases']").First;
    private ILocator LoggedInLabel => Page.Locator("li:has-text('Logged in as')");
    
    protected BasePage(IPage page)
    {
        Page = page;
    }

    /// <summary>
    /// Получить заголовок вкладки
    /// </summary>
    /// <returns>Заголовок вкладки</returns>
    public Task<string> GetTitle() => Page.TitleAsync();

    /// <summary>
    /// Получить текущий URL страницы
    /// </summary>
    /// <returns>URL страницы</returns>
    public string GetUrl() => Page.Url;

    /// <summary>
    /// Открыть страницу по URL, заданному в свойстве Url конкретного класса
    /// </summary>
    /// <exception cref="InvalidOperationException">Если Url не задан в наследнике</exception>
    public async Task OpenAsync()
    {
        if (string.IsNullOrEmpty(Url))
            throw new InvalidOperationException($"Url не задан в классе {GetType().Name}");

        await Page.GotoAsync(Url, new PageGotoOptions { WaitUntil = WaitUntilState.DOMContentLoaded });
    }
    
    /// <summary>
    /// Нажать по ссылке «Home» в шапке сайта
    /// </summary>
    public Task ClickHomeAsync() => HomeLink.ClickAsync();

    /// <summary>
    /// Нажать по ссылке «Products» в шапке сайта
    /// </summary>
    public Task ClickProductsAsync() => ProductsLink.ClickAsync();

    /// <summary>
    /// Нажать по ссылке «Cart» в шапке сайта
    /// </summary>
    public Task ClickCartAsync() => CartLink.ClickAsync();

    /// <summary>
    /// Нажать по ссылке «Signup / Login» в шапке сайта
    /// </summary>
    public Task ClickSignupLoginAsync() => SignupLoginLink.ClickAsync();

    /// <summary>
    /// Нажать по ссылке «Logout» в шапке сайта
    /// </summary>
    public Task ClickLogoutAsync() => LogoutLink.ClickAsync();

    /// <summary>
    /// Нажать по ссылке «Delete Account» в шапке сайта.
    /// Удаляет текущий аккаунт и переводит пользователя на страницу-подтверждение.
    /// </summary>
    public Task ClickDeleteAccountAsync() => DeleteAccountLink.ClickAsync();

    /// <summary>
    /// Нажать по ссылке «Contact us» в шапке сайта
    /// </summary>
    public Task ClickContactUsAsync() => ContactUsLink.ClickAsync();

    /// <summary>
    /// Нажать по ссылке «Test Cases» в шапке сайта
    /// </summary>
    public Task ClickTestCasesAsync() => TestCasesLink.ClickAsync();

    /// <summary>
    /// Проверить состояние отображание надписи «Logged in as ...»
    /// </summary>
    public async Task<bool> IsLoggedInAsync()
        => await LoggedInLabel.IsVisibleAsync();
}