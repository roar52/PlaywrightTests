using Microsoft.Playwright;

namespace Pages.Pages;

public class BrandProductsPage : BasePage
{
    private ILocator PageHeading => Page.Locator(".features_items .title");

    public BrandProductsPage(IPage page) : base(page) { }

    /// <summary>
    /// Получить состояние отображения заголовка страницы с ожидаемым текстом
    /// </summary>
    /// <param name="expectedText">Ожидаемый текст в заголовке</param>
    public async Task<bool> IsBrandHeadingVisibleAsync(string expectedText)
    {
        var locator = Page.Locator($".features_items .title:has-text('{expectedText}')");
        await locator.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible });
        return await locator.IsVisibleAsync();
    }

    /// <summary>
    /// Получить текст заголовка страницы
    /// </summary>
    public Task<string> GetHeadingTextAsync() => PageHeading.InnerTextAsync();
}
