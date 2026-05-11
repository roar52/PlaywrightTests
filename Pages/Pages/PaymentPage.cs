using Microsoft.Playwright;

namespace Pages.Pages;

public class PaymentPage : BasePage
{
    protected override string Url => "/payment";

    private ILocator NameOnCardInput => Page.Locator("input[name='name_on_card']");
    private ILocator CardNumberInput => Page.Locator("input[name='card_number']");
    private ILocator CvcInput => Page.Locator("input[name='cvc']");
    private ILocator ExpiryMonthInput => Page.Locator("input[name='expiry_month']");
    private ILocator ExpiryYearInput => Page.Locator("input[name='expiry_year']");
    private ILocator PayAndConfirmButton => Page.Locator("button[data-qa='pay-button']");
    private ILocator OrderConfirmedMessage => Page.Locator("h2[data-qa='order-placed'], div.col-sm-9.col-sm-offset-1 h2:has-text('Order Placed')");
    private ILocator DownloadInvoiceButton => Page.Locator("a.check_out:has-text('Download Invoice')");

    public PaymentPage(IPage page) : base(page) { }

    /// <summary>
    /// Заполнить платёжные реквизиты
    /// </summary>
    /// <param name="nameOnCard">Имя владельца карты</param>
    /// <param name="cardNumber">Номер карты</param>
    /// <param name="cvc">CVC-код</param>
    /// <param name="month">Месяц окончания (MM)</param>
    /// <param name="year">Год окончания (YYYY)</param>
    public async Task FillPaymentDetailsAsync(string nameOnCard, string cardNumber, string cvc, string month, string year)
    {
        await NameOnCardInput.FillAsync(nameOnCard);
        await CardNumberInput.FillAsync(cardNumber);
        await CvcInput.FillAsync(cvc);
        await ExpiryMonthInput.FillAsync(month);
        await ExpiryYearInput.FillAsync(year);
    }

    /// <summary>
    /// Нажать кнопку "Pay and Confirm Order"
    /// </summary>
    public Task ClickPayAndConfirmAsync() => PayAndConfirmButton.ClickAsync();

    /// <summary>
    /// Получить состояние отображения сообщения о подтверждении заказа (с ожиданием появления)
    /// </summary>
    public async Task<bool> IsOrderConfirmedVisibleAsync()
    {
        await OrderConfirmedMessage.First.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible });
        return await OrderConfirmedMessage.First.IsVisibleAsync();
    }

    /// <summary>
    /// Скачать инвойс заказа. Возвращает абсолютный путь к загруженному файлу.
    /// </summary>
    public async Task<string> DownloadInvoiceAsync()
    {
        var download = await Page.RunAndWaitForDownloadAsync(async () =>
        {
            await DownloadInvoiceButton.ClickAsync();
        });
        var targetPath = Path.Combine(Path.GetTempPath(), download.SuggestedFilename);
        await download.SaveAsAsync(targetPath);
        return targetPath;
    }
}
