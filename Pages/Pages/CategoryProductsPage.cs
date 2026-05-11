using Microsoft.Playwright;

namespace Pages.Pages;

public class CategoryProductsPage : BasePage
{
    private ILocator PageHeading => Page.Locator(".features_items .title");

    public CategoryProductsPage(IPage page) : base(page) { }

    /// <summary>
    /// Получить состояние отображения заголовка категории с указанным текстом
    /// (например, "WOMEN - DRESS PRODUCTS"). Метод дожидается появления заголовка.
    /// </summary>
    /// <param name="expectedText">Ожидаемый текст в заголовке страницы категории</param>
    public async Task<bool> IsCategoryHeadingVisibleAsync(string expectedText)
    {
        var locator = Page.Locator($".features_items .title:has-text('{expectedText}')");
        await locator.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible });
        return await locator.IsVisibleAsync();
    }

    /// <summary>
    /// Получить текст заголовка страницы категории/бренда
    /// </summary>
    public Task<string> GetHeadingTextAsync() => PageHeading.InnerTextAsync();
}
