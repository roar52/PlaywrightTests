using NUnit.Framework;

namespace UITests;

public class SearchAndCartAfterLoginTests : BaseTest
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
    [Description("20 - Поиск товаров и сохранение корзины после логина")]
    public async Task SearchProducts_AndCartIsKept_AfterLogin()
    {
        const string searchQuery = "Top";

        await HomePage.OpenAsync();
        Assert.That(await HomePage.IsSliderVisibleAsync(), Is.True, "Слайдер на главной странице не отобразился, а должен был");

        await HomePage.ClickProductsAsync();
        Assert.That(await ProductsPage.IsAllProductsVisibleAsync(), Is.True, "Заголовок 'ALL PRODUCTS' не отобразился, а должен был");

        await ProductsPage.SearchProductAsync(searchQuery);
        Assert.That(await ProductsPage.IsSearchedProductsVisibleAsync(), Is.True, "Заголовок 'SEARCHED PRODUCTS' не отобразился, а должен был");
        var foundCount = await ProductsPage.GetSearchResultsCountAsync();
        Assert.That(foundCount, Is.GreaterThan(0), "Поиск не вернул ни одного товара");

        await ProductsPage.HoverAndAddProductToCartAsync(0);
        Assert.That(await CartModal.IsModalVisibleAsync(), Is.True, "Модальное окно корзины не отобразилось после добавления товара");
        await CartModal.ClickContinueShoppingAsync();

        await HomePage.ClickCartAsync();
        Assert.That(await CartPage.IsCartTableVisibleAsync(), Is.True, "Таблица товаров в корзине не отобразилась перед логином");
        var productsCountBeforeLogin = await CartPage.GetProductsCountAsync();
        Assert.That(productsCountBeforeLogin, Is.GreaterThan(0), "Корзина пуста до логина — товары должны были сохраниться");

        await HomePage.ClickSignupLoginAsync();
        await LoginPage.LoginAsync(User.Email, User.Password);
        Assert.That(await HomePage.IsLoggedInAsync(), Is.True, "Индикатор 'Logged in as' не отобразился — пользователь не авторизован");

        await HomePage.ClickCartAsync();
        Assert.That(await CartPage.IsCartTableVisibleAsync(), Is.True, "Таблица товаров в корзине после логина не отобразилась, а должна была");
        Assert.That(await CartPage.GetProductsCountAsync(), Is.EqualTo(productsCountBeforeLogin), "Количество товаров в корзине после логина не совпадает с количеством до логина");
    }
}
