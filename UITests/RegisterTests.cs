using NUnit.Framework;

namespace UITests;

public class RegisterTests : BaseTest
{
    protected override async Task OnTearDownAsync()
    {
        await AuthHelper.DeleteAccountAsync();
    }

    [Test]
    [Description("1 - Регистрация пользователя")]
    public async Task Register_NewUserShouldBeRegistered()
    {
        await HomePage.OpenAsync();
        Assert.That(await HomePage.IsSliderVisibleAsync(), Is.True, "Слайдер на главной странице не отобразился, а должен был");

        await HomePage.ClickSignupLoginAsync();
        Assert.That(await LoginPage.IsNewUserSignupVisibleAsync(), Is.True, "Форма 'New User Signup!' не отобразилась, а должна была");

        await LoginPage.SignupAsync(User.Name, User.Email);
        Assert.That(await SignUpPage.IsEnterAccountInfoVisibleAsync(), Is.True, "Форма 'Enter Account Information' не отобразилась, а должна была");

        await SignUpPage.SelectTitleAsync(User.Title);
        await SignUpPage.FillPasswordAsync(User.Password);
        await SignUpPage.SelectDateOfBirthAsync(User.Day, User.Month, User.Year);

        await SignUpPage.CheckNewsletterAsync();
        await SignUpPage.CheckSpecialOffersAsync();

        await SignUpPage.FillAddressDetailsAsync(
            firstName: User.FirstName,
            lastName: User.LastName,
            company: User.Company,
            address: User.Address,
            address2: User.Address2,
            country: User.Country,
            state: User.State,
            city: User.City,
            zipcode: User.Zipcode,
            mobile: User.Mobile
        );

        await SignUpPage.ClickCreateAccountAsync();
        Assert.That(await SignUpPage.IsAccountCreatedVisibleAsync(), Is.True, "Сообщение 'Account Created!' не отобразилось, а должно было");

        await SignUpPage.ClickContinueAsync();
        Assert.That(await HomePage.IsLoggedInAsync(), Is.True, "Индикатор 'Logged in as' не отобразился — пользователь не авторизован после регистрации");
    }
}
