namespace WsRest_UpWay.Models.Responses;

public class SetupOTPResponse
{
    public string? Secret { get; set; }

    public string? Message { get; set; }

    public static SetupOTPResponse Success(string secret)
    {
        return new SetupOTPResponse { Secret = secret };
    }

    public static SetupOTPResponse Error(string message)
    {
        return new SetupOTPResponse { Message = message };
    }
}