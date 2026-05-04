using Microsoft.Playwright;

namespace Pages.Pages;

public class HomePage : BasePage
{
    protected override string Url => "/";

    private ILocator Slider => Page.Locator("#slider-carousel");

    public HomePage(IPage page) : base(page) { }

    /// <summary>
    /// Получить состояние отображения слайдера
    /// </summary>
    public Task<bool> IsSliderVisibleAsync() => Slider.IsVisibleAsync();
}
