using NUnit.Framework;

namespace UITests;

public class RegisterTests : BaseTest
{
    [Test]
    [Description("1 - Регистрация пользователя")]
    public async Task Register_NewUserShouldBeRegistered()
    {
        await HomePage.OpenAsync();
        Assert.That(await HomePage.IsSliderVisibleAsync(), Is.True);

        await HomePage.ClickSignupLoginAsync();
        Assert.That(await LoginPage.IsNewUserSignupVisibleAsync(), Is.True);

        await LoginPage.SignupAsync(User.Name, User.Email);
        Assert.That(await SignUpPage.IsEnterAccountInfoVisibleAsync(), Is.True);

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
        Assert.That(await SignUpPage.IsAccountCreatedVisibleAsync(), Is.True);

        await SignUpPage.ClickContinueAsync();
        Assert.That(await HomePage.IsLoggedInAsync(), Is.True);

        await HomePage.ClickDeleteAccountAsync();
        Assert.That(await AccountPage.IsAccountDeletedVisibleAsync(), Is.True);
        await AccountPage.ClickContinueAsync();
    }
}
