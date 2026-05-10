using Microsoft.Playwright;

namespace Pages.Pages;

public class HomePage : BasePage
{
    protected override string Url => "/";

    private ILocator Slider => Page.Locator("#slider-carousel");

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
}
