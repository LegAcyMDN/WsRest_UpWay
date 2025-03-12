namespace WsRest_UpWay.Models.Responses;

public class UserAuthResponse
{
    public string? Token { get; set; }

    public string? Message { get; set; }

    public static UserAuthResponse Success(string token)
    {
        return new UserAuthResponse { Token = token };
    }

    public static UserAuthResponse Error(string message)
    {
        return new UserAuthResponse { Message = message };
    }
}