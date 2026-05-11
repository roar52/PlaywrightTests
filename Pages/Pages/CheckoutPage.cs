using Microsoft.Playwright;

namespace Pages.Pages;

public class CheckoutPage : BasePage
{
    protected override string Url => "/checkout";

    private ILocator AddressDelivery => Page.Locator("#address_delivery");
    private ILocator AddressInvoice => Page.Locator("#address_invoice");
    private ILocator ReviewYourOrderHeading => Page.Locator("h2:has-text('Review Your Order')");
    private ILocator CommentTextarea => Page.Locator("textarea[name='message']");
    private ILocator PlaceOrderButton => Page.Locator("a.btn.check_out");

    #region Register/Login modal
    private ILocator RegisterLoginModal => Page.Locator("#checkoutModal");
    private ILocator RegisterLoginLink => Page.Locator("#checkoutModal a[href='/login']");
    #endregion

    public CheckoutPage(IPage page) : base(page) { }

    /// <summary>
    /// Получить состояние отображения блока адреса доставки (с ожиданием появления)
    /// </summary>
    public async Task<bool> IsAddressDeliveryVisibleAsync()
    {
        await AddressDelivery.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible });
        return await AddressDelivery.IsVisibleAsync();
    }

    /// <summary>
    /// Получить состояние отображения блока адреса биллинга (с ожиданием появления)
    /// </summary>
    public async Task<bool> IsAddressInvoiceVisibleAsync()
    {
        await AddressInvoice.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible });
        return await AddressInvoice.IsVisibleAsync();
    }

    /// <summary>
    /// Получить состояние отображения заголовка "Review Your Order" (с ожиданием появления)
    /// </summary>
    public async Task<bool> IsReviewYourOrderVisibleAsync()
    {
        await ReviewYourOrderHeading.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible });
        return await ReviewYourOrderHeading.IsVisibleAsync();
    }

    /// <summary>
    /// Ввести комментарий к заказу
    /// </summary>
    /// <param name="comment">Текст комментария</param>
    public Task EnterCommentAsync(string comment) => CommentTextarea.FillAsync(comment);

    /// <summary>
    /// Нажать кнопку "Place Order"
    /// </summary>
    public Task ClickPlaceOrderAsync() => PlaceOrderButton.ClickAsync();

    /// <summary>
    /// Получить состояние отображения модального окна "Register / Login"
    /// (отображается, когда пользователь не авторизован при попытке оформить заказ).
    /// Метод активно дожидается появления модалки до DefaultTimeout.
    /// </summary>
    public async Task<bool> IsRegisterLoginModalVisibleAsync()
    {
        await RegisterLoginModal.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible });
        return await RegisterLoginModal.IsVisibleAsync();
    }

    /// <summary>
    /// Кликнуть по ссылке "Register / Login" в модальном окне чекаута
    /// </summary>
    public Task ClickRegisterLoginInModalAsync() => RegisterLoginLink.ClickAsync();
}
