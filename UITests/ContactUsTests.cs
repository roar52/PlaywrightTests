using NUnit.Framework;

namespace UITests;

public class ContactUsTests : BaseTest
{
    [Test]
    [Description("6 - Отправка формы Contact Us")]
    public async Task ContactUs_FormIsSubmittedSuccessfully()
    {
        await HomePage.OpenAsync();
        Assert.That(await HomePage.IsSliderVisibleAsync(), Is.True, "Слайдер на главной странице не отобразился, а должен был");

        await HomePage.ClickContactUsAsync();
        Assert.That(await ContactUsPage.IsGetInTouchVisibleAsync(), Is.True, "Заголовок 'GET IN TOUCH' не отобразился, а должен был");

        await ContactUsPage.FillFormAsync(
            name: User.Name,
            email: User.Email,
            subject: "Test subject",
            message: "Test message from automated test"
        );

        var filePath = Path.Combine(AppContext.BaseDirectory, "appsettings.json");
        await ContactUsPage.UploadFileAsync(filePath);

        await ContactUsPage.ClickSubmitAsync();
        Assert.That(await ContactUsPage.IsSuccessMessageVisibleAsync(), Is.True, "Сообщение об успешной отправке формы Contact Us не отобразилось, а должно было");

        await HomePage.ClickHomeAsync();
        Assert.That(await HomePage.IsSliderVisibleAsync(), Is.True, "Слайдер на главной странице не отобразился после возврата с Contact Us");
    }
}
