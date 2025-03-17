namespace WsRest_UpWay.Models.Requests;

public class CreateOrderRequest
{
    public int OrderNumber { get; set; }
    public string PaymentMethodNonce { get; set; }
}