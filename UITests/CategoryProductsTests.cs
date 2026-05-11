using NUnit.Framework;

namespace UITests;

public class CategoryProductsTests : BaseTest
{
    [Test]
    [Description("18 - Просмотр товаров по категориям")]
    public async Task CategoryProducts_AreDisplayedCorrectly()
    {
        await HomePage.OpenAsync();
        Assert.That(await HomePage.IsSliderVisibleAsync(), Is.True, "Слайдер на главной странице не отобразился, а должен был");

        await HomePage.ClickProductsAsync();
        Assert.That(await ProductsPage.IsCategoriesSidebarVisibleAsync(), Is.True, "Sidebar с категориями не отобразился, а должен был");

        await ProductsPage.ExpandWomenCategoryAsync();
        await ProductsPage.ClickWomenDressAsync();
        Assert.That(await CategoryProductsPage.IsCategoryHeadingVisibleAsync("Women - Dress Products"), Is.True, "Заголовок категории 'Women - Dress Products' не отобразился, а должен был");

        await ProductsPage.ExpandMenCategoryAsync();
        await ProductsPage.ClickMenTshirtsAsync();
        Assert.That(await CategoryProductsPage.IsCategoryHeadingVisibleAsync("Men - Tshirts Products"), Is.True, "Заголовок категории 'Men - Tshirts Products' не отобразился, а должен был");
    }
}
