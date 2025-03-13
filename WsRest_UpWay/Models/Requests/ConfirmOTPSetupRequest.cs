namespace WsRest_UpWay.Models.Requests;

public class ConfirmOTPSetupRequest
{
    private string code;

    public string Code
    {
        get => code;
        set => code = value ?? throw new ArgumentNullException(nameof(value));
    }
}