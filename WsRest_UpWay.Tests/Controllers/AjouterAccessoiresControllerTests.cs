using Microsoft.VisualStudio.TestTools.UnitTesting;
using WsRest_UpWay.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Security.Claims;
using WsRest_UpWay.Models.EntityFramework;
using WsRest_UpWay.Models.Repository;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using WsRest_UpWay.Models;
using Microsoft.Extensions.Configuration;

namespace WsRest_UpWay.Controllers.Tests;

[TestClass]
public class AjouterAccessoiresControllerTests
{
    private AjouterAccessoiresController _controller;
    private Mock<IDataRepository<AjouterAccessoire>> _mockRepo;
    private Mock<IDataPanier> _mockPanierRepo;

    private AjouterAccessoire _ajouterAccessoire;
    private Panier _panier;
    private IConfiguration _config;
    private CompteClient _user;

    [TestInitialize]
    public void TestInitialize()
    {
        _mockRepo = new Mock<IDataRepository<AjouterAccessoire>>();
        _mockPanierRepo = new Mock<IDataPanier>();

        _controller = new AjouterAccessoiresController(_mockRepo.Object, _mockPanierRepo.Object);

        _config = new ConfigurationManager();
        _config["JWT_SECRET_KEY"] = "c8qWCt7UFn3DASzZHD0vBhNcpNlZsQeszbpYkw28Cb3BLkzf7By6VPHQSnd0iMg";
        _config["JWT_ISSUER"] = "http://localhost:5194/";
        _config["JWT_AUDIENCE"] = "http://localhost:5194/";

        _user = new CompteClient
        {
            LoginClient = "jean.patrick",
            EmailClient = "jean.patrick@gmail.com",
            PrenomClient = "Jean",
            NomClient = "Patrick",
            ClientId = 20,
            MotDePasseClient = new PasswordHasher<CompteClient>().HashPassword(null, "Jean@Patrick!"),
            Usertype = Policies.User
        };
        
        _panier = new Panier
        {
            PanierId = 1,
            ClientId = _user.ClientId,
        };

        _ajouterAccessoire = new AjouterAccessoire
        {
            AccessoireId = 1,
            PanierId = _panier.PanierId,
            QuantiteAccessoire = 2
        };
        
        var jwt = new JwtSecurityToken(_user.GenerateJwtToken(_config));
        _controller.ControllerContext.HttpContext = new DefaultHttpContext
        {
            User = new ClaimsPrincipal(new ClaimsIdentity(jwt.Claims))
        };
    }
    
    [TestMethod]
    public async Task GetAll_ReturnsOk()
    {
        _user.Usertype = Policies.Admin;
        var jwt = new JwtSecurityToken(_user.GenerateJwtToken(_config));
        _controller.ControllerContext.HttpContext = new DefaultHttpContext
        {
            User = new ClaimsPrincipal(new ClaimsIdentity(jwt.Claims))
        };
        
        var list = new List<AjouterAccessoire> { _ajouterAccessoire };
        _mockRepo.Setup(r => r.GetAllAsync(0)).ReturnsAsync(list);

        var result = await _controller.Gets();

        Assert.IsInstanceOfType<IEnumerable<AjouterAccessoire>>(result.Value);
        CollectionAssert.AreEqual(list, result.Value.ToList());
    }

 /* TODO: Find a way to make the Authorize annotation works during tests
  [TestMethod]
    public async Task GetAll_ReturnsUnauthorized()
    {
        var list = new List<AjouterAccessoire> { _ajouterAccessoire };
        _mockRepo.Setup(r => r.GetAllAsync(0)).ReturnsAsync(list);

        var result = await _controller.Gets();
        Assert.IsInstanceOfType<UnauthorizedResult>(result.Result);
    }*/
    
    [TestMethod]
    public async Task Get_ReturnsOk_WhenAccessoireExistsAndUserAuthorized()
    {
        _mockRepo.Setup(r => r.GetByIdAsync(_ajouterAccessoire.AccessoireId)).ReturnsAsync(_ajouterAccessoire);
        _mockPanierRepo.Setup(p => p.GetByIdAsync(_panier.PanierId)).ReturnsAsync(_panier);

        var result = await _controller.Get(_ajouterAccessoire.AccessoireId);

        Assert.IsInstanceOfType<AjouterAccessoire>(result.Value);
        Assert.AreEqual(_ajouterAccessoire, result.Value);
    }

    [TestMethod]
    public async Task Get_ReturnsNotFound_WhenAccessoireNotFound()
    {
        _mockRepo.Setup(r => r.GetByIdAsync(_ajouterAccessoire.AccessoireId)).ReturnsAsync((AjouterAccessoire)null);

        var result = await _controller.Get(_ajouterAccessoire.AccessoireId);

        Assert.IsInstanceOfType<NotFoundResult>(result.Result);
    }

