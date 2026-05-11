using NUnit.Framework;

namespace UITests;

public class RecommendedItemsTests : BaseTest
{
    [Test]
    [Description("22 - Добавление в корзину из блока 'Recommended Items'")]
    public async Task RecommendedItem_AddToCart_IsInCart()
    {
        await HomePage.OpenAsync();
        Assert.That(await HomePage.IsSliderVisibleAsync(), Is.True, "Слайдер на главной странице не отобразился, а должен был");

        await HomePage.ScrollToRecommendedItemsAsync();
        Assert.That(await HomePage.IsRecommendedItemsVisibleAsync(), Is.True, "Заголовок 'RECOMMENDED ITEMS' не отобразился, а должен был");

        await HomePage.AddRecommendedProductToCartAsync();
        Assert.That(await CartModal.IsModalVisibleAsync(), Is.True, "Модальное окно корзины не отобразилось после добавления рекомендованного товара");
        await CartModal.ClickViewCartAsync();

        Assert.That(await CartPage.IsCartTableVisibleAsync(), Is.True, "Таблица товаров в корзине не отобразилась, а должна была");
        Assert.That(await CartPage.GetProductsCountAsync(), Is.GreaterThan(0), "Корзина пуста — рекомендованный товар не добавился");
    }
}
