using NUnit.Framework;

namespace UITests;

public class SubscriptionTests : BaseTest
{
    [Test]
    [Description("10 - Подписка на рассылку с главной страницы")]
    public async Task Subscription_OnHomePage_IsSuccessful()
    {
        await HomePage.OpenAsync();
        Assert.That(await HomePage.IsSliderVisibleAsync(), Is.True, "Слайдер на главной странице не отобразился, а должен был");

        await HomePage.ScrollToFooterAsync();
        Assert.That(await HomePage.IsSubscriptionHeadingVisibleAsync(), Is.True, "Заголовок 'SUBSCRIPTION' в футере не отобразился, а должен был");

        await HomePage.SubscribeAsync(User.Email);
        Assert.That(await HomePage.IsSubscribeSuccessVisibleAsync(), Is.True, "Сообщение об успешной подписке не отобразилось, а должно было");
    }
}
