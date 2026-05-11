using Microsoft.Playwright;

namespace Pages.Pages;

public class CartModalDialog : BasePage
{
    private ILocator Modal => Page.Locator("#cartModal");
    private ILocator ContinueShoppingButton => Page.Locator("#cartModal button.close-modal");
    private ILocator ViewCartLink => Page.Locator("#cartModal a[href='/view_cart']");

    public CartModalDialog(IPage page) : base(page) { }

    /// <summary>
    /// Получить состояние отображения модального окна корзины (после Add to cart).
    /// Метод активно дожидается появления модалки до DefaultTimeout.
    /// </summary>
    public async Task<bool> IsModalVisibleAsync()
    {
        await Modal.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible });
        return await Modal.IsVisibleAsync();
    }

    /// <summary>
    /// Нажать кнопку "Continue Shopping" в модальном окне
    /// </summary>
    public Task ClickContinueShoppingAsync() => ContinueShoppingButton.ClickAsync();

    /// <summary>
    /// Нажать ссылку "View Cart" в модальном окне
    /// </summary>
    public Task ClickViewCartAsync() => ViewCartLink.ClickAsync();
}
