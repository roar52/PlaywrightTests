using NUnit.Framework;

namespace UITests;

public class RemoveProductsFromCartTests : BaseTest
{
    [Test]
    [Description("17 - Удаление товара из корзины")]
    public async Task RemoveProduct_FromCart_CartBecomesEmpty()
    {
        await HomePage.OpenAsync();
        Assert.That(await HomePage.IsSliderVisibleAsync(), Is.True, "Слайдер на главной странице не отобразился, а должен был");

        await HomePage.ClickProductsAsync();
        await ProductsPage.HoverAndAddProductToCartAsync(0);
        Assert.That(await CartModal.IsModalVisibleAsync(), Is.True, "Модальное окно корзины не отобразилось после добавления товара");
        await CartModal.ClickViewCartAsync();

        Assert.That(await CartPage.IsCartTableVisibleAsync(), Is.True, "Таблица товаров в корзине не отобразилась, а должна была");
        Assert.That(await CartPage.GetProductsCountAsync(), Is.EqualTo(1), "В корзине ожидался 1 товар, фактическое количество отличается");

        await CartPage.RemoveProductAsync(0);
        Assert.That(await CartPage.IsCartEmptyAsync(), Is.True, "Корзина не отображается как пустая после удаления товара");
    }
}
