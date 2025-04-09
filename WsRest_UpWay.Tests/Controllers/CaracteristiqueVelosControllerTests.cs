using Microsoft.VisualStudio.TestTools.UnitTesting;
using WsRest_UpWay.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WsRest_UpWay.Controllers;
using WsRest_UpWay.Models.Repository;

namespace WsRest_UpWay.Tests.Controllers
{
    [TestClass()]
    public class CaracteristiqueVeloControllerTests
    {
        private CaracteristiqueVelosController _controller;
        private Mock<IDataRepository<CaracteristiqueVelo>> _mockDataRepository;

        // Initialisation mocks
        [TestInitialize]
        public void TestInitialize()
        {
            _mockDataRepository = new Mock<IDataRepository<CaracteristiqueVelo>>();
            _controller = new CaracteristiqueVelosController(_mockDataRepository.Object);
        }

        // Test GetCaracteristiqueVelo() Exist
        [TestMethod]
        public async Task GetCaracteristiqueVelos_ReturnsOkResult_WhenCaracteristiqueVelosExist()
        {
            // Arrange
            var caracteristiqueVelo = new List<CaracteristiqueVelo>
        {
            new() { CaracteristiqueVeloId = 1, TubeSelle = 1 },
            new() { CaracteristiqueVeloId = 2, TubeSelle = 2 }
        };
            _mockDataRepository.Setup(repo => repo.GetAllAsync(0)).ReturnsAsync(caracteristiqueVelo);

            // Act
            var result = await _controller.GetCaracteristiquevelos();

            // Assert
            var returnedCaracteristiqueVelo = result.Value as List<CaracteristiqueVelo>;
            Assert.IsNotNull(returnedCaracteristiqueVelo);
            CollectionAssert.AreEquivalent(caracteristiqueVelo, returnedCaracteristiqueVelo);
        }

        // Test GetCaracteristiquevelo() NotExist
        [TestMethod]
        public async Task GetCaracteristiquevelo_ReturnsNotFound_WhenCaracteristiqueveloDoesNotExist()
        {
            // Arrange
            var caraVelo = new CaracteristiqueVelo
            {
                CaracteristiqueVeloId = 1,
                TubeSelle = 1,
            };
            var mockRepository = new Mock<IDataRepository<CaracteristiqueVelo>>();
            mockRepository.Setup(x => x.GetByIdAsync(1).Result).Returns(caraVelo);
            var catArticleController = new CaracteristiqueVelosController(mockRepository.Object);

            // Act
            var actionResult = catArticleController.GetCaracteristiqueVelo(1).Result;

            // Assert   
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            Assert.AreEqual(caraVelo, actionResult.Value);
        }

        // Test GetCaracteristiqueveloId() Ok
        [TestMethod]
        public async Task GetCaracteristiquevelo_ReturnsOkResult_WhenCaracteristiqueveloIdExists()
        {
            // Arrange
            var caracteristiqueveloId = 1;
            var caracteristiquevelo = new CaracteristiqueVelo { CaracteristiqueVeloId = caracteristiqueveloId, TubeSelle = 1 };
            _mockDataRepository.Setup(repo => repo.GetByIdAsync(caracteristiqueveloId)).ReturnsAsync(caracteristiquevelo);

            // Act
            var result = await _controller.GetCaracteristiqueVelo(caracteristiqueveloId);

            // Assert
            var returnedCaracteristiquevelo = result.Value;
            Assert.IsNotNull(returnedCaracteristiquevelo);
            Assert.AreEqual(caracteristiqueveloId, returnedCaracteristiquevelo.CaracteristiqueVeloId);
        }

        // Test PostCaracteristiquevelo() Valid
        [TestMethod]
        public async Task PostCaracteristiquevelo_ReturnsCreatedResult_WhenModelIsValid()
        {
            // Arrange
            var newCaracteristiquevelo = new CaracteristiqueVelo { CaracteristiqueVeloId = 1, TubeSelle = 1 };
            _mockDataRepository.Setup(repo => repo.AddAsync(It.IsAny<CaracteristiqueVelo>())).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.PostCaracteristiqueVelo(newCaracteristiquevelo);

            // Assert
            var createdAtActionResult = result.Result as CreatedAtActionResult;
            Assert.IsNotNull(createdAtActionResult);
            Assert.AreEqual("GetCaracteristique", createdAtActionResult.ActionName);
            Assert.AreEqual(newCaracteristiquevelo.CaracteristiqueVeloId, createdAtActionResult.RouteValues["id"]);
        }

        // Test PostCaracteristiquevelo() Invalid
        [TestMethod]
        public async Task PostCaracteristiquevelo_ReturnsBadRequest_WhenModelIsInvalid()
        {
            // Arrange
            var newCaracteristiquevelo = new CaracteristiqueVelo();
            _controller.ModelState.AddModelError("TubeSelle", "Required");

            // Act
            var result = await _controller.PostCaracteristiqueVelo(newCaracteristiquevelo);

            // Assert
            var badRequestResult = result.Result as BadRequestObjectResult;
            Assert.IsNotNull(badRequestResult);
        }

        // Test pour PutCaracteristiquevelo()
        [TestMethod]
        public async Task PutCaracteristiquevelo_ReturnsNoContent_WhenCaracteristiqueveloIsUpdated()
        {
            // Arrange
            var CaracteristiqueveloId = 1;
            var updatedCaracteristiquevelo = new CaracteristiqueVelo { CaracteristiqueVeloId = CaracteristiqueveloId, TubeSelle = 1 };
            _mockDataRepository.Setup(repo => repo.GetByIdAsync(CaracteristiqueveloId)).ReturnsAsync(updatedCaracteristiquevelo);
            _mockDataRepository.Setup(repo => repo.UpdateAsync(It.IsAny<CaracteristiqueVelo>(), It.IsAny<CaracteristiqueVelo>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.PutCaracteristiqueVelo(CaracteristiqueveloId, updatedCaracteristiquevelo);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        // Test pour PutCaracteristiquevelo() NotExist
        [TestMethod]
        public async Task PutCaracteristiquevelo_ReturnsNotFound_WhenCaracteristiqueveloDoesNotExist()
        {
            // Arrange
            var CaracteristiqueveloId = 1;
            var updatedCaracteristiquevelo = new CaracteristiqueVelo { CaracteristiqueVeloId = CaracteristiqueveloId, TubeSelle = 1 };
            _mockDataRepository.Setup(repo => repo.GetByIdAsync(CaracteristiqueveloId)).ReturnsAsync((CaracteristiqueVelo)null);

            // Act
            var result = await _controller.PutCaracteristiqueVelo(CaracteristiqueveloId, updatedCaracteristiquevelo);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        // Test pour DeleteCaracteristiquevelo()
        [TestMethod]
        public async Task DeleteCaracteristiquevelo_ReturnsNoContent_WhenCaracteristiqueveloIsDeleted()
        {
            // Arrange
            var CaracteristiqueveloId = 1;
            var Caracteristiquevelo = new CaracteristiqueVelo { CaracteristiqueVeloId = CaracteristiqueveloId };
            _mockDataRepository.Setup(repo => repo.GetByIdAsync(CaracteristiqueveloId)).ReturnsAsync(Caracteristiquevelo);
            _mockDataRepository.Setup(repo => repo.DeleteAsync(It.IsAny<CaracteristiqueVelo>())).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.DeleteCaracteristiqueVelo(CaracteristiqueveloId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }
    }
}