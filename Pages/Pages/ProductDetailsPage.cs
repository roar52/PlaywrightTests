using Microsoft.Playwright;

namespace Pages.Pages;

public class ProductDetailsPage : BasePage
{
    private ILocator ProductInformation => Page.Locator(".product-information");
    private ILocator ProductName => ProductInformation.Locator("h2");
    private ILocator Category => ProductInformation.Locator("p:has-text('Category')");
    private ILocator Price => ProductInformation.Locator("span span");
    private ILocator Availability => ProductInformation.Locator("p:has-text('Availability')");
    private ILocator Condition => ProductInformation.Locator("p:has-text('Condition')");
    private ILocator Brand => ProductInformation.Locator("p:has-text('Brand')");

    public ProductDetailsPage(IPage page) : base(page) { }

    /// <summary>
    /// Получить состояние отображения названия товара
    /// </summary>
    public Task<bool> IsProductNameVisibleAsync() => ProductName.IsVisibleAsync();

    /// <summary>
    /// Получить состояние отображения категории товара
    /// </summary>
    public Task<bool> IsCategoryVisibleAsync() => Category.IsVisibleAsync();

    /// <summary>
    /// Получить состояние отображения цены товара
    /// </summary>
    public Task<bool> IsPriceVisibleAsync() => Price.IsVisibleAsync();

    /// <summary>
    /// Получить состояние отображения поля Availability
    /// </summary>
    public Task<bool> IsAvailabilityVisibleAsync() => Availability.IsVisibleAsync();

    /// <summary>
    /// Получить состояние отображения поля Condition
    /// </summary>
    public Task<bool> IsConditionVisibleAsync() => Condition.IsVisibleAsync();

    /// <summary>
    /// Получить состояние отображения поля Brand
    /// </summary>
    public Task<bool> IsBrandVisibleAsync() => Brand.IsVisibleAsync();
}
