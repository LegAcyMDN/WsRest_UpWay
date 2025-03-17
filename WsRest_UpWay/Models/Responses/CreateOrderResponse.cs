namespace WsRest_UpWay.Models.Responses;

public class CreateOrderResponse
{
    public string? TransactionId { get; set; }
    public string? Message { get; set; }

    public static CreateOrderResponse Success(string transactionId)
    {
        return new CreateOrderResponse { TransactionId = transactionId };
    }

    public static CreateOrderResponse Error(string message)
    {
        return new CreateOrderResponse { Message = message };
    }
}