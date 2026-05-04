namespace Core;

/// <summary>
/// Модель настроек тестов, считываемых из секции TestSettings в appsettings.json
/// </summary>
public class TestSettings
{
    public string BaseUrl { get; set; } = null!;
    public string Browser { get; set; } = null!;
    public bool Headless { get; set; }
    public int SlowMo { get; set; }
    public int DefaultTimeout { get; set; }
}
