using Microsoft.Playwright;

namespace Pages.Pages;

public class AccountPage : BasePage
{
    protected override string Url => "/account";

    private ILocator AccountDeletedHeading => Page.Locator("h2.title:has-text('Account Deleted!')");
    private ILocator ContinueButton => Page.Locator("a[data-qa='continue-button']");

    public AccountPage(IPage page) : base(page) { }

    /// <summary>
    /// Получить состояние отображения информации об удалении аккаунта
    /// </summary>
    public Task<bool> IsAccountDeletedVisibleAsync() => AccountDeletedHeading.IsVisibleAsync();
    
    /// <summary>
    /// Нажать кнопку "Continue"
    /// </summary>
    public Task ClickContinueAsync() => ContinueButton.ClickAsync();
}
