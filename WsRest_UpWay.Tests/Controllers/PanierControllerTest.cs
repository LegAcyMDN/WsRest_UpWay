using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WsRest_UpWay.Controllers;
using WsRest_UpWay.Models;
using WsRest_UpWay.Models.EntityFramework;
using WsRest_UpWay.Models.Repository;

namespace WsRest_UpWay.Tests.Controllers;

[TestClass]
public class PanierControllerTest
{
    private IConfiguration _config;

    [TestInitialize]
    public void InitializeTests()
    {
        _config = new ConfigurationManager();
        _config["JWT_SECRET_KEY"] = "c8qWCt7UFn3DASzZHD0vBhNcpNlZsQeszbpYkw28Cb3BLkzf7By6VPHQSnd0iMg";
        _config["JWT_ISSUER"] = "http://localhost:5194/";
        _config["JWT_AUDIENCE"] = "http://localhost:5194/";
    }

    [TestMethod]
    public void TestGetByIdNotFoundMoq()
    {
        var panierId = 1;

        var mockRepo = new Mock<IDataPanier>();
        mockRepo.Setup(r => r.GetByIdAsync(panierId).Result).Returns(new ActionResult<Panier>((Panier)null));
        var controller = new PanierController(mockRepo.Object);

        var res = controller.GetById(panierId).Result;

        Assert.IsInstanceOfType<NotFoundResult>(res.Result);
    }

    [TestMethod]
    public void TestGetByIdNotOwnerMoq()
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

        var panier = new Panier
        {
            PanierId = 1,
            ClientId = 2
        };

        var mockRepo = new Mock<IDataPanier>();
        mockRepo.Setup(r => r.GetByIdAsync(panier.PanierId).Result).Returns(new ActionResult<Panier>(panier));
        var controller = new PanierController(mockRepo.Object);

        var jwt = new JwtSecurityToken(user.GenerateJwtToken(_config));
        controller.ControllerContext.HttpContext = new DefaultHttpContext
        {
            User = new ClaimsPrincipal(new ClaimsIdentity(jwt.Claims))
        };

        var res = controller.GetById(panier.PanierId).Result;

