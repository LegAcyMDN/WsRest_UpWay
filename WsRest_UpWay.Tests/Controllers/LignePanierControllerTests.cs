using Microsoft.VisualStudio.TestTools.UnitTesting;
using WsRest_UpWay.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using WsRest_UpWay.Models.EntityFramework;
using WsRest_UpWay.Models.Repository;
using Microsoft.AspNetCore.Mvc;

namespace WsRest_UpWay.Controllers.Tests
{
    [TestClass()]
    public class LignePanierControllerTests
    {
        private LignePanierController _controller;
        private Mock<IDataRepository<LignePanier>> _ligneRepository;
        private  Mock<IDataRepository<Panier>> _panierRepository;
        private  Mock<IDataRepository<MarquageVelo>> _marquageVeloRepository;

        [TestInitialize]
        public void TestInitialize()
        {
            _ligneRepository = new Mock<IDataRepository<LignePanier>>();
            _panierRepository = new Mock<IDataRepository<Panier>>();
            _marquageVeloRepository = new Mock<IDataRepository<MarquageVelo>>();
            _controller = new LignePanierController(_ligneRepository.Object, _panierRepository.Object, _marquageVeloRepository.Object);
        }

        [TestMethod]
        public async Task GetlignePaniers_ReturnsOkResult_WhenlignePaniersExist()
        {
            // Arrange
            var lignePanier = new List<LignePanier>
            {
            new() { PanierId = 1, QuantitePanier = 1, },
            new() { PanierId = 2, QuantitePanier = 2, }
            };
            _ligneRepository.Setup(repo => repo.GetAllAsync(0)).ReturnsAsync(lignePanier);

            // Act
            var result = await _controller.GetPaniers();

            // Assert
            var returnedlignePanier = result.Value as List<LignePanier>;
            Assert.IsNotNull(returnedlignePanier);
            CollectionAssert.AreEquivalent(lignePanier, returnedlignePanier);
        }
        [TestMethod]
        public async Task GetLignePanier_ReturnsNotFound_WhenLignePanierDoesNotExist()
        {
            var mockRepository = new Mock<IDataRepository<LignePanier>>();
            mockRepository.Setup(x => x.GetByIdAsync(0).Result).Returns((LignePanier)null);
            var lignController = new LignePanierController(mockRepository.Object, new Mock<IDataRepository<Panier>>().Object, new Mock<IDataRepository<MarquageVelo>>().Object);

            // Act
            var actionResult = lignController.GetPanier(0).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));
        }
        [TestMethod]
        public void PutLignePanierTest_AvecMoq()
        {
            var lignToEdit = new LignePanier
            {
                PanierId = 1,
                QuantitePanier = 1,
            };
            // Arrange
            var lignEdited = new LignePanier
            {
                PanierId = 1,
                QuantitePanier = 1,
            };
            var mockRepository = new Mock<IDataRepository<LignePanier>>();
            mockRepository.Setup(x => x.GetByIdAsync(1).Result).Returns(lignToEdit);
            var lignController = new LignePanierController(mockRepository.Object, new Mock<IDataRepository<Panier>>().Object, new Mock<IDataRepository<MarquageVelo>>().Object);

            // Act
            var actionResult = lignController.PutPanier(1, lignEdited).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult");
        }
        [TestMethod]
        public void PostLignePanierTest_ModelValidated_CreationOK()
        {
            // Arrange
            var mockRepository = new Mock<IDataRepository<LignePanier>>();
            var lignController = new LignePanierController(mockRepository.Object, new Mock<IDataRepository<Panier>>().Object, new Mock<IDataRepository<MarquageVelo>>().Object);
            var lign = new LignePanier
            {
                PanierId = 1,
                QuantitePanier = 1,
            };

            // Act
            var actionResult = lignController.PostPanier(lign).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<LignePanier>), "Pas un ActionResult<LignePanier>");
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");
            var result = actionResult.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(result.Value, typeof(LignePanier), "Pas un LignePanier");
            lign.PanierId = ((LignePanier)result.Value).PanierId;
            Assert.AreEqual(lign, (LignePanier)result.Value, "LignePaniers pas identiques");
        }
        [TestMethod]
        public void DeleteLignePanierTest_AvecMoq()
        {
            // Arrange
            var lign = new LignePanier
            {
                PanierId = 1,
                QuantitePanier = 1,
            };
            var mockRepository = new Mock<IDataRepository<LignePanier>>();
            mockRepository.Setup(x => x.GetByIdAsync(2).Result).Returns(lign);
            var catController = new LignePanierController(mockRepository.Object, new Mock<IDataRepository<Panier>>().Object, new Mock<IDataRepository<MarquageVelo>>().Object);

            // Act
            var actionResult = catController.DeletePanier(2).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult),
                "Pas un NoContentResult"); // Test du type de retour
        }
    }
}