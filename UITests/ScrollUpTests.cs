using NUnit.Framework;

namespace UITests;

public class ScrollUpTests : BaseTest
{
    [Test]
    [Description("25 - Прокрутка страницы вверх через стрелку")]
    public async Task ScrollUp_ArrowButton_ReturnsPageToTop()
    {
        await HomePage.OpenAsync();
        Assert.That(await HomePage.IsSliderVisibleAsync(), Is.True, "Слайдер на главной странице не отобразился, а должен был");

        await HomePage.ScrollToBottomAsync();
        Assert.That(await HomePage.IsSubscriptionHeadingVisibleAsync(), Is.True, "Заголовок 'SUBSCRIPTION' в футере не отобразился после прокрутки вниз");

        await HomePage.ClickScrollUpArrowAsync();
        Assert.That(await HomePage.IsScrolledToTopAsync(), Is.True, "После клика по стрелке страница не прокрутилась в самый верх");
        Assert.That(await HomePage.IsSliderVisibleAsync(), Is.True, "Слайдер на главной странице не отобразился после прокрутки наверх");
    }
}
