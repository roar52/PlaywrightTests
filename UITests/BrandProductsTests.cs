using NUnit.Framework;

namespace UITests;

public class BrandProductsTests : BaseTest
{
    [Test]
    [Description("19 - Просмотр и переход по брендам")]
    public async Task BrandProducts_AreDisplayedCorrectly()
    {
        await HomePage.OpenAsync();
        Assert.That(await HomePage.IsSliderVisibleAsync(), Is.True, "Слайдер на главной странице не отобразился, а должен был");

        await HomePage.ClickProductsAsync();
        Assert.That(await ProductsPage.IsBrandsSidebarVisibleAsync(), Is.True, "Sidebar с брендами не отобразился, а должен был");

        await ProductsPage.ClickBrandAsync("Polo");
        Assert.That(await BrandProductsPage.IsBrandHeadingVisibleAsync("Brand - Polo Products"), Is.True, "Заголовок страницы бренда 'Brand - Polo Products' не отобразился, а должен был");

        await ProductsPage.ClickBrandAsync("H&M");
        Assert.That(await BrandProductsPage.IsBrandHeadingVisibleAsync("Brand - H&M Products"), Is.True, "Заголовок страницы бренда 'Brand - H&M Products' не отобразился, а должен был");
    }
}
