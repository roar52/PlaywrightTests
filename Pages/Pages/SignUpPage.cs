using Microsoft.Playwright;

namespace Pages.Pages;

public class SignUpPage : BasePage
{
    protected override string Url => "/signup";

    #region Информация об аккаунте
    private ILocator EnterAccountInfoHeading => Page.Locator("h2.title:has-text('Enter Account Information')");
    private ILocator TitleMr => Page.Locator("input#id_gender1");
    private ILocator TitleMrs => Page.Locator("input#id_gender2");
    private ILocator PasswordInput => Page.Locator("input[data-qa='password']");
    private ILocator DaysSelect => Page.Locator("select[data-qa='days']");
    private ILocator MonthsSelect => Page.Locator("select[data-qa='months']");
    private ILocator YearsSelect => Page.Locator("select[data-qa='years']");
    private ILocator NewsletterCheckbox => Page.Locator("input#newsletter");
    private ILocator SpecialOffersCheckbox => Page.Locator("input#optin");
    #endregion

    #region Адресные данные
    private ILocator FirstNameInput => Page.Locator("input[data-qa='first_name']");
    private ILocator LastNameInput => Page.Locator("input[data-qa='last_name']");
    private ILocator CompanyInput => Page.Locator("input[data-qa='company']");
    private ILocator AddressInput => Page.Locator("input[data-qa='address']");
    private ILocator Address2Input => Page.Locator("input[data-qa='address2']");
    private ILocator CountrySelect => Page.Locator("select[data-qa='country']");
    private ILocator StateInput => Page.Locator("input[data-qa='state']");
    private ILocator CityInput => Page.Locator("input[data-qa='city']");
    private ILocator ZipcodeInput => Page.Locator("input[data-qa='zipcode']");
    private ILocator MobileInput => Page.Locator("input[data-qa='mobile_number']");
    #endregion

    #region Кнопки и подтверждение
    private ILocator CreateAccountButton => Page.Locator("button[data-qa='create-account']");
    private ILocator AccountCreatedHeading => Page.Locator("h2.title:has-text('Account Created!')");
    private ILocator ContinueButton => Page.Locator("a[data-qa='continue-button']");
    #endregion

    public SignUpPage(IPage page) : base(page) { }

    /// <summary>
    /// Получить состояние отображения заголовка "Enter Account Information"
    /// </summary>
    public Task<bool> IsEnterAccountInfoVisibleAsync() => EnterAccountInfoHeading.IsVisibleAsync();

    /// <summary>
    /// Выбрать обращение: "Mr" или "Mrs"
    /// </summary>
    /// <param name="title">Обращение: "Mr" или "Mrs"</param>
    public async Task SelectTitleAsync(string title)
    {
        if (title == "Mr") await TitleMr.CheckAsync();
        else await TitleMrs.CheckAsync();
    }

    /// <summary>
    /// Заполнить поле пароля
    /// </summary>
    /// <param name="password">Пароль</param>
    public Task FillPasswordAsync(string password) => PasswordInput.FillAsync(password);

    /// <summary>
    /// Выбрать дату рождения: день, месяц, год
    /// </summary>
    /// <param name="day">День (числовое значение, например "15")</param>
    /// <param name="month">Месяц (числовое значение, например "5")</param>
    /// <param name="year">Год (например "1990")</param>
    public async Task SelectDateOfBirthAsync(string day, string month, string year)
    {
        await DaysSelect.SelectOptionAsync(day);
        await MonthsSelect.SelectOptionAsync(month);
        await YearsSelect.SelectOptionAsync(year);
    }

    /// <summary>
    /// Отметить чекбокс "Sign up for our newsletter!"
    /// </summary>
    public Task CheckNewsletterAsync() => NewsletterCheckbox.CheckAsync();

    /// <summary>
    /// Отметить чекбокс "Receive special offers from our partners!"
    /// </summary>
    public Task CheckSpecialOffersAsync() => SpecialOffersCheckbox.CheckAsync();

    /// <summary>
    /// Заполнить адресные данные: имя, фамилия, компания, адрес, страна, город и т.д.
    /// </summary>
    /// <param name="firstName">Имя</param>
    /// <param name="lastName">Фамилия</param>
    /// <param name="company">Название компании</param>
    /// <param name="address">Основной адрес</param>
    /// <param name="address2">Дополнительный адрес</param>
    /// <param name="country">Страна</param>
    /// <param name="state">Штат / регион</param>
    /// <param name="city">Город</param>
    /// <param name="zipcode">Почтовый индекс</param>
    /// <param name="mobile">Номер мобильного телефона</param>
    public async Task FillAddressDetailsAsync(string firstName, string lastName, string company,
        string address, string address2, string country, string state,
        string city, string zipcode, string mobile)
    {
        await FirstNameInput.FillAsync(firstName);
        await LastNameInput.FillAsync(lastName);
        await CompanyInput.FillAsync(company);
        await AddressInput.FillAsync(address);
        await Address2Input.FillAsync(address2);
        await CountrySelect.SelectOptionAsync(country);
        await StateInput.FillAsync(state);
        await CityInput.FillAsync(city);
        await ZipcodeInput.FillAsync(zipcode);
        await MobileInput.FillAsync(mobile);
    }

    /// <summary>
    /// Нажать кнопку "Create Account"
    /// </summary>
    public Task ClickCreateAccountAsync() => CreateAccountButton.ClickAsync();

    /// <summary>
    /// Получить состояние отображения заголовка "Account Created!"
    /// </summary>
    public Task<bool> IsAccountCreatedVisibleAsync() => AccountCreatedHeading.IsVisibleAsync();

    /// <summary>
    /// Нажать кнопку "Continue"
    /// </summary>
    public Task ClickContinueAsync() => ContinueButton.ClickAsync();
}
