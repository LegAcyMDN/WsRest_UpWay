using Microsoft.VisualStudio.TestTools.UnitTesting;
using WsRest_UpWay.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using WsRest_UpWay.Models.Repository;
using WsRest_UpWay.Models.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace WsRest_UpWay.Controllers.Tests
{
    [TestClass()]
    public class AssurancesControllerTests
    {
        [TestMethod]
        public void GetAssuranceById_ExistingId_ReturnsAssurance()
        {
            // Arrange
            var assurance = new Assurance { AssuranceId = 1, TitreAssurance = "Basique", PrixAssurance = 29.99M };
            var mockRepo = new Mock<IDataRepository<Assurance>>();
            mockRepo.Setup(x => x.GetByIdAsync(1).Result).Returns(assurance);
            var controller = new AssurancesController(mockRepo.Object);

            // Act
            var result = controller.GetAssuranceById(1).Result;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Value);
            Assert.AreEqual(assurance, result.Value);
        }

        [TestMethod]
        public void GetAssuranceById_UnknownId_ReturnsNotFound()
        {
            // Arrange
            var mockRepo = new Mock<IDataRepository<Assurance>>();
            mockRepo.Setup(x => x.GetByIdAsync(0).Result).Returns((Assurance)null);
            var controller = new AssurancesController(mockRepo.Object);

            // Act
            var result = controller.GetAssuranceById(0).Result;

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void GetAssuranceByTitre_ExistingTitle_ReturnsAssurance()
        {
            // Arrange
            var assurance = new Assurance { AssuranceId = 1, TitreAssurance = "Premium", PrixAssurance = 99.99M };
            var mockRepo = new Mock<IDataRepository<Assurance>>();
            mockRepo.Setup(x => x.GetByStringAsync("Premium").Result).Returns(assurance);
            var controller = new AssurancesController(mockRepo.Object);

            // Act
            var result = controller.GetAssuranceByTitre("Premium").Result;

            // Assert
            Assert.IsNotNull(result.Value);
            Assert.AreEqual("Premium", result.Value.TitreAssurance);
        }

        [TestMethod]
        public void GetAssuranceByTitre_UnknownTitle_ReturnsNotFound()
        {
            // Arrange
            var mockRepo = new Mock<IDataRepository<Assurance>>();
            mockRepo.Setup(x => x.GetByStringAsync("Gold").Result).Returns((Assurance)null);
            var controller = new AssurancesController(mockRepo.Object);

            // Act
            var result = controller.GetAssuranceByTitre("Gold").Result;

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void PutAssurance_ValidUpdate_ReturnsNoContent()
        {
            // Arrange
            var assuranceBefore = new Assurance { AssuranceId = 1, TitreAssurance = "Basique" };
            var assuranceAfter = new Assurance { AssuranceId = 1, TitreAssurance = "Premium" };

            var mockRepo = new Mock<IDataRepository<Assurance>>();
            mockRepo.Setup(x => x.GetByIdAsync(1).Result).Returns(assuranceBefore);

            var controller = new AssurancesController(mockRepo.Object);

            // Act
            var result = controller.PutAssurance(1, assuranceAfter).Result;

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public void PutAssurance_IdMismatch_ReturnsBadRequest()
        {
            // Arrange
            var assurance = new Assurance { AssuranceId = 2, TitreAssurance = "Basique" };
            var mockRepo = new Mock<IDataRepository<Assurance>>();
            var controller = new AssurancesController(mockRepo.Object);

            // Act
            var result = controller.PutAssurance(1, assurance).Result;

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }

        [TestMethod]
        public void PostAssurance_ValidAssurance_ReturnsCreatedResult()
        {
            // Arrange
            var assurance = new Assurance { AssuranceId = 1, TitreAssurance = "Silver", PrixAssurance = 19.99M };
            var mockRepo = new Mock<IDataRepository<Assurance>>();
            var controller = new AssurancesController(mockRepo.Object);

            // Act
            var result = controller.PostAssurance(assurance).Result;

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(CreatedAtActionResult));
            var created = result.Result as CreatedAtActionResult;
            Assert.AreEqual(assurance, created.Value);
        }

        [TestMethod]
        public void DeleteAssurance_ExistingAssurance_ReturnsNoContent()
        {
            // Arrange
            var assurance = new Assurance { AssuranceId = 3, TitreAssurance = "Gold" };
            var mockRepo = new Mock<IDataRepository<Assurance>>();
            mockRepo.Setup(x => x.GetByIdAsync(3).Result).Returns(assurance);
            var controller = new AssurancesController(mockRepo.Object);

            // Act
            var result = controller.DeleteAssurance(3).Result;

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public void DeleteAssurance_UnknownId_ReturnsNotFound()
        {
            // Arrange
            var mockRepo = new Mock<IDataRepository<Assurance>>();
            mockRepo.Setup(x => x.GetByIdAsync(0).Result).Returns((Assurance)null);
            var controller = new AssurancesController(mockRepo.Object);

            // Act
            var result = controller.DeleteAssurance(0).Result;

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
    }
}