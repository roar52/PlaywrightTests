using NUnit.Framework;

namespace UITests;

public class SubscriptionInCartTests : BaseTest
{
    [Test]
    [Description("11 - Подписка на рассылку со страницы корзины")]
    public async Task Subscription_OnCartPage_IsSuccessful()
    {
        await HomePage.OpenAsync();
        Assert.That(await HomePage.IsSliderVisibleAsync(), Is.True, "Слайдер на главной странице не отобразился, а должен был");

        await HomePage.ClickCartAsync();
        Assert.That(await CartPage.IsCartPageVisibleAsync(), Is.True, "Страница корзины не отобразилась, а должна была");

        await CartPage.ScrollToFooterAsync();
        Assert.That(await CartPage.IsSubscriptionHeadingVisibleAsync(), Is.True, "Заголовок 'SUBSCRIPTION' в футере страницы корзины не отобразился, а должен был");

        await CartPage.SubscribeAsync(User.Email);
        Assert.That(await CartPage.IsSubscribeSuccessVisibleAsync(), Is.True, "Сообщение об успешной подписке на странице корзины не отобразилось, а должно было");
    }
}
