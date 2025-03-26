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
    private readonly S215UpWayContext ctx;
    private readonly BraintreeGateway gateway;
    private readonly IDataRepository<Panier> orderManager;

    public CheckoutController(IDataRepository<Panier> orderManager, BraintreeGateway gateway, S215UpWayContext ctx)
    {
        this.orderManager = orderManager;
        this.gateway = gateway;
        this.ctx = ctx;
    }

    /// <summary>
    ///     R�cup�re le token de client pour Braintree.
    /// </summary>
    /// <returns>Http response</returns>
    /// <response code="200">Lorsque le token est r�cup�r� avec succ�s.</response>
    [HttpGet("get-token")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [Authorize(Policy = Policies.User)]
    public async Task<ActionResult<GetTokenResponse>> GetToken()
    {
        var token = await gateway.ClientToken.GenerateAsync();
        return Ok(new GetTokenResponse { Token = token });
    }

    /// <summary>
    ///     Cr�e une commande et effectue une transaction.
    /// </summary>
    /// <param name="body">Les informations de la commande � cr�er.</param>
    /// <returns>Http response</returns>
    /// <response code="200">Lorsque la commande est cr��e avec succ�s.</response>
    /// <response code="400">Lorsque la commande n'est pas trouv�e ou en cas d'erreur de transaction.</response>
    /// <response code="401">Lorsque l'utilisateur n'est pas autoris� � effectuer cette commande.</response>
    [HttpPost("create")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [Authorize(Policy = Policies.User)]
    public async Task<ActionResult<CreateOrderResponse>> CreateOrder([FromBody] CreateOrderRequest body)
    {
        var order = (await orderManager.GetByIdAsync(body.OrderNumber)).Value;
        if (order == null)
            return BadRequest(CreateOrderResponse.Error("Order not found"));

        if (order.ClientId != User.GetId())
            return Unauthorized();

        decimal price = 0;

        try
        {
            ctx.Entry(order).Collection(e => e.ListeLignePaniers).Load();
        }
        catch (Exception _)
        {
        } // Unit tests cause this to return null

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

    /// <summary>
    ///     Affiche les d�tails d'une transaction.
    /// </summary>
    /// <param name="body">Les informations de la transaction � afficher.</param>
    /// <returns>Http response</returns>
    /// <response code="200">Lorsque les d�tails de la transaction sont r�cup�r�s avec succ�s.</response>
    /// <response code="400">Lorsque l'identifiant de la transaction n'est pas reconnu.</response>
    [HttpPost("show")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize(Policy = Policies.User)]
    public async Task<ActionResult<ShowTransactionResponse>> ShowTransaction([FromBody] ShowTransactionRequest body)
    {
        var transaction = await gateway.Transaction.FindAsync(body.TransactionId);
        if (transaction == null) return BadRequest(new ShowTransactionResponse(TransactionStatus.UNRECOGNIZED));

        return new ShowTransactionResponse(transaction.Status, transaction);
    }
}