using Microsoft.Extensions.Configuration;

namespace Core;

public static class ConfigurationProvider
{
    private static readonly TestSettings _settings;

    static ConfigurationProvider()
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
            .Build();

        _settings = config.GetSection("TestSettings").Get<TestSettings>()
                    ?? throw new InvalidOperationException("Секция TestSettings не найдена в appsettings.json");
    }

    /// <summary>
    /// Получить настройки
    /// </summary>
    public static TestSettings Settings => _settings;
}
