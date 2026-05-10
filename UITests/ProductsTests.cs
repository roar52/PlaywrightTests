using NUnit.Framework;

namespace UITests;

public class ProductsTests : BaseTest
{
    [Test]
    [Description("8 - Просмотр всех товаров и страницы деталей товара")]
    public async Task AllProducts_AndProductDetailPage_AreDisplayedCorrectly()
    {
        await HomePage.OpenAsync();
        Assert.That(await HomePage.IsSliderVisibleAsync(), Is.True, "Слайдер на главной странице не отобразился, а должен был");

        await HomePage.ClickProductsAsync();
        Assert.That(await ProductsPage.IsAllProductsVisibleAsync(), Is.True, "Заголовок 'ALL PRODUCTS' не отобразился, а должен был");
        Assert.That(await ProductsPage.AreProductsListedAsync(), Is.True, "Список товаров пуст — товары не отобразились");

        await ProductsPage.ClickViewFirstProductAsync();
        Assert.Multiple(async () =>
        {
            Assert.That(await ProductDetailsPage.IsProductNameVisibleAsync(), Is.True, "Название товара не отобразилось, а должно было");
            Assert.That(await ProductDetailsPage.IsCategoryVisibleAsync(), Is.True, "Категория товара не отобразилась, а должна была");
            Assert.That(await ProductDetailsPage.IsPriceVisibleAsync(), Is.True, "Цена товара не отобразилась, а должна была");
            Assert.That(await ProductDetailsPage.IsAvailabilityVisibleAsync(), Is.True, "Поле Availability не отобразилось, а должно было");
            Assert.That(await ProductDetailsPage.IsConditionVisibleAsync(), Is.True, "Поле Condition не отобразилось, а должно было");
            Assert.That(await ProductDetailsPage.IsBrandVisibleAsync(), Is.True, "Поле Brand не отобразилось, а должно было");
        });
    }

    [Test]
    [Description("9 - Поиск товара")]
    public async Task SearchProduct_ReturnsRelevantResults()
    {
        await HomePage.OpenAsync();
        Assert.That(await HomePage.IsSliderVisibleAsync(), Is.True, "Слайдер на главной странице не отобразился, а должен был");

        await HomePage.ClickProductsAsync();
        Assert.That(await ProductsPage.IsAllProductsVisibleAsync(), Is.True, "Заголовок 'ALL PRODUCTS' не отобразился, а должен был");

        await ProductsPage.SearchProductAsync("Top");
        Assert.That(await ProductsPage.IsSearchedProductsVisibleAsync(), Is.True, "Заголовок 'SEARCHED PRODUCTS' не отобразился, а должен был");
        Assert.That(await ProductsPage.GetSearchResultsCountAsync(), Is.GreaterThan(0), "Поиск не вернул ни одного товара");
    }
}
