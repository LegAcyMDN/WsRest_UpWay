using Braintree;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WsRest_UpWay.Helpers;
using WsRest_UpWay.Models;
using WsRest_UpWay.Models.EntityFramework;
using WsRest_UpWay.Models.Repository;
using WsRest_UpWay.Models.Requests;
using WsRest_UpWay.Models.Responses;

namespace WsRest_UpWay.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CheckoutController : ControllerBase
{
    private readonly IDataRepository<Panier> orderManager;
    private readonly BraintreeGateway gateway;

    public CheckoutController(IDataRepository<Panier> orderManager, BraintreeGateway gateway)
    {
        this.orderManager = orderManager;
        this.gateway = gateway;
    }

    [HttpGet("get-token")]
    [Authorize(Policy = Policies.User)]
    public async Task<ActionResult<GetTokenResponse>> GetToken()
    {
        var token = await gateway.ClientToken.GenerateAsync();
        return Ok(new GetTokenResponse { Token = token });
    }

    [HttpPost("create")]
    [Authorize(Policy = Policies.User)]
    public async Task<ActionResult<CreateOrderResponse>> CreateOrder([FromBody] CreateOrderRequest body)
    {
        var order = (await orderManager.GetByIdAsync(body.OrderNumber)).Value;
        if (order == null)
            return BadRequest(CreateOrderResponse.Error("Order not found"));

        if (order.ClientId != User.GetId())
            return Unauthorized();

        decimal price = 0;

        foreach (var lignePanier in order.ListeLignePaniers)
            price += lignePanier.QuantitePanier * lignePanier.PrixQuantite;

        var transactionRequest = new TransactionRequest
        {
            Amount = price,
            PaymentMethodNonce = body.PaymentMethodNonce,
            Options = new TransactionOptionsRequest
            {
                SubmitForSettlement = true
            }
        };

        Result<Transaction> res = await gateway.Transaction.SaleAsync(transactionRequest);
        if (res.IsSuccess()) return Ok(CreateOrderResponse.Success(res.Target.Id));

        if (res.Transaction != null) return Ok(CreateOrderResponse.Success(res.Transaction.Id));

        return BadRequest(CreateOrderResponse.Error(res.Message));
    }

    [HttpPost("show")]
    [Authorize(Policy = Policies.User)]
    public async Task<ActionResult<ShowTransactionResponse>> ShowTransaction([FromBody] ShowTransactionRequest body)
    {
        var transaction = await gateway.Transaction.FindAsync(body.TransactionId);
        if (transaction == null) return BadRequest(new ShowTransactionResponse(TransactionStatus.UNRECOGNIZED));

        return new ShowTransactionResponse(transaction.Status, transaction);
    }
}