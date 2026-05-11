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
    private ILocator QuantityInput => Page.Locator("#quantity");
    private ILocator AddToCartButton => Page.Locator("button.cart");

    #region Write Your Review
    private ILocator WriteReviewTab => Page.Locator("a[href='#reviews']");
    private ILocator ReviewName => Page.Locator("#name");
    private ILocator ReviewEmail => Page.Locator("#email");
    private ILocator ReviewText => Page.Locator("#review");
    private ILocator SubmitReviewButton => Page.Locator("#button-review");
    private ILocator ReviewSuccessMessage => Page.Locator("div.alert-success span:has-text('Thank you for your review.')");
    #endregion

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

    /// <summary>
    /// Установить значение в поле Quantity
    /// </summary>
    /// <param name="quantity">Желаемое количество товара</param>
    public Task SetQuantityAsync(int quantity) => QuantityInput.FillAsync(quantity.ToString());

    /// <summary>
    /// Нажать кнопку "Add to cart" на странице деталей товара
    /// </summary>
    public Task ClickAddToCartAsync() => AddToCartButton.ClickAsync();

    /// <summary>
    /// Получить состояние отображения секции "Write Your Review"
    /// </summary>
    public Task<bool> IsWriteReviewSectionVisibleAsync() => WriteReviewTab.IsVisibleAsync();

    /// <summary>
    /// Заполнить форму отзыва: имя, email и текст отзыва
    /// </summary>
    /// <param name="name">Имя автора отзыва</param>
    /// <param name="email">Email автора отзыва</param>
    /// <param name="review">Текст отзыва</param>
    public async Task FillReviewAsync(string name, string email, string review)
    {
        await ReviewName.FillAsync(name);
        await ReviewEmail.FillAsync(email);
        await ReviewText.FillAsync(review);
    }

    /// <summary>
    /// Нажать кнопку "Submit" на форме отзыва
    /// </summary>
    public Task ClickSubmitReviewAsync() => SubmitReviewButton.ClickAsync();

    /// <summary>
    /// Получить состояние отображения сообщения "Thank you for your review."
    /// (с ожиданием появления элемента)
    /// </summary>
    public async Task<bool> IsReviewSuccessMessageVisibleAsync()
    {
        await ReviewSuccessMessage.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible });
        return await ReviewSuccessMessage.IsVisibleAsync();
    }
}
