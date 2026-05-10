using NUnit.Framework;

namespace UITests;

public class TestCasesPageTests : BaseTest
{
    [Test]
    [Description("7 - Переход на страницу Test Cases")]
    public async Task TestCases_PageIsOpenedSuccessfully()
    {
        await HomePage.OpenAsync();
        Assert.That(await HomePage.IsSliderVisibleAsync(), Is.True, "Слайдер на главной странице не отобразился, а должен был");

        await HomePage.ClickTestCasesAsync();
        Assert.That(await TestCasesPage.IsTestCasesHeadingVisibleAsync(), Is.True, "Заголовок страницы 'TEST CASES' не отобразился, а должен был");
    }
}
