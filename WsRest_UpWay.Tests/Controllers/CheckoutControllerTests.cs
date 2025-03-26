using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Braintree;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WsRest_UpWay.Models;
using WsRest_UpWay.Models.EntityFramework;
using WsRest_UpWay.Models.Repository;
using WsRest_UpWay.Models.Requests;
using WsRest_UpWay.Models.Responses;
using Environment = Braintree.Environment;

namespace WsRest_UpWay.Controllers.Tests;

[TestClass]
[TestSubject(typeof(CheckoutController))]
public class CheckoutControllerTests
{
    private IConfiguration _config;
    private BraintreeGateway _gateway;

    [TestInitialize]
    public void InitializeTests()
    {
        _config = new ConfigurationManager().AddJsonFile("appsettings.Development.json").AddEnvironmentVariables()
            .Build();
        _config["JWT_SECRET_KEY"] = "c8qWCt7UFn3DASzZHD0vBhNcpNlZsQeszbpYkw28Cb3BLkzf7By6VPHQSnd0iMg";
        _config["JWT_ISSUER"] = "http://localhost:5194/";
        _config["JWT_AUDIENCE"] = "http://localhost:5194/";

        _gateway = new BraintreeGateway(Environment.ParseEnvironment(_config["BRAINTREE_ENV"]),
            _config["BRAINTREE_MERCHANT_ID"], _config["BRAINTREE_PUBLIC_KEY"], _config["BRAINTREE_PRIVATE_KEY"]);
    }

    [TestMethod]
    public void GetTokenTest()
    {
        var user = new CompteClient
        {
            ClientId = 1,
            LoginClient = "jean.patrick",
            EmailClient = "jean.patrick@gmail.com",
            PrenomClient = "Jean",
            NomClient = "Patrick",
            MotDePasseClient = new PasswordHasher<CompteClient>().HashPassword(null, "Jean@Patrick!"),
            Usertype = Policies.User
        };

        var mockRepo = new Mock<IDataRepository<Panier>>();
        var mockCtx = new Mock<S215UpWayContext>();
        var controller = new CheckoutController(mockRepo.Object, _gateway, mockCtx.Object);
        var jwt = new JwtSecurityToken(user.GenerateJwtToken(_config));
        controller.ControllerContext.HttpContext = new DefaultHttpContext
        {
            User = new ClaimsPrincipal(new ClaimsIdentity(jwt.Claims))
        };

        var res = controller.GetToken().Result;
        Assert.IsNotNull(res);
        Assert.IsNotNull(res.Result);
        Assert.IsInstanceOfType<OkObjectResult>(res.Result);
        var res2 = (OkObjectResult)res.Result;
        Assert.IsNotNull(res2.Value);
        Assert.IsInstanceOfType<GetTokenResponse>(res2.Value);
        var res3 = (GetTokenResponse)res2.Value;
        Assert.IsNotNull(res3.Token);
    }

    [TestMethod]
    public void CreateOrderTest()
    {
        var user = new CompteClient
        {
            ClientId = 1,
            LoginClient = "jean.patrick",
            EmailClient = "jean.patrick@gmail.com",
            PrenomClient = "Jean",
            NomClient = "Patrick",
            MotDePasseClient = new PasswordHasher<CompteClient>().HashPassword(null, "Jean@Patrick!"),
            Usertype = Policies.User
        };

        var address_exp = new AdresseExpedition
        {
            AdresseExpeId = 1,
            ClientId = user.ClientId,
            PaysExpedition = "France",
            RueExpedition = "32 route du petit lutin",
            CPExpedition = "23456",
            RegionExpedition = "NordDeFrance",
            VilleExpedition = "Corse",
            TelephoneExpedition = "+3306123456789",
            DonneesSauvegardees = false
        };

        var address_fact = new AdresseFacturation
        {
            ClientId = user.ClientId,
            AdresseFactId = 1,
            AdresseExpId = address_exp.AdresseExpeId,
            PaysFacturation = "France",
            RueFacturation = "32 route du petit lutin",
            CPFacturation = "23456",
            RegionFacturation = "NordDeFrance",
            VilleFacturation = "Corse",
            TelephoneFacturation = "+3306123456789"
        };

        var command = new DetailCommande
        {
            ClientId = user.ClientId,
            AdresseFactId = address_fact.AdresseExpId,
            CommandeId = 1,
            DateAchat = DateTime.Now,
            EtatCommandeId = 1,
            RetraitMagasinId = null,
            PanierId = null,
            MoyenPaiement = "Carte Banquaire",
            ModeExpedition = "Expeditiuon"
        };

        var order = new Panier
        {
            PanierId = 1,
            ClientId = user.ClientId,
            CommandeId = command.CommandeId,
            PrixPanier = (decimal)69.69,
            Cookie = null
        };

        var ligne = new LignePanier
        {
            PanierId = order.PanierId,
            PrixQuantite = (decimal)order.PrixPanier,
            QuantitePanier = 1,
            AssuranceId = 1,
            VeloId = 1
        };

        order.ListeLignePaniers = new List<LignePanier>();
        order.ListeLignePaniers.Add(ligne);

        var mockRepo = new Mock<IDataRepository<Panier>>();
        mockRepo.Setup(x => x.GetByIdAsync(order.PanierId)).ReturnsAsync(order);
        var mockCtx = new Mock<S215UpWayContext>();
        var controller = new CheckoutController(mockRepo.Object, _gateway, mockCtx.Object);
        var jwt = new JwtSecurityToken(user.GenerateJwtToken(_config));
        controller.ControllerContext.HttpContext = new DefaultHttpContext
        {
            User = new ClaimsPrincipal(new ClaimsIdentity(jwt.Claims))
        };

        var customer = _gateway.Customer.Create(new CustomerRequest
        {
            FirstName = "Jean",
            LastName = "Patrick",
            Company = "MachinTruc",
            Email = "jean.patrick@machin-truc.com",
            Phone = "0612345678",
            Fax = "0612345678",
            Website = "https://machin-truc.com"
        });
        Assert.IsTrue(customer.IsSuccess());

        var cb = _gateway.CreditCard.Create(new CreditCardRequest
        {
            CustomerId = customer.Target.Id,
            Number = "4111111111111111",
            ExpirationDate = "06/22",
            CVV = "100"
        });
        Assert.IsTrue(cb.IsSuccess());

        var nonce = _gateway.PaymentMethodNonce.Create(cb.Target.Token);
        Assert.IsTrue(nonce.IsSuccess());

        var res = controller.CreateOrder(new CreateOrderRequest
        {
            OrderNumber = order.PanierId,
            PaymentMethodNonce = nonce.Target.Nonce
        }).Result;

        Assert.IsNotNull(res);
        Assert.IsNotNull(res.Result);
        Assert.IsInstanceOfType<OkObjectResult>(res.Result);
        var res2 = (OkObjectResult)res.Result;
        Assert.IsNotNull(res2.Value);
        Assert.IsInstanceOfType<CreateOrderResponse>(res2.Value);
        var res3 = (CreateOrderResponse)res2.Value;
        Assert.IsNotNull(res3.TransactionId);

        var transaction = _gateway.Transaction.Find(res3.TransactionId);
        Assert.IsNotNull(transaction);
    }
}