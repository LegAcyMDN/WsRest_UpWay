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
    public class MarquesControllerTests
    {
        [TestMethod]
        public void GetMarqueById_ExistingIdPassed_ReturnsRightItem_AvecMoq()
        {
            // Arrange
            Marque mar = new Marque
            {
                MarqueId = 1,
                NomMarque = "Bosh"
            };
            var mockRepository = new Mock<IDataRepository<Marque>>();
            mockRepository.Setup(x => x.GetByIdAsync(1).Result).Returns(mar);
            var marController = new MarquesController(mockRepository.Object);

            // Act
            var actionResult = marController.GetMarque(1).Result;

            // Assert   
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            Assert.AreEqual(mar, actionResult.Value as Marque);
        }

        [TestMethod]
        public void GetMarqueById_UnknownIdPassed_ReturnsNotFoundResult_AvecMoq()
        {
            var mockRepository = new Mock<IDataRepository<Marque>>();
            var marController = new MarquesController(mockRepository.Object);

            // Act
            var actionResult = marController.GetMarque(0).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void GetMarqueByModeLivraison_ExistingModePassed_AvecMoq()
        {
            // Arrange
            Marque mar = new Marque
            {
                MarqueId = 1,
                NomMarque = "Bosh"
            };
            var mockRepository = new Mock<IDataRepository<Marque>>();
            mockRepository.Setup(x => x.GetByStringAsync("Retrait Magasin").Result).Returns(mar);
            var marController = new MarquesController(mockRepository.Object);

            // Act
            var actionResult = marController.GetMarqueByNom("Retrait Magasin").Result;

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            Assert.AreEqual(mar, actionResult.Value as Marque);
        }

        [TestMethod]
        public void GetMarqueByModeLivraison_UnknownModePassed_AvecMoq()
        {
            var mockRepository = new Mock<IDataRepository<Marque>>();
            var marController = new MarquesController(mockRepository.Object);

            // Act
            var actionResult = marController.GetMarqueByNom("Marche à pieds").Result;

            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void PutMarqueTest_AvecMoq()
        {
            Marque marToEdit = new Marque
            {
                MarqueId = 1,
                NomMarque = "Bosh"
            };
            // Arrange
            Marque marEdited = new Marque
            {
                MarqueId = 1,
                NomMarque = "Canondale"
            };
            var mockRepository = new Mock<IDataRepository<Marque>>();
            mockRepository.Setup(x => x.GetByIdAsync(1).Result).Returns(marToEdit);
            var marController = new MarquesController(mockRepository.Object);

            // Act
            var actionResult = marController.PutMarque(1, marEdited).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult");
        }

        [TestMethod]
        public void PostMarqueTest_ModelValidated_CreationOK()
        {
            // Arrange
            var mockRepository = new Mock<IDataRepository<Marque>>();
            var marController = new MarquesController(mockRepository.Object);
            Marque mar = new Marque
            {
                MarqueId = 1,
                NomMarque = "Bosh"
            };

            // Act
            var actionResult = marController.PostMarque(mar).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<Marque>), "Pas un ActionResult<Marque>");
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");
            var result = actionResult.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(result.Value, typeof(Marque), "Pas un Marque");
            mar.MarqueId = ((Marque)result.Value).MarqueId;
            Assert.AreEqual(mar, (Marque)result.Value, "Marques pas identiques");
        }


        [TestMethod]
        public void DeleteMarqueTest_AvecMoq()
        {
            // Arrange
            Marque mar = new Marque
            {
                MarqueId = 2,
                NomMarque = "Nakamura"
            };
            var mockRepository = new Mock<IDataRepository<Marque>>();
            mockRepository.Setup(x => x.GetByIdAsync(2).Result).Returns(mar);
            var marController = new MarquesController(mockRepository.Object);

            // Act
            var actionResult = marController.DeleteMarque(2).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult"); // Test du type de retour
        }
    }
}