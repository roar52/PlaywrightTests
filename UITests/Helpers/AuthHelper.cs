using Pages.Pages;
using UITests.Models;

namespace UITests.Helpers;

public class AuthHelper
{
    private readonly HomePage _homePage;
    private readonly LoginPage _loginPage;
    private readonly SignUpPage _signUpPage;
    private readonly AccountPage _accountPage;
    private readonly UserData _user;
    
    public AuthHelper(HomePage homePage, LoginPage loginPage, SignUpPage signUpPage, AccountPage accountPage, UserData user)
    {
        _homePage = homePage;
        _loginPage = loginPage;
        _signUpPage = signUpPage;
        _accountPage = accountPage;
        _user = user;
    }

    /// <summary>
    /// Зарегистрировать пользователя и выйти из аккаунта
    /// </summary>
    public async Task RegisterAsync()
    {
        await _homePage.OpenAsync();
        await _homePage.ClickSignupLoginAsync();
        await _loginPage.SignupAsync(_user.Name, _user.Email);

        await _signUpPage.SelectTitleAsync(_user.Title);
        await _signUpPage.FillPasswordAsync(_user.Password);
        await _signUpPage.SelectDateOfBirthAsync(_user.Day, _user.Month, _user.Year);

        await _signUpPage.CheckNewsletterAsync();
        await _signUpPage.CheckSpecialOffersAsync();

        await _signUpPage.FillAddressDetailsAsync(
            firstName: _user.FirstName,
            lastName: _user.LastName,
            company: _user.Company,
            address: _user.Address,
            address2: _user.Address2,
            country: _user.Country,
            state: _user.State,
            city: _user.City,
            zipcode: _user.Zipcode,
            mobile: _user.Mobile
        );

        await _signUpPage.ClickCreateAccountAsync();
        await _signUpPage.ClickContinueAsync();
        await _homePage.ClickLogoutAsync();
        await _loginPage.WaitForLoginPageAsync();
    }

    /// <summary>
    /// Удалить аккаунт текущего пользователя
    /// </summary>
    public async Task DeleteAccountAsync()
    {
        await _homePage.OpenAsync();
        if (!await _homePage.IsLoggedInAsync())
        {
            await _homePage.ClickSignupLoginAsync();
            await _loginPage.LoginAsync(_user.Email, _user.Password);
        }

        await _homePage.ClickDeleteAccountAsync();
        await _accountPage.ClickContinueAsync();
    }
}
