using Microsoft.Playwright;

namespace Core;

public class PlaywrightDriver : IAsyncDisposable
{
    private readonly IBrowser _browser;
    private readonly IPlaywright _playwright;

    /// <summary>
    /// Инициализировать драйвер с готовыми экземплярами Playwright и браузера
    /// </summary>
    /// <param name="playwright">Экземпляр Playwright</param>
    /// <param name="browser">Запущенный браузер</param>
    private PlaywrightDriver(IPlaywright playwright, IBrowser browser)
    {
        _playwright = playwright;
        _browser = browser;
    }

    /// <summary>
    /// Создать экземпляр драйвера
    /// </summary>
    public static async Task<PlaywrightDriver> CreateAsync()
    {
        var settings = ConfigurationProvider.Settings;
        var playwright = await Playwright.CreateAsync();

        var launchOptions = new BrowserTypeLaunchOptions
        {
            Headless = settings.Headless,
            SlowMo = settings.SlowMo
        };

        IBrowser browser = settings.Browser.ToLower() switch
        {
            "chrome" => await playwright.Chromium.LaunchAsync(launchOptions),
            "webkit" => await playwright.Webkit.LaunchAsync(launchOptions),
            _ => await playwright.Firefox.LaunchAsync(launchOptions)
        };

        return new PlaywrightDriver(playwright, browser);
    }

    /// <summary>
    /// Создать новый контекст браузера
    /// </summary>
    public async Task<IBrowserContext> CreateContextAsync()
    {
        var settings = ConfigurationProvider.Settings;

        var context = await _browser.NewContextAsync(new BrowserNewContextOptions
        {
            BaseURL = settings.BaseUrl
        });

        context.SetDefaultTimeout(settings.DefaultTimeout);
        return context;
    }

    /// <summary>
    /// Закрыть браузер и остановить Playwright
    /// </summary>
    public async ValueTask DisposeAsync()
    {
        await _browser.DisposeAsync();
        _playwright.Dispose();
    }
}
