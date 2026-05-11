using Microsoft.Playwright;

namespace Pages.Pages;

public class HomePage : BasePage
{
    protected override string Url => "/";

    private ILocator Slider => Page.Locator("#slider-carousel");
    private ILocator FeaturedProducts => Page.Locator(".features_items .product-image-wrapper");

    #region Recommended Items
    private ILocator RecommendedSection => Page.Locator("div.recommended_items");
    private ILocator RecommendedHeading => Page.Locator("div.recommended_items h2.title");
    private ILocator RecommendedAddToCartFirst => Page.Locator(".recommended_items .item.active .productinfo .add-to-cart").First;
    #endregion

    #region Scroll Up
    private ILocator ScrollUpButton => Page.Locator("#scrollUp");
    #endregion

    #region Subscription
    private ILocator Footer => Page.Locator("#footer");
    private ILocator SubscriptionHeading => Page.Locator("div.single-widget h2:has-text('Subscription')");
    private ILocator SubscribeEmailInput => Page.Locator("#susbscribe_email");
    private ILocator SubscribeButton => Page.Locator("#subscribe");
    private ILocator SubscribeSuccessMessage => Page.Locator("#success-subscribe");
    #endregion

    public HomePage(IPage page) : base(page) { }

    /// <summary>
    /// Получить состояние отображения слайдера
    /// </summary>
    public Task<bool> IsSliderVisibleAsync() => Slider.IsVisibleAsync();

    /// <summary>
    /// Прокрутить страницу к футеру
    /// </summary>
    public Task ScrollToFooterAsync() => Footer.ScrollIntoViewIfNeededAsync();

    /// <summary>
    /// Получить состояние отображения заголовка "SUBSCRIPTION" в футере
    /// </summary>
    public Task<bool> IsSubscriptionHeadingVisibleAsync() => SubscriptionHeading.IsVisibleAsync();

    /// <summary>
    /// Подписаться на рассылку: ввести email и нажать кнопку подписки
    /// </summary>
    /// <param name="email">Адрес электронной почты для подписки</param>
    public async Task SubscribeAsync(string email)
    {
        await SubscribeEmailInput.FillAsync(email);
        await SubscribeButton.ClickAsync();
    }

    /// <summary>
    /// Получить состояние отображения сообщения "You have been successfully subscribed!"
    /// </summary>
    public Task<bool> IsSubscribeSuccessVisibleAsync() => SubscribeSuccessMessage.IsVisibleAsync();

    /// <summary>
    /// Навести курсор на товар по индексу и нажать "Add to cart" в overlay
    /// </summary>
    /// <param name="index">Индекс товара в блоке Featured items (0-based)</param>
    public async Task HoverAndAddProductToCartAsync(int index)
    {
        var product = FeaturedProducts.Nth(index);
        await product.HoverAsync();
        await product.Locator(".product-overlay .add-to-cart").First.ClickAsync();
    }

    /// <summary>
    /// Прокрутить страницу к секции "RECOMMENDED ITEMS"
    /// </summary>
    public Task ScrollToRecommendedItemsAsync() => RecommendedSection.ScrollIntoViewIfNeededAsync();

    /// <summary>
    /// Получить состояние отображения заголовка секции "RECOMMENDED ITEMS"
    /// </summary>
    public Task<bool> IsRecommendedItemsVisibleAsync() => RecommendedHeading.IsVisibleAsync();

    /// <summary>
    /// Нажать "Add to cart" у первого товара в карусели рекомендованных товаров
    /// </summary>
    public Task AddRecommendedProductToCartAsync() => RecommendedAddToCartFirst.ClickAsync();

    /// <summary>
    /// Прокрутить страницу до самого низа
    /// </summary>
    public Task ScrollToBottomAsync() => Page.EvaluateAsync("window.scrollTo(0, document.body.scrollHeight)");

    /// <summary>
    /// Кликнуть по стрелке "Scroll Up" в правом нижнем углу
    /// </summary>
    public Task ClickScrollUpArrowAsync() => ScrollUpButton.ClickAsync();

    /// <summary>
    /// Проверить, что страница прокручена в самый верх (window.scrollY == 0)
    /// </summary>
    public async Task<bool> IsScrolledToTopAsync()
    {
        // Ждём, пока скролл вернётся в 0 (анимация scrollUp)
        await Page.WaitForFunctionAsync("() => window.scrollY === 0");
        var y = await Page.EvaluateAsync<int>("() => window.scrollY");
        return y == 0;
    }
}
