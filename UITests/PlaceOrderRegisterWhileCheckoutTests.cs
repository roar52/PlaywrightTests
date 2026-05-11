using NUnit.Framework;
using UITests.Models;

namespace UITests;

public class PlaceOrderRegisterWhileCheckoutTests : BaseTest
{
    protected override async Task OnTearDownAsync()
    {
        await AuthHelper.DeleteAccountAsync();
    }

    [Test]
    [Description("14 - Оформление заказа: регистрация во время чекаута")]
    public async Task PlaceOrder_RegisterWhileCheckout_OrderIsConfirmed()
    {
        await HomePage.OpenAsync();
        Assert.That(await HomePage.IsSliderVisibleAsync(), Is.True, "Слайдер на главной странице не отобразился, а должен был");

        await HomePage.ClickProductsAsync();
        await ProductsPage.HoverAndAddProductToCartAsync(0);
        Assert.That(await CartModal.IsModalVisibleAsync(), Is.True, "Модальное окно корзины не отобразилось после добавления товара");
        await CartModal.ClickViewCartAsync();

        Assert.That(await CartPage.IsCartTableVisibleAsync(), Is.True, "Таблица товаров в корзине не отобразилась, а должна была");

        await CartPage.ClickProceedToCheckoutAsync();
        Assert.That(await CheckoutPage.IsRegisterLoginModalVisibleAsync(), Is.True, "Модальное окно 'Register / Login' не отобразилось — гостю должно быть предложено зарегистрироваться");

        await CheckoutPage.ClickRegisterLoginInModalAsync();

        await LoginPage.SignupAsync(User.Name, User.Email);
        Assert.That(await SignUpPage.IsEnterAccountInfoVisibleAsync(), Is.True, "Форма 'Enter Account Information' не отобразилась, а должна была");

        await SignUpPage.SelectTitleAsync(User.Title);
        await SignUpPage.FillPasswordAsync(User.Password);
        await SignUpPage.SelectDateOfBirthAsync(User.Day, User.Month, User.Year);
        await SignUpPage.CheckNewsletterAsync();
        await SignUpPage.CheckSpecialOffersAsync();

        await SignUpPage.FillAddressDetailsAsync(
            firstName: User.FirstName,
            lastName: User.LastName,
            company: User.Company,
            address: User.Address,
            address2: User.Address2,
            country: User.Country,
            state: User.State,
            city: User.City,
            zipcode: User.Zipcode,
            mobile: User.Mobile
        );

        await SignUpPage.ClickCreateAccountAsync();
        Assert.That(await SignUpPage.IsAccountCreatedVisibleAsync(), Is.True, "Сообщение 'Account Created!' не отобразилось, а должно было");

        await SignUpPage.ClickContinueAsync();
        Assert.That(await HomePage.IsLoggedInAsync(), Is.True, "Индикатор 'Logged in as' не отобразился — пользователь не авторизован после регистрации");

        await HomePage.ClickCartAsync();
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
