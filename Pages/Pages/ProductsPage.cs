using Microsoft.Playwright;

namespace Pages.Pages;

public class ProductsPage : BasePage
{
    protected override string Url => "/products";

    private ILocator AllProductsHeading => Page.Locator("h2.title:has-text('All Products')");
    private ILocator SearchedProductsHeading => Page.Locator("h2.title:has-text('Searched Products')");
    private ILocator ProductsList => Page.Locator(".features_items .product-image-wrapper");
    private ILocator FirstViewProductLink => Page.Locator("a[href^='/product_details/']").First;
    private ILocator SearchInput => Page.Locator("#search_product");
    private ILocator SearchButton => Page.Locator("#submit_search");

    public ProductsPage(IPage page) : base(page) { }

    /// <summary>
    /// Получить состояние отображения заголовка "ALL PRODUCTS"
    /// </summary>
    public Task<bool> IsAllProductsVisibleAsync() => AllProductsHeading.IsVisibleAsync();

    /// <summary>
    /// Проверить, что список товаров отображается (есть хотя бы одна карточка)
    /// </summary>
    public async Task<bool> AreProductsListedAsync() => await ProductsList.CountAsync() > 0;

    /// <summary>
    /// Кликнуть по ссылке "View Product" у первого товара в каталоге
    /// </summary>
    public Task ClickViewFirstProductAsync() => FirstViewProductLink.ClickAsync();

    /// <summary>
    /// Ввести поисковый запрос и нажать кнопку поиска
    /// </summary>
    /// <param name="name">Название товара или поисковая фраза</param>
    public async Task SearchProductAsync(string name)
    {
        await SearchInput.FillAsync(name);
        await SearchButton.ClickAsync();
    }

    /// <summary>
    /// Получить состояние отображения заголовка "SEARCHED PRODUCTS"
    /// </summary>
    public Task<bool> IsSearchedProductsVisibleAsync() => SearchedProductsHeading.IsVisibleAsync();

    /// <summary>
    /// Получить количество товаров, отображаемых в результатах поиска
    /// </summary>
    public Task<int> GetSearchResultsCountAsync() => ProductsList.CountAsync();

    /// <summary>
    /// Навести курсор на товар по индексу и нажать "Add to cart" в overlay
    /// </summary>
    /// <param name="index">Индекс товара (0-based)</param>
    public async Task HoverAndAddProductToCartAsync(int index)
    {
        var product = ProductsList.Nth(index);
        await product.HoverAsync();
        await product.Locator(".product-overlay .add-to-cart").First.ClickAsync();
    }
}
