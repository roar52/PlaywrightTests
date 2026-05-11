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

    #region Sidebar Categories / Brands
    private ILocator CategoriesSidebar => Page.Locator("#accordian");
    private ILocator BrandsSidebar => Page.Locator(".brands_products");
    private ILocator WomenCategoryToggle => Page.Locator("a[href='#Women']");
    private ILocator MenCategoryToggle => Page.Locator("a[href='#Men']");
    private ILocator KidsCategoryToggle => Page.Locator("a[href='#Kids']");
    private ILocator WomenDressLink => Page.Locator("a[href='/category_products/1']");
    private ILocator MenTshirtsLink => Page.Locator("a[href='/category_products/3']");
    #endregion

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

    /// <summary>
    /// Получить состояние отображения sidebar с категориями (Women/Men/Kids)
    /// </summary>
    public Task<bool> IsCategoriesSidebarVisibleAsync() => CategoriesSidebar.IsVisibleAsync();

    /// <summary>
    /// Получить состояние отображения sidebar с брендами
    /// </summary>
    public Task<bool> IsBrandsSidebarVisibleAsync() => BrandsSidebar.IsVisibleAsync();

    /// <summary>
    /// Раскрыть категорию "Women" в sidebar
    /// </summary>
    public Task ExpandWomenCategoryAsync() => WomenCategoryToggle.ClickAsync();

    /// <summary>
    /// Раскрыть категорию "Men" в sidebar
    /// </summary>
    public Task ExpandMenCategoryAsync() => MenCategoryToggle.ClickAsync();

    /// <summary>
    /// Раскрыть категорию "Kids" в sidebar
    /// </summary>
    public Task ExpandKidsCategoryAsync() => KidsCategoryToggle.ClickAsync();

    /// <summary>
    /// Кликнуть подкатегорию Women → Dress
    /// </summary>
    public Task ClickWomenDressAsync() => WomenDressLink.ClickAsync();

    /// <summary>
    /// Кликнуть подкатегорию Men → Tshirts
    /// </summary>
    public Task ClickMenTshirtsAsync() => MenTshirtsLink.ClickAsync();

    /// <summary>
    /// Кликнуть по бренду в sidebar по его имени
    /// </summary>
    /// <param name="brandName">Точное название бренда (например, "Polo", "H&M")</param>
    public Task ClickBrandAsync(string brandName) => Page.Locator($"a[href='/brand_products/{brandName}']").ClickAsync();
}