    [TestMethod]
    public async Task Get_ReturnsUnauthorized_WhenUserIsNotOwner()
    {
        _panier.ClientId = 99;
        _mockRepo.Setup(r => r.GetByIdAsync(_ajouterAccessoire.AccessoireId)).ReturnsAsync(_ajouterAccessoire);
        _mockPanierRepo.Setup(p => p.GetByIdAsync(_panier.PanierId)).ReturnsAsync(_panier);

        var result = await _controller.Get(_ajouterAccessoire.AccessoireId);

        Assert.IsInstanceOfType(result.Result, typeof(UnauthorizedResult));
    }

    [TestMethod]
    public async Task PostAjouterAccessoire_ReturnsCreated_WhenValid()
    {
        _mockPanierRepo.Setup(p => p.GetByIdAsync(_panier.PanierId)).ReturnsAsync(_panier);

        var result = await _controller.PostAjouterAccessoire(_ajouterAccessoire);

        Assert.IsInstanceOfType(result.Result, typeof(CreatedAtActionResult));
    }

    [TestMethod]
    public async Task PostAjouterAccessoire_ReturnsUnauthorized_WhenUserNotOwner()
    {
        _panier.ClientId = 999;
        _mockPanierRepo.Setup(p => p.GetByIdAsync(_panier.PanierId)).ReturnsAsync(_panier);

        var result = await _controller.PostAjouterAccessoire(_ajouterAccessoire);

        Assert.IsInstanceOfType(result.Result, typeof(UnauthorizedResult));
    }

    [TestMethod]
    public async Task PostAjouterAccessoire_ReturnsNotFound_WhenPanierNotFound()
    {
        _mockPanierRepo.Setup(p => p.GetByIdAsync(_panier.PanierId)).ReturnsAsync((Panier)null);

        var result = await _controller.PostAjouterAccessoire(_ajouterAccessoire);

        Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
    }

    [TestMethod]
    public async Task Put_ReturnsNoContent_WhenUpdatedSuccessfully()
    {
        _mockRepo.Setup(r => r.GetByIdAsync(_ajouterAccessoire.AccessoireId)).ReturnsAsync(_ajouterAccessoire);
        _mockPanierRepo.Setup(p => p.GetByIdAsync(_panier.PanierId)).ReturnsAsync(_panier);

        var result = await _controller.Put(_ajouterAccessoire);

        Assert.IsInstanceOfType(result, typeof(NoContentResult));
    }

    [TestMethod]
    public async Task Put_ReturnsUnauthorized_WhenUserNotOwner()
    {
        _panier.ClientId = 999;
        _mockRepo.Setup(r => r.GetByIdAsync(_ajouterAccessoire.AccessoireId)).ReturnsAsync(_ajouterAccessoire);
        _mockPanierRepo.Setup(p => p.GetByIdAsync(_panier.PanierId)).ReturnsAsync(_panier);

        var result = await _controller.Put(_ajouterAccessoire);

        Assert.IsInstanceOfType(result, typeof(UnauthorizedResult));
    }

    [TestMethod]
    public async Task Put_ReturnsNotFound_WhenAccessoireNotFound()
    {
        _mockRepo.Setup(r => r.GetByIdAsync(_ajouterAccessoire.AccessoireId)).ReturnsAsync((AjouterAccessoire)null);

        var result = await _controller.Put(_ajouterAccessoire);

        Assert.IsInstanceOfType(result, typeof(NotFoundResult));
    }

    [TestMethod]
    public async Task DeleteAjouterAccessoire_ReturnsNoContent_WhenDeleted()
    {
        _mockRepo.Setup(r => r.GetByIdAsync(_ajouterAccessoire.AccessoireId)).ReturnsAsync(_ajouterAccessoire);
        _mockPanierRepo.Setup(p => p.GetByIdAsync(_panier.PanierId)).ReturnsAsync(_panier);

        var result = await _controller.DeleteAjouterAccessoire(_ajouterAccessoire.AccessoireId);

        Assert.IsInstanceOfType(result, typeof(NoContentResult));
    }

    [TestMethod]
    public async Task DeleteAjouterAccessoire_ReturnsUnauthorized_WhenUserNotOwner()
    {
        _panier.ClientId = 999;
        _mockRepo.Setup(r => r.GetByIdAsync(_ajouterAccessoire.AccessoireId)).ReturnsAsync(_ajouterAccessoire);
        _mockPanierRepo.Setup(p => p.GetByIdAsync(_panier.PanierId)).ReturnsAsync(_panier);

        var result = await _controller.DeleteAjouterAccessoire(_ajouterAccessoire.AccessoireId);

        Assert.IsInstanceOfType(result, typeof(UnauthorizedResult));
    }

    [TestMethod]
    public async Task DeleteAjouterAccessoire_ReturnsNotFound_WhenAccessoireNotFound()
    {
        _mockRepo.Setup(r => r.GetByIdAsync(_panier.PanierId)).ReturnsAsync((AjouterAccessoire)null);

        var result = await _controller.DeleteAjouterAccessoire(_ajouterAccessoire.AccessoireId);

        Assert.IsInstanceOfType(result, typeof(NotFoundResult));
    }
}