        Assert.IsInstanceOfType<UnauthorizedResult>(res.Result);
    }

    [TestMethod]
    public void TestGetByIdOkMoq()
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

        var panier = new Panier
        {
            PanierId = 1,
            ClientId = user.ClientId
        };

        var mockRepo = new Mock<IDataPanier>();
        mockRepo.Setup(r => r.GetByIdAsync(panier.PanierId).Result).Returns(new ActionResult<Panier>(panier));
        var controller = new PanierController(mockRepo.Object);

        var jwt = new JwtSecurityToken(user.GenerateJwtToken(_config));
        controller.ControllerContext.HttpContext = new DefaultHttpContext
        {
            User = new ClaimsPrincipal(new ClaimsIdentity(jwt.Claims))
        };

        var res = controller.GetById(panier.PanierId).Result;

        Assert.IsInstanceOfType<Panier>(res.Value);
        var panier2 = res.Value;

        Assert.AreEqual(panier, panier2);
    }

    [TestMethod]
    public void TestGetMyPanierNotFoundMoq()
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

        var mockRepo = new Mock<IDataPanier>();
        mockRepo.Setup(r => r.GetByUser(user.ClientId).Result).Returns(new ActionResult<Panier>((Panier)null));
        var controller = new PanierController(mockRepo.Object);

        var jwt = new JwtSecurityToken(user.GenerateJwtToken(_config));
        controller.ControllerContext.HttpContext = new DefaultHttpContext
        {
            User = new ClaimsPrincipal(new ClaimsIdentity(jwt.Claims))
        };

        var res = controller.GetMyPanier().Result;

        Assert.IsInstanceOfType<NotFoundResult>(res.Result);
    }

    [TestMethod]
    public void TestGetMyPanierOkMoq()
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

        var panier = new Panier
        {
            PanierId = 1,
            ClientId = user.ClientId
        };

        var mockRepo = new Mock<IDataPanier>();
        mockRepo.Setup(r => r.GetByUser(user.ClientId).Result).Returns(new ActionResult<Panier>(panier));
        var controller = new PanierController(mockRepo.Object);

        var jwt = new JwtSecurityToken(user.GenerateJwtToken(_config));
        controller.ControllerContext.HttpContext = new DefaultHttpContext
        {
            User = new ClaimsPrincipal(new ClaimsIdentity(jwt.Claims))
        };

        var res = controller.GetMyPanier().Result;

        Assert.IsInstanceOfType<Panier>(res.Value);
        var panier2 = res.Value;

        Assert.AreEqual(panier, panier2);
    }

    [TestMethod]
    public void TestPostPanierUnauthorizedMoq()
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

        var panier = new Panier
        {
            PanierId = 1,
            ClientId = 2
        };

        var mockRepo = new Mock<IDataPanier>();
        mockRepo.Setup(r => r.GetByUser(user.ClientId).Result).Returns(new ActionResult<Panier>((Panier)null));
        var controller = new PanierController(mockRepo.Object);

        var jwt = new JwtSecurityToken(user.GenerateJwtToken(_config));
        controller.ControllerContext.HttpContext = new DefaultHttpContext
        {
            User = new ClaimsPrincipal(new ClaimsIdentity(jwt.Claims))
        };

        var res = controller.PostPanier(panier).Result;

        Assert.IsInstanceOfType<UnauthorizedResult>(res.Result);
    }

    [TestMethod]
    public void TestPostPanierBadRequestMoq()
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

        var panier = new Panier
        {
            PanierId = 1,
            ClientId = user.ClientId
        };

        var mockRepo = new Mock<IDataPanier>();
        mockRepo.Setup(r => r.GetByUser(user.ClientId).Result).Returns(new ActionResult<Panier>(panier));
        var controller = new PanierController(mockRepo.Object);

        var jwt = new JwtSecurityToken(user.GenerateJwtToken(_config));
        controller.ControllerContext.HttpContext = new DefaultHttpContext
        {
            User = new ClaimsPrincipal(new ClaimsIdentity(jwt.Claims))
        };

        var res = controller.PostPanier(panier).Result;

        Assert.IsInstanceOfType<BadRequestResult>(res.Result);
    }

    [TestMethod]
    public void TestPostPanierOkMoq()
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

        var panier = new Panier
        {
            PanierId = 1,
            ClientId = user.ClientId
        };

        var mockRepo = new Mock<IDataPanier>();
        mockRepo.Setup(r => r.GetByUser(user.ClientId).Result).Returns(new ActionResult<Panier>((Panier)null));
        var controller = new PanierController(mockRepo.Object);

        var jwt = new JwtSecurityToken(user.GenerateJwtToken(_config));
        controller.ControllerContext.HttpContext = new DefaultHttpContext
        {
            User = new ClaimsPrincipal(new ClaimsIdentity(jwt.Claims))
        };

        var res = controller.PostPanier(panier).Result;

        Assert.IsInstanceOfType<CreatedAtActionResult>(res.Result);
        var res2 = (CreatedAtActionResult)res.Result;

        Assert.IsInstanceOfType<Panier>(res2.Value);
        Assert.AreEqual(panier, res2.Value);
    }

    [TestMethod]
    public void TestPutPanierIdsNotMatchingMoq()
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

        var panier = new Panier
        {
            PanierId = 1,
            ClientId = user.ClientId
        };

        var mockRepo = new Mock<IDataPanier>();
        mockRepo.Setup(r => r.GetByIdAsync(panier.PanierId).Result).Returns(new ActionResult<Panier>(panier));
        var controller = new PanierController(mockRepo.Object);

        var jwt = new JwtSecurityToken(user.GenerateJwtToken(_config));
        controller.ControllerContext.HttpContext = new DefaultHttpContext
        {
            User = new ClaimsPrincipal(new ClaimsIdentity(jwt.Claims))
        };

        var res = controller.PutPanier(2, new Panier
        {
            PanierId = panier.PanierId,
            ClientId = user.ClientId,
            Cookie = "Fondant au pépites de chocolat"
        }).Result;

        Assert.IsInstanceOfType<BadRequestResult>(res);
    }

    [TestMethod]
    public void TestPutPanierIdsNotFoundMoq()
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

        var panier = new Panier
        {
            PanierId = 1,
            ClientId = user.ClientId
        };

        var mockRepo = new Mock<IDataPanier>();
        mockRepo.Setup(r => r.GetByIdAsync(panier.PanierId).Result).Returns(new ActionResult<Panier>((Panier)null));
        var controller = new PanierController(mockRepo.Object);

        var jwt = new JwtSecurityToken(user.GenerateJwtToken(_config));
        controller.ControllerContext.HttpContext = new DefaultHttpContext
        {
            User = new ClaimsPrincipal(new ClaimsIdentity(jwt.Claims))
        };

        var res = controller.PutPanier(panier.PanierId, new Panier
        {
            PanierId = panier.PanierId,
            ClientId = user.ClientId,
            Cookie = "Fondant au pépites de chocolat"
        }).Result;

        Assert.IsInstanceOfType<NotFoundResult>(res);
    }

    [TestMethod]
    public void TestPutPanierUnauthorizedMoq()
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

        var panier = new Panier
        {
            PanierId = 1,
            ClientId = 2
        };

        var mockRepo = new Mock<IDataPanier>();
        mockRepo.Setup(r => r.GetByIdAsync(panier.PanierId).Result).Returns(new ActionResult<Panier>(panier));
        var controller = new PanierController(mockRepo.Object);

        var jwt = new JwtSecurityToken(user.GenerateJwtToken(_config));
        controller.ControllerContext.HttpContext = new DefaultHttpContext
        {
            User = new ClaimsPrincipal(new ClaimsIdentity(jwt.Claims))
        };

        var res = controller.PutPanier(panier.PanierId, new Panier
        {
            PanierId = panier.PanierId,
            ClientId = user.ClientId,
            Cookie = "Fondant au pépites de chocolat"
        }).Result;

        Assert.IsInstanceOfType<UnauthorizedResult>(res);
    }

    [TestMethod]
    public void TestPutPanierOkMoq()
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

        var panier = new Panier
        {
            PanierId = 1,
            ClientId = user.ClientId
        };

        var mockRepo = new Mock<IDataPanier>();
        mockRepo.Setup(r => r.GetByIdAsync(panier.PanierId).Result).Returns(new ActionResult<Panier>(panier));
        var controller = new PanierController(mockRepo.Object);

        var jwt = new JwtSecurityToken(user.GenerateJwtToken(_config));
        controller.ControllerContext.HttpContext = new DefaultHttpContext
        {
            User = new ClaimsPrincipal(new ClaimsIdentity(jwt.Claims))
        };

        var res = controller.PutPanier(panier.PanierId, new Panier
        {
            PanierId = panier.PanierId,
            ClientId = user.ClientId,
            Cookie = "Fondant au pépites de chocolat"
        }).Result;

        Assert.IsInstanceOfType<NoContentResult>(res);
    }

    [TestMethod]
    public void TestDeletePanierUnauthorizedMoq()
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

        var panier = new Panier
        {
            PanierId = 1,
            ClientId = 2
        };

        var mockRepo = new Mock<IDataPanier>();
        mockRepo.Setup(r => r.GetByIdAsync(panier.PanierId).Result).Returns(new ActionResult<Panier>(panier));
        var controller = new PanierController(mockRepo.Object);

        var jwt = new JwtSecurityToken(user.GenerateJwtToken(_config));
        controller.ControllerContext.HttpContext = new DefaultHttpContext
        {
            User = new ClaimsPrincipal(new ClaimsIdentity(jwt.Claims))
        };

        var res = controller.DeletePanier(panier.PanierId).Result;

        Assert.IsInstanceOfType<UnauthorizedResult>(res);
    }

    [TestMethod]
    public void TestDeletePanierNotFoundMoq()
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

        var panierId = 1;

        var mockRepo = new Mock<IDataPanier>();
        mockRepo.Setup(r => r.GetByIdAsync(panierId).Result).Returns(new ActionResult<Panier>((Panier)null));
        var controller = new PanierController(mockRepo.Object);

        var jwt = new JwtSecurityToken(user.GenerateJwtToken(_config));
        controller.ControllerContext.HttpContext = new DefaultHttpContext
        {
            User = new ClaimsPrincipal(new ClaimsIdentity(jwt.Claims))
        };

        var res = controller.DeletePanier(panierId).Result;

        Assert.IsInstanceOfType<NotFoundResult>(res);
    }

    [TestMethod]
    public void TestDeletePanierOkMoq()
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

        var panier = new Panier
        {
            PanierId = 1,
            ClientId = user.ClientId
        };

        var mockRepo = new Mock<IDataPanier>();
        mockRepo.Setup(r => r.GetByIdAsync(panier.PanierId).Result).Returns(new ActionResult<Panier>(panier));
        var controller = new PanierController(mockRepo.Object);

        var jwt = new JwtSecurityToken(user.GenerateJwtToken(_config));
        controller.ControllerContext.HttpContext = new DefaultHttpContext
        {
            User = new ClaimsPrincipal(new ClaimsIdentity(jwt.Claims))
        };

        var res = controller.DeletePanier(panier.PanierId).Result;

        Assert.IsInstanceOfType<NoContentResult>(res);
    }
}