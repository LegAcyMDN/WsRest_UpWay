namespace WsRest_UpWay.Models.Requests;

public class UserRegistrationRequest
{
    private string login;
    private string password;
    private string email;
    private string first_name;
    private string last_name;

    public UserRegistrationRequest(string login, string password, string email, string firstName, string lastName)
    {
        this.Login = login;
        this.Password = password;
        this.email = email;
        this.FirstName = firstName;
        this.LastName = lastName;
    }

    public UserRegistrationRequest()
    {
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

    public string Email
    {
        get => email;
        set => email = value ?? throw new ArgumentNullException(nameof(value));
    }

    public string FirstName
    {
        get => first_name;
        set => first_name = value ?? throw new ArgumentNullException(nameof(value));
    }

    public string LastName
    {
        get => last_name;
        set => last_name = value ?? throw new ArgumentNullException(nameof(value));
    }
}