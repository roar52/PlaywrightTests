using NUnit.Framework;

namespace UITests;

public class AddressDetailsInCheckoutTests : BaseTest
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
    [Description("23 - Проверка адресных данных на странице чекаута")]
    public async Task AddressDetails_OnCheckout_MatchRegistrationData()
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

        await CartPage.ClickProceedToCheckoutAsync();
        Assert.That(await CheckoutPage.IsAddressDeliveryVisibleAsync(), Is.True, "Блок адреса доставки не отобразился, а должен был");
        Assert.That(await CheckoutPage.IsAddressInvoiceVisibleAsync(), Is.True, "Блок адреса биллинга не отобразился, а должен был");

        var deliveryLines = await CheckoutPage.GetDeliveryAddressLinesAsync();
        var billingLines = await CheckoutPage.GetBillingAddressLinesAsync();

        var fullName = $"{User.Title}. {User.FirstName} {User.LastName}";

        Assert.That(deliveryLines, Has.Some.Contains(fullName), $"Адрес доставки не содержит ФИО пользователя ('{fullName}')");
        Assert.That(deliveryLines, Has.Some.Contains(User.Company), "Адрес доставки не содержит название компании");
        Assert.That(deliveryLines, Has.Some.Contains(User.Address), "Адрес доставки не содержит улицу");
        Assert.That(deliveryLines, Has.Some.Contains(User.City), $"Адрес доставки не содержит город '{User.City}'");
        Assert.That(deliveryLines, Has.Some.Contains(User.Country), $"Адрес доставки не содержит страну '{User.Country}'");
        Assert.That(deliveryLines, Has.Some.Contains(User.Mobile), "Адрес доставки не содержит номер мобильного телефона");

        Assert.That(billingLines, Has.Some.Contains(fullName), $"Адрес биллинга не содержит ФИО пользователя ('{fullName}')");
        Assert.That(billingLines, Has.Some.Contains(User.Company), "Адрес биллинга не содержит название компании");
        Assert.That(billingLines, Has.Some.Contains(User.Address), "Адрес биллинга не содержит улицу");
        Assert.That(billingLines, Has.Some.Contains(User.City), $"Адрес биллинга не содержит город '{User.City}'");
        Assert.That(billingLines, Has.Some.Contains(User.Country), $"Адрес биллинга не содержит страну '{User.Country}'");
        Assert.That(billingLines, Has.Some.Contains(User.Mobile), "Адрес биллинга не содержит номер мобильного телефона");
    }
}
