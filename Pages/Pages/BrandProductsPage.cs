using Microsoft.Playwright;

namespace Pages.Pages;

public class BrandProductsPage : BasePage
{
    private ILocator PageHeading => Page.Locator(".features_items .title");

    public BrandProductsPage(IPage page) : base(page) { }

    /// <summary>
    /// Получить состояние отображения заголовка страницы бренда с указанным текстом
    /// (например, "BRAND - POLO PRODUCTS"). Метод дожидается появления заголовка.
    /// </summary>
    /// <param name="expectedText">Ожидаемый текст в заголовке страницы бренда</param>
    public async Task<bool> IsBrandHeadingVisibleAsync(string expectedText)
    {
        var locator = Page.Locator($".features_items .title:has-text('{expectedText}')");
        await locator.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible });
        return await locator.IsVisibleAsync();
    }

    /// <summary>
    /// Получить текст заголовка страницы бренда
    /// </summary>
    public Task<string> GetHeadingTextAsync() => PageHeading.InnerTextAsync();
}
