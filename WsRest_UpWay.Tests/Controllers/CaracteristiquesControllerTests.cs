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
    public class CaracteristiquesControllerTests
    {
        private CaracteristiquesController _controller;
        private Mock<IDataRepository<Caracteristique>> _mockDataRepository;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockDataRepository = new Mock<IDataRepository<Caracteristique>>();
            _controller = new CaracteristiquesController(_mockDataRepository.Object);
        }
        [TestMethod]
        public async Task GetCaracteristique_ReturnsOkResult_WhenCaracteristiqueExist()
        {
            // Arrange
            var caracteristique = new List<Caracteristique>
        {
            new() { CaracteristiqueId = 1, LibelleCaracteristique = "Véhicule volant", },
            new() { CaracteristiqueId = 2, LibelleCaracteristique = "Véhicule voyageant dans le temps" }
        };
            _mockDataRepository.Setup(repo => repo.GetAllAsync(0)).ReturnsAsync(caracteristique);

            // Act
            var result = await _controller.GetCaracteristiques();

            // Assert
            var returnedcaracteristique = result.Value as List<Caracteristique>;
            Assert.IsNotNull(returnedcaracteristique);
            CollectionAssert.AreEquivalent(caracteristique, returnedcaracteristique);
        }
        [TestMethod]
        public async Task GetCaracteristiqueById_ExistingIdPassed_ReturnsRightItem_AvecMoq()
        {
            // Arrange
            var caracteristiqueId = 1;
            var caracteristique = new Caracteristique { CaracteristiqueId = caracteristiqueId, LibelleCaracteristique = "Véhicule volant" };
            _mockDataRepository.Setup(repo => repo.GetByIdAsync(caracteristiqueId)).ReturnsAsync(caracteristique);

            // Act
            var result = await _controller.GetCaracteristique(caracteristiqueId);

            // Assert
            var returnedcaracteristique = result.Value;
            Assert.IsNotNull(returnedcaracteristique);
            Assert.AreEqual(caracteristiqueId, returnedcaracteristique.CaracteristiqueId);
        }
        [TestMethod]
        public async Task GetCaracteristique_ReturnsNotFound_WhenCaracteristiqueDoesNotExist()
        {
            var cara = new Caracteristique
            {
                CaracteristiqueId = 1,
                LibelleCaracteristique = "Véhicule volant",
                ImageCaracteristique = "nothing.png"
            };
            var mockRepository = new Mock<IDataRepository<Caracteristique>>();
            mockRepository.Setup(x => x.GetByIdAsync(1).Result).Returns(cara);
            var caraController = new CaracteristiquesController(mockRepository.Object);

            // Act
            var actionResult = caraController.GetCaracteristique(1).Result;

            // Assert   
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            Assert.AreEqual(cara, actionResult.Value);
        }
        [TestMethod]
        public async Task PostCaracteristique_ReturnsCreatedResult_WhenModelIsValid()
        {
            // Arrange
            var newcaracteristique = new Caracteristique {
                CaracteristiqueId = 1,
                LibelleCaracteristique = "Véhicule volant",
                ImageCaracteristique = "nothing.png"
            };
            _mockDataRepository.Setup(repo => repo.AddAsync(It.IsAny<Caracteristique>())).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.PostCaracteristique(newcaracteristique);

            // Assert
            var createdAtActionResult = result.Result as CreatedAtActionResult;
            Assert.IsNotNull(createdAtActionResult);
            Assert.AreEqual("GetById", createdAtActionResult.ActionName);
            Assert.AreEqual(newcaracteristique.CaracteristiqueId, createdAtActionResult.RouteValues["id"]);
        }
        [TestMethod]
        public async Task PostCaracteristique_ReturnsBadRequest_WhenModelIsInvalid()
        {
            // Arrange
            var newcaracteristique = new Caracteristique();
            _controller.ModelState.AddModelError("LibelleCaracteristique", "Required");

            // Act
            var result = await _controller.PostCaracteristique(newcaracteristique);

            // Assert
            var badRequestResult = result.Result as BadRequestObjectResult;
            Assert.IsNotNull(badRequestResult);
        }

        [TestMethod]
        public async Task PutCaracteristique_ReturnsNoContent_WhenCaracteristiqueIsUpdated()
        {
            // Arrange
            var caracteristiqueId = 1;
            var updatedcaracteristique = new Caracteristique {
                CaracteristiqueId = 1,
                LibelleCaracteristique = "Véhicule volant",
                ImageCaracteristique = "nothing.png"
            };
            _mockDataRepository.Setup(repo => repo.GetByIdAsync(caracteristiqueId)).ReturnsAsync(updatedcaracteristique);
            _mockDataRepository.Setup(repo => repo.UpdateAsync(It.IsAny<Caracteristique>(), It.IsAny<Caracteristique>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.PutCaracteristique(caracteristiqueId, updatedcaracteristique);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }
        [TestMethod]
        public async Task PutCaracteristique_ReturnsNotFound_WhenCaracteristiqueDoesNotExist()
        {
            // Arrange
            var caracteristiqueId = 1;
            var updatedcaracteristique = new Caracteristique {
                CaracteristiqueId = 1,
                LibelleCaracteristique = "Véhicule volant",
                ImageCaracteristique = "nothing.png"
            };
            _mockDataRepository.Setup(repo => repo.GetByIdAsync(caracteristiqueId)).ReturnsAsync((Caracteristique)null);

            // Act
            var result = await _controller.PutCaracteristique(caracteristiqueId, updatedcaracteristique);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
        [TestMethod]
        public async Task DeleteCaracteristique_ReturnsNoContent_WhenCaracteristiqueIsDeleted()
        {
            // Arrange
            var caracteristiqueId = 1;
            var caracteristique = new Caracteristique {
                CaracteristiqueId = 1,
                LibelleCaracteristique = "Véhicule volant",
                ImageCaracteristique = "nothing.png"
            };
            _mockDataRepository.Setup(repo => repo.GetByIdAsync(caracteristiqueId)).ReturnsAsync(caracteristique);
            _mockDataRepository.Setup(repo => repo.DeleteAsync(It.IsAny<Caracteristique>())).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.DeleteCaracteristique(caracteristiqueId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }
        [TestMethod]
        public async Task DeleteCaracteristique_ReturnsNotFound_WhenCaracteristiqueDoesNotExist()
        {
            // Arrange
            var caracteristiqueId = 1;
            _mockDataRepository.Setup(repo => repo.GetByIdAsync(caracteristiqueId)).ReturnsAsync((Caracteristique)null);

            // Act
            var result = await _controller.DeleteCaracteristique(caracteristiqueId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
    }
}