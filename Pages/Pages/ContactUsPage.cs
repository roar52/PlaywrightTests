using Microsoft.Playwright;

namespace Pages.Pages;

public class ContactUsPage : BasePage
{
    protected override string Url => "/contact_us";

    private ILocator GetInTouchHeading => Page.Locator("div.contact-form h2.title");
    private ILocator NameInput => Page.Locator("input[data-qa='name']");
    private ILocator EmailInput => Page.Locator("input[data-qa='email']");
    private ILocator SubjectInput => Page.Locator("input[data-qa='subject']");
    private ILocator MessageInput => Page.Locator("textarea[data-qa='message']");
    private ILocator FileUploadInput => Page.Locator("input[name='upload_file']");
    private ILocator SubmitButton => Page.Locator("input[data-qa='submit-button']");
    private ILocator SuccessMessage => Page.Locator("div.status.alert.alert-success");

    public ContactUsPage(IPage page) : base(page)
    {
        // Подписываемся на диалог сразу при создании page-объекта,
        // чтобы любой alert/confirm на странице автоматически принимался.
        Page.Dialog += (_, dialog) => dialog.AcceptAsync();
    }

    /// <summary>
    /// Получить состояние отображения заголовка "GET IN TOUCH"
    /// </summary>
    public Task<bool> IsGetInTouchVisibleAsync() => GetInTouchHeading.IsVisibleAsync();

    /// <summary>
    /// Заполнить поля формы Contact Us
    /// </summary>
    /// <param name="name">Имя отправителя</param>
    /// <param name="email">Email отправителя</param>
    /// <param name="subject">Тема обращения</param>
    /// <param name="message">Текст сообщения</param>
    public async Task FillFormAsync(string name, string email, string subject, string message)
    {
        await NameInput.FillAsync(name);
        await EmailInput.FillAsync(email);
        await SubjectInput.FillAsync(subject);
        await MessageInput.FillAsync(message);
    }

    /// <summary>
    /// Прикрепить файл к форме Contact Us
    /// </summary>
    /// <param name="filePath">Абсолютный путь к загружаемому файлу</param>
    public Task UploadFileAsync(string filePath) => FileUploadInput.SetInputFilesAsync(filePath);

    /// <summary>
    /// Нажать кнопку "Submit". Браузерный alert принимается автоматически
    /// через подписчик Page.Dialog, зарегистрированный в конструкторе.
    /// После клика дожидаемся появления success-сообщения.
    /// </summary>
    public async Task ClickSubmitAsync()
    {
        await SubmitButton.ClickAsync();
        await SuccessMessage.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible });
    }

    /// <summary>
    /// Получить состояние отображения сообщения "Success! Your details have been submitted successfully."
    /// </summary>
    public Task<bool> IsSuccessMessageVisibleAsync() => SuccessMessage.IsVisibleAsync();
}
