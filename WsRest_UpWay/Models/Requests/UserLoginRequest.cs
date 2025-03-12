namespace WsRest_UpWay.Models.Requests;

public class UserLoginRequest
{
    private string login;
    private string password;

    public UserLoginRequest()
    {
    }

    public UserLoginRequest(string login, string password)
    {
        this.Login = login;
        this.Password = password;
    }

    public string Login
    {
        get => login;
        set => login = value ?? throw new ArgumentNullException(nameof(value));
    }

    public string Password
    {
        get => password;
        set => password = value ?? throw new ArgumentNullException(nameof(value));
    }
}