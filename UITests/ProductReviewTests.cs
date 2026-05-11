using NUnit.Framework;

namespace UITests;

public class ProductReviewTests : BaseTest
{
    [Test]
    [Description("21 - Добавление отзыва на товар")]
    public async Task AddReview_OnProduct_IsSubmittedSuccessfully()
    {
        await HomePage.OpenAsync();
        Assert.That(await HomePage.IsSliderVisibleAsync(), Is.True, "Слайдер на главной странице не отобразился, а должен был");

        await HomePage.ClickProductsAsync();
        Assert.That(await ProductsPage.IsAllProductsVisibleAsync(), Is.True, "Заголовок 'ALL PRODUCTS' не отобразился, а должен был");

        await ProductsPage.ClickViewFirstProductAsync();
        Assert.That(await ProductDetailsPage.IsWriteReviewSectionVisibleAsync(), Is.True, "Секция 'Write Your Review' не отобразилась, а должна была");

        await ProductDetailsPage.FillReviewAsync(
            name: User.Name,
            email: User.Email,
            review: "Great product! Test review."
        );
        await ProductDetailsPage.ClickSubmitReviewAsync();

        Assert.That(await ProductDetailsPage.IsReviewSuccessMessageVisibleAsync(), Is.True, "Сообщение 'Thank you for your review.' не отобразилось, а должно было");
    }
}
