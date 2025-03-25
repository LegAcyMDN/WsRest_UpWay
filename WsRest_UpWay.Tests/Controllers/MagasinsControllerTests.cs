using Microsoft.VisualStudio.TestTools.UnitTesting;
using WsRest_UpWay.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WsRest_UpWay.Models.EntityFramework;
using WsRest_UpWay.Models.Repository;

namespace WsRest_UpWay.Controllers.Tests
{
    [TestClass()]
    public class MagasinsControllerTests
    {
        [TestMethod]
        public void GetMagasinById_ExistingIdPassed_ReturnsRightItem_AvecMoq()
        {
            // Arrange
            Magasin mag = new Magasin
            {
                MagasinId = 1,
                NomMagasin = "La Petite Roue",
                HoraireMagasin = "8h00 - 16h00",
                RueMagasin = "Course Arc en Ciel Couronne",
                CPMagasin = "74000",
                VilleMagasin = "Coconut"
            };
            var mockRepository = new Mock<IDataRepository<Magasin>>();
            mockRepository.Setup(x => x.GetByIdAsync(1).Result).Returns(mag);
            var magController = new MagasinsController(mockRepository.Object);

            // Act
            var actionResult = magController.GetMagasin(1).Result;

            // Assert   
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            Assert.AreEqual(mag, actionResult.Value as Magasin);
        }

        [TestMethod]
        public void GetMagasinById_UnknownIdPassed_ReturnsNotFoundResult_AvecMoq()
        {
            var mockRepository = new Mock<IDataRepository<Magasin>>();
            var magController = new MagasinsController(mockRepository.Object);

            // Act
            var actionResult = magController.GetMagasin(0).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void GGetMagasinByNom_ExistingNomPassed_AvecMoq()
        {
            // Arrange
            Magasin mag = new Magasin
            {
                MagasinId = 1,
                NomMagasin = "Coconut Mall",
                HoraireMagasin = "8h00 - 16h00",
                RueMagasin = "Course Arc en Ciel Couronne",
                CPMagasin = "74000",
                VilleMagasin = "Coconut"
            };
            var mockRepository = new Mock<IDataRepository<Magasin>>();
            mockRepository.Setup(x => x.GetByStringAsync("Coconut Mall").Result).Returns(mag);
            var magController = new MagasinsController(mockRepository.Object);

            // Act
            var actionResult = magController.GetMagasinByNom("Coconut Mall").Result;

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            Assert.AreEqual(mag, actionResult.Value as Magasin);
        }

        [TestMethod]
        public void GetMagasinByNom_UnknownNomPassed_AvecMoq()
        {
            var mockRepository = new Mock<IDataRepository<Magasin>>();
            var magController = new MagasinsController(mockRepository.Object);

            // Act
            var actionResult = magController.GetMagasinByNom("Bowser Mall").Result;

            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void PutMagasinTest_AvecMoq()
        {
            Magasin magToEdit = new Magasin
            {
                MagasinId = 1,
                NomMagasin = "La Petite Roue",
                HoraireMagasin = "8h00 - 16h00",
                RueMagasin = "Course Arc en Ciel Couronne",
                CPMagasin = "74000",
                VilleMagasin = "Coconut"
            };
            // Arrange
            Magasin magEdited = new Magasin
            {
                MagasinId = 1,
                NomMagasin = "Coconut Mall",
                HoraireMagasin = "8h00 - 16h00",
                RueMagasin = "Course Arc en Ciel Couronne",
                CPMagasin = "74000",
                VilleMagasin = "Coconut"
            };
            var mockRepository = new Mock<IDataRepository<Magasin>>();
            mockRepository.Setup(x => x.GetByIdAsync(1).Result).Returns(magToEdit);
            var magController = new MagasinsController(mockRepository.Object);

            // Act
            var actionResult = magController.PutMagasin(1, magEdited).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult");
        }

        [TestMethod]
        public void PostMagasinTest_ModelValidated_CreationOK()
        {
            // Arrange
            var mockRepository = new Mock<IDataRepository<Magasin>>();
            var magController = new MagasinsController(mockRepository.Object);
            Magasin mag = new Magasin
            {
                MagasinId = 3,
                NomMagasin = "Playa",
                HoraireMagasin = "8h00 - 16h00",
                RueMagasin = "Course De Quentin",
                CPMagasin = "74000",
                VilleMagasin = "Coconut"
            };

            // Act
            var actionResult = magController.PostMagasin(mag).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<Magasin>), "Pas un ActionResult<Magasin>");
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");
            var result = actionResult.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(result.Value, typeof(Magasin), "Pas un Magasin");
            mag.MagasinId = ((Magasin)result.Value).MagasinId;
            Assert.AreEqual(mag, (Magasin)result.Value, "Magasins pas identiques");
        }


        [TestMethod]
        public void DeleteMagasinTest_AvecMoq()
        {
            // Arrange
            Magasin detCommande = new Magasin
            {
                MagasinId = 2,
                NomMagasin = "La Petite Roue",
                HoraireMagasin = "8h00 - 16h00",
                RueMagasin = "Course Arc en Ciel Couronne",
                CPMagasin = "74000",
                VilleMagasin = "Coconut"
            };
            var mockRepository = new Mock<IDataRepository<Magasin>>();
            mockRepository.Setup(x => x.GetByIdAsync(2).Result).Returns(detCommande);
            var detCommandeController = new MagasinsController(mockRepository.Object);

            // Act
            var actionResult = detCommandeController.DeleteMagasin(2).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult"); // Test du type de retour
        }
    }
}