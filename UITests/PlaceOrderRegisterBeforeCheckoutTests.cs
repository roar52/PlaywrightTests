using NUnit.Framework;
using UITests.Models;

namespace UITests;

public class PlaceOrderRegisterBeforeCheckoutTests : BaseTest
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
    [Description("15 - Оформление заказа: регистрация до чекаута")]
    public async Task PlaceOrder_RegisterBeforeCheckout_OrderIsConfirmed()
    {
        await HomePage.OpenAsync();
        Assert.That(await HomePage.IsSliderVisibleAsync(), Is.True, "Слайдер на главной странице не отобразился, а должен был");

        await HomePage.ClickSignupLoginAsync();
        await LoginPage.LoginAsync(User.Email, User.Password);
        Assert.That(await HomePage.IsLoggedInAsync(), Is.True, "Индикатор 'Logged in as' не отобразился — пользователь не авторизован");

        await HomePage.ClickProductsAsync();
        await ProductsPage.HoverAndAddProductToCartAsync(0);
        Assert.That(await CartModal.IsModalVisibleAsync(), Is.True, "Модальное окно корзины не отобразилось после добавления товара");
        await CartModal.ClickViewCartAsync();

        Assert.That(await CartPage.IsCartTableVisibleAsync(), Is.True, "Таблица товаров в корзине не отобразилась, а должна была");

        await CartPage.ClickProceedToCheckoutAsync();
        Assert.That(await CheckoutPage.IsAddressDeliveryVisibleAsync(), Is.True, "Блок адреса доставки на странице чекаута не отобразился, а должен был");
        Assert.That(await CheckoutPage.IsAddressInvoiceVisibleAsync(), Is.True, "Блок адреса биллинга на странице чекаута не отобразился, а должен был");

        await CheckoutPage.EnterCommentAsync("Test order comment");
        await CheckoutPage.ClickPlaceOrderAsync();

        await PaymentPage.FillPaymentDetailsAsync(
            nameOnCard: TestConstants.Payment.NameOnCard,
            cardNumber: TestConstants.Payment.CardNumber,
            cvc: TestConstants.Payment.Cvc,
            month: TestConstants.Payment.ExpiryMonth,
            year: TestConstants.Payment.ExpiryYear
        );
        await PaymentPage.ClickPayAndConfirmAsync();

        Assert.That(await PaymentPage.IsOrderConfirmedVisibleAsync(), Is.True, "Заказ не подтверждён — сообщение 'Order Placed!' не отобразилось");
    }
}
