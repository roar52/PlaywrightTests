using Microsoft.Playwright;

namespace Pages.Pages;

public class TestCasesPage : BasePage
{
    protected override string Url => "/test_cases";

    private ILocator TestCasesHeading => Page.Locator("h2.title b:has-text('Test Cases')");

    public TestCasesPage(IPage page) : base(page) { }

    /// <summary>
    /// Получить состояние отображения заголовка "TEST CASES"
    /// </summary>
    public Task<bool> IsTestCasesHeadingVisibleAsync() => TestCasesHeading.IsVisibleAsync();
}
