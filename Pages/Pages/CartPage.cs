using Microsoft.Playwright;

namespace Pages.Pages;

public class CartPage : BasePage
{
    protected override string Url => "/view_cart";

    private ILocator CartTable => Page.Locator("#cart_info_table");
    private ILocator CartItemsBlock => Page.Locator("#cart_items");
    private ILocator ProductRows => Page.Locator("tr[id^='product-']");
    private ILocator ProceedToCheckoutButton => Page.Locator("a.btn.check_out");
    private ILocator EmptyCartMessage => Page.Locator("#empty_cart");

    #region Subscription
    private ILocator Footer => Page.Locator("#footer");
    private ILocator SubscriptionHeading => Page.Locator("div.single-widget h2:has-text('Subscription')");
    private ILocator SubscribeEmailInput => Page.Locator("#susbscribe_email");
    private ILocator SubscribeButton => Page.Locator("#subscribe");
    private ILocator SubscribeSuccessMessage => Page.Locator("#success-subscribe");
    #endregion

    public CartPage(IPage page) : base(page) { }

    /// <summary>
    /// Проверить, что страница корзины открыта (отображается контейнер #cart_items)
    /// </summary>
    public Task<bool> IsCartPageVisibleAsync() => CartItemsBlock.IsVisibleAsync();

    /// <summary>
    /// Проверить, что в корзине отображается таблица с товарами
    /// </summary>
    public Task<bool> IsCartTableVisibleAsync() => CartTable.IsVisibleAsync();

    /// <summary>
    /// Получить количество товаров (строк) в корзине
    /// </summary>
    public Task<int> GetProductsCountAsync() => ProductRows.CountAsync();

    /// <summary>
    /// Получить значение количества (quantity) у товара по индексу строки
    /// </summary>
    /// <param name="rowIndex">Индекс строки товара (0-based)</param>
    public async Task<string> GetProductQuantityAsync(int rowIndex)
    {
        var row = ProductRows.Nth(rowIndex);
        return await row.Locator(".cart_quantity button").InnerTextAsync();
    }

    /// <summary>
    /// Получить общую цену (total) у товара по индексу строки
    /// </summary>
    /// <param name="rowIndex">Индекс строки товара (0-based)</param>
    public async Task<string> GetProductTotalPriceAsync(int rowIndex)
    {
        var row = ProductRows.Nth(rowIndex);
        return await row.Locator(".cart_total .cart_total_price").InnerTextAsync();
    }

    /// <summary>
    /// Получить цену за единицу товара по индексу строки
    /// </summary>
    /// <param name="rowIndex">Индекс строки товара (0-based)</param>
    public async Task<string> GetProductUnitPriceAsync(int rowIndex)
    {
        var row = ProductRows.Nth(rowIndex);
        return await row.Locator(".cart_price p").InnerTextAsync();
    }

    /// <summary>
    /// Нажать кнопку "Proceed To Checkout"
    /// </summary>
    public Task ClickProceedToCheckoutAsync() => ProceedToCheckoutButton.ClickAsync();

    /// <summary>
    /// Получить название товара в корзине по индексу строки
    /// </summary>
    /// <param name="rowIndex">Индекс строки товара (0-based)</param>
    public async Task<string> GetProductNameAsync(int rowIndex)
    {
        var row = ProductRows.Nth(rowIndex);
        return await row.Locator(".cart_description h4 a").InnerTextAsync();
    }

    /// <summary>
    /// Удалить товар из корзины по индексу строки.
    /// Метод ожидает исчезновения строки из DOM.
    /// </summary>
    /// <param name="rowIndex">Индекс удаляемой строки (0-based)</param>
    public async Task RemoveProductAsync(int rowIndex)
    {
        var row = ProductRows.Nth(rowIndex);
        await row.Locator("a.cart_quantity_delete").ClickAsync();
        await row.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Detached });
    }

    /// <summary>
    /// Получить состояние отображения сообщения "Cart is empty!"
    /// (с ожиданием появления элемента)
    /// </summary>
    public async Task<bool> IsCartEmptyAsync()
    {
        await EmptyCartMessage.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible });
        return await EmptyCartMessage.IsVisibleAsync();
    }

    /// <summary>
    /// Прокрутить страницу к футеру
    /// </summary>
    public Task ScrollToFooterAsync() => Footer.ScrollIntoViewIfNeededAsync();

    /// <summary>
    /// Получить состояние отображения заголовка "SUBSCRIPTION" в футере
    /// </summary>
    public Task<bool> IsSubscriptionHeadingVisibleAsync() => SubscriptionHeading.IsVisibleAsync();

    /// <summary>
    /// Подписаться на рассылку: ввести email и нажать кнопку подписки
    /// </summary>
    /// <param name="email">Адрес электронной почты для подписки</param>
    public async Task SubscribeAsync(string email)
    {
        await SubscribeEmailInput.FillAsync(email);
        await SubscribeButton.ClickAsync();
    }

    /// <summary>
    /// Получить состояние отображения сообщения "You have been successfully subscribed!"
    /// </summary>
    public Task<bool> IsSubscribeSuccessVisibleAsync() => SubscribeSuccessMessage.IsVisibleAsync();
}
