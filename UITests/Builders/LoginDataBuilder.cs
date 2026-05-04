using UITests.Models;

namespace UITests.Builders;

public class LoginDataBuilder
{
    private string? _email;
    private string? _password;

    /// <summary>
    /// Задать email для входа
    /// </summary>
    /// <param name="email">Адрес электронной почты</param>
    public LoginDataBuilder WithEmail(string email) { _email = email; return this; }

    /// <summary>
    /// Задать пароль для входа
    /// </summary>
    /// <param name="password">Пароль</param>
    public LoginDataBuilder WithPassword(string password) { _password = password; return this; }

    /// <summary>
    /// Собрать объект LoginData с заданными параметрами.
    /// Требует обязательного указания email и пароля через WithEmail и WithPassword.
    /// </summary>
    /// <exception cref="InvalidOperationException">Если email или пароль не заданы</exception>
    public LoginData Build()
    {
        if (string.IsNullOrEmpty(_email))
            throw new InvalidOperationException("Email не задан. Используйте WithEmail() перед вызовом Build().");
        if (string.IsNullOrEmpty(_password))
            throw new InvalidOperationException("Пароль не задан. Используйте WithPassword() перед вызовом Build().");

        return new LoginData(_email, _password);
    }
}
