using UITests.Models;

namespace UITests.Builders;

public class UserBuilder
{
    private string _name = TestConstants.NewUser.Name;
    private string _email = $"tester_{Guid.NewGuid():N}@mailinator.com";
    private string _password = TestConstants.NewUser.Password;
    private string _title = TestConstants.NewUser.Title;
    private string _day = TestConstants.NewUser.Day;
    private string _month = TestConstants.NewUser.Month;
    private string _year = TestConstants.NewUser.Year;
    private string _firstName = TestConstants.NewUser.FirstName;
    private string _lastName = TestConstants.NewUser.LastName;
    private string _company = TestConstants.NewUser.Company;
    private string _address = TestConstants.NewUser.Address;
    private string _address2 = TestConstants.NewUser.Address2;
    private string _country = TestConstants.NewUser.Country;
    private string _state = TestConstants.NewUser.State;
    private string _city = TestConstants.NewUser.City;
    private string _zipcode = TestConstants.NewUser.Zipcode;
    private string _mobile = TestConstants.NewUser.Mobile;

    /// <summary>
    /// Собрать объект UserData с заданными параметрами
    /// </summary>
    public UserData Build() => new UserData(
        Name: _name,
        Email: _email,
        Password: _password,
        Title: _title,
        Day: _day,
        Month: _month,
        Year: _year,
        FirstName: _firstName,
        LastName: _lastName,
        Company: _company,
        Address: _address,
        Address2: _address2,
        Country: _country,
        State: _state,
        City: _city,
        Zipcode: _zipcode,
        Mobile: _mobile
    );
}
