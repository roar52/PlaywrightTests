using NUnit.Framework;

namespace UITests;

public class CartTests : BaseTest
{
    [Test]
    [Description("12 - Добавление товаров в корзину")]
    public async Task AddProducts_ToCart_AreVisibleInCart()
    {
        await HomePage.OpenAsync();
        Assert.That(await HomePage.IsSliderVisibleAsync(), Is.True, "Слайдер на главной странице не отобразился, а должен был");

        await HomePage.ClickProductsAsync();
        Assert.That(await ProductsPage.IsAllProductsVisibleAsync(), Is.True, "Заголовок 'ALL PRODUCTS' не отобразился, а должен был");

        await ProductsPage.HoverAndAddProductToCartAsync(0);
        Assert.That(await CartModal.IsModalVisibleAsync(), Is.True, "Модальное окно корзины не отобразилось после добавления первого товара");
        await CartModal.ClickContinueShoppingAsync();

        await ProductsPage.HoverAndAddProductToCartAsync(1);
        Assert.That(await CartModal.IsModalVisibleAsync(), Is.True, "Модальное окно корзины не отобразилось после добавления второго товара");
        await CartModal.ClickViewCartAsync();

        Assert.That(await CartPage.IsCartTableVisibleAsync(), Is.True, "Таблица товаров в корзине не отобразилась, а должна была");
        Assert.That(await CartPage.GetProductsCountAsync(), Is.EqualTo(2), "В корзине ожидалось 2 товара, фактическое количество отличается");
    }

    [Test]
    [Description("13 - Проверка количества товара в корзине")]
    public async Task ProductQuantity_InCart_MatchesSelectedQuantity()
    {
        const int expectedQuantity = 4;

        await HomePage.OpenAsync();
        Assert.That(await HomePage.IsSliderVisibleAsync(), Is.True, "Слайдер на главной странице не отобразился, а должен был");

        await HomePage.ClickProductsAsync();
        Assert.That(await ProductsPage.IsAllProductsVisibleAsync(), Is.True, "Заголовок 'ALL PRODUCTS' не отобразился, а должен был");

        await ProductsPage.ClickViewFirstProductAsync();
        Assert.That(await ProductDetailsPage.IsProductNameVisibleAsync(), Is.True, "Название товара на странице деталей не отобразилось, а должно было");

        await ProductDetailsPage.SetQuantityAsync(expectedQuantity);
        await ProductDetailsPage.ClickAddToCartAsync();

        Assert.That(await CartModal.IsModalVisibleAsync(), Is.True, "Модальное окно корзины не отобразилось после добавления товара");
        await CartModal.ClickViewCartAsync();

        Assert.That(await CartPage.IsCartTableVisibleAsync(), Is.True, "Таблица товаров в корзине не отобразилась, а должна была");
        Assert.That(await CartPage.GetProductQuantityAsync(0), Is.EqualTo(expectedQuantity.ToString()), "Количество товара в корзине не соответствует выбранному значению");
    }
}
