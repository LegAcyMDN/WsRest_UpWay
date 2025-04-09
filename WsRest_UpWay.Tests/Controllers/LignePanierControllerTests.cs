using Microsoft.VisualStudio.TestTools.UnitTesting;
using WsRest_UpWay.Controllers;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Moq;
using WsRest_UpWay.Models.EntityFramework;
using WsRest_UpWay.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WsRest_UpWay.Models;

namespace WsRest_UpWay.Controllers.Tests
{
    [TestClass()]
    public class LignePanierControllerTests
    {
        private IConfiguration _config;
        private LignePanierController _controller;
        private Mock<IDataLignePanier> _ligneRepository;
        private  Mock<IDataRepository<Panier>> _panierRepository;
        private  Mock<IDataRepository<MarquageVelo>> _marquageVeloRepository;

        [TestInitialize]
        public void TestInitialize()
        {
            _config = new ConfigurationManager().AddJsonFile("appsettings.Development.json").AddEnvironmentVariables()
                .Build();
            _config["JWT_SECRET_KEY"] = "c8qWCt7UFn3DASzZHD0vBhNcpNlZsQeszbpYkw28Cb3BLkzf7By6VPHQSnd0iMg";
            _config["JWT_ISSUER"] = "http://localhost:5194/";
            _config["JWT_AUDIENCE"] = "http://localhost:5194/";
            
            _ligneRepository = new Mock<IDataLignePanier>();
            _panierRepository = new Mock<IDataRepository<Panier>>();
            _marquageVeloRepository = new Mock<IDataRepository<MarquageVelo>>();
            _controller = new LignePanierController(_ligneRepository.Object, _panierRepository.Object, _marquageVeloRepository.Object);
        }

        [TestMethod]
        public void GetlignePaniers_ReturnsOkResult_WhenlignePaniersExist()
        {
            // Arrange
            var lignePanier = new List<LignePanier>
            {
            new() { PanierId = 1, QuantitePanier = 1, },
            new() { PanierId = 2, QuantitePanier = 2, }
            };
            _ligneRepository.Setup(repo => repo.GetAllAsync(0)).ReturnsAsync(lignePanier);

            // Act
            var result = _controller.GetPaniers().Result;

            // Assert
            var returnedlignePanier = result.Value as List<LignePanier>;
            Assert.IsNotNull(returnedlignePanier);
            CollectionAssert.AreEquivalent(lignePanier, returnedlignePanier);
        }
        
        [TestMethod]
        public void GetLignePanier_ReturnsNotFound_WhenLignePanierDoesNotExist()
        {
            var mockRepository = new Mock<IDataLignePanier>();
            mockRepository.Setup(x => x.GetByIdsAsync(0, 0).Result).Returns((LignePanier)null);
            var lignController = new LignePanierController(mockRepository.Object, new Mock<IDataRepository<Panier>>().Object, new Mock<IDataRepository<MarquageVelo>>().Object);

            // Act
            var actionResult = lignController.GetLignePanier(0, 0).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));
        }
        
        [TestMethod]
        public void PutLignePanierTest_AvecMoq()
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
            
            var panier = new Panier()
            {
                PanierId = 1,
                ClientId = user.ClientId
            };
            
            var lignToEdit = new LignePanier
            {
                PanierId = panier.PanierId,
                QuantitePanier = 1,
            };
            // Arrange
            var lignEdited = new LignePanier
            {
                
                PanierId = panier.PanierId,
                QuantitePanier = 2,
            };
            var mockRepository = new Mock<IDataLignePanier>();
            mockRepository.Setup(x => x.GetByIdsAsync(lignToEdit.PanierId, lignEdited.VeloId)).ReturnsAsync(lignToEdit);
            _panierRepository.Setup(x => x.GetByIdAsync(lignEdited.PanierId))
                .ReturnsAsync(panier);
            
            
            var lignController = new LignePanierController(mockRepository.Object, _panierRepository.Object, new Mock<IDataRepository<MarquageVelo>>().Object);
            var jwt = new JwtSecurityToken(user.GenerateJwtToken(_config));
            lignController.ControllerContext.HttpContext = new DefaultHttpContext
            {
                User = new ClaimsPrincipal(new ClaimsIdentity(jwt.Claims))
            };
            // Act
            var actionResult = lignController.PutPanier(lignEdited).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult");
        }
        [TestMethod]
        public void PostLignePanierTest_ModelValidated_CreationOK()
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
            
            var panier = new Panier()
            {
                PanierId = 1,
                ClientId = user.ClientId
            };
            
            var lign = new LignePanier
            {
                PanierId = 1,
                VeloId = 1,
                QuantitePanier = 1,
            };
            
            // Arrange
            var mockRepository = new Mock<IDataLignePanier>();
            _panierRepository.Setup(x => x.GetByIdAsync(lign.PanierId))
                .ReturnsAsync(panier);
            var lignController = new LignePanierController(mockRepository.Object, _panierRepository.Object, new Mock<IDataRepository<MarquageVelo>>().Object);
            var jwt = new JwtSecurityToken(user.GenerateJwtToken(_config));
            lignController.ControllerContext.HttpContext = new DefaultHttpContext
            {
                User = new ClaimsPrincipal(new ClaimsIdentity(jwt.Claims))
            };
            
           

            // Act
            var actionResult = lignController.PostPanier(lign).Result;

            // Assert
            Assert.IsInstanceOfType<CreatedAtActionResult>(actionResult.Result, "Pas un CreatedAtActionResult");
            var result = actionResult.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType<LignePanier>(result.Value, "Pas un LignePanier");
            Assert.AreEqual(lign, (LignePanier)result.Value, "LignePaniers pas identiques");
        }
        [TestMethod]
        public void DeleteLignePanierTest_AvecMoq()
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
            
            var panier = new Panier()
            {
                PanierId = 1,
                ClientId = user.ClientId
            };
            
            // Arrange
            var lign = new LignePanier
            {
                PanierId = panier.PanierId,
                VeloId = 1,
                QuantitePanier = 1,
            };
            
            var mockRepository = new Mock<IDataLignePanier>();
            mockRepository.Setup(x => x.GetByIdsAsync(lign.PanierId, lign.PanierId).Result).Returns(lign);
            _panierRepository.Setup(x => x.GetByIdAsync(lign.PanierId))
                .ReturnsAsync(panier);
            
            var catController = new LignePanierController(mockRepository.Object, _panierRepository.Object, new Mock<IDataRepository<MarquageVelo>>().Object);
            var jwt = new JwtSecurityToken(user.GenerateJwtToken(_config));
            catController.ControllerContext.HttpContext = new DefaultHttpContext
            {
                User = new ClaimsPrincipal(new ClaimsIdentity(jwt.Claims))
            };
            
            
            // Act
            var actionResult = catController.DeletePanier(lign.PanierId, lign.VeloId).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult),
                "Pas un NoContentResult"); // Test du type de retour
        }
    }
}