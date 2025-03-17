using Braintree;

namespace WsRest_UpWay.Models.Responses;

public class ShowTransactionResponse
{
    public ShowTransactionResponse(TransactionStatus status, Transaction? transaction)
    {
        Status = status;
        Transaction = transaction;
    }

    public ShowTransactionResponse(TransactionStatus status) : this(status, null)
    {
    }

    public TransactionStatus Status { get; set; }
    public Transaction? Transaction { get; set; }
}