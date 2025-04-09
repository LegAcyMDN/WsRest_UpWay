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
    public class CompteClientsControllerTests
    {
        private CompteClientsController _controller;
        private Mock<IDataRepository<CompteClient>> _mockDataRepository;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockDataRepository = new Mock<IDataRepository<CompteClient>>();
            _controller = new CompteClientsController(_mockDataRepository.Object);
        }

        [TestMethod]
        public async Task GetCompteClient_ReturnsOkResult_WhenCompteClientExist()
        {
            // Arrange
            var compteClient = new List<CompteClient>
            {
            new() { ClientId = 1, LoginClient = "Cequetuveux", },
            new() { ClientId = 2, LoginClient = "Frenchie", }
            };
            _mockDataRepository.Setup(repo => repo.GetAllAsync(0)).ReturnsAsync(compteClient);

            // Act
            var result = await _controller.GetCompteclients();

            // Assert
            var returnedcompteClient = result.Value as List<CompteClient>;
            Assert.IsNotNull(returnedcompteClient);
            CollectionAssert.AreEquivalent(compteClient, returnedcompteClient);
        }
        [TestMethod]
        public async Task GetCompteClient_UnknownIdPassed_ReturnsNotFoundResult()
        {
            // Arrange
            var mockRepository = new Mock<IDataRepository<CompteClient>>();
            mockRepository.Setup(x => x.GetByIdAsync(1).Result).Returns((CompteClient)null);
            var comptController = new CompteClientsController(mockRepository.Object);

            // Act
            var actionResult = comptController.GetCompteClient(2).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));
        }
        [TestMethod]
        public async Task PostcompteClient_ReturnsCreatedResult_WhenModelIsValid()
        {
            // Arrange
            var newcompte = new CompteClient
            {
                ClientId = 1,
                LoginClient = "Cequetuveux"
            };
            _mockDataRepository.Setup(repo => repo.AddAsync(It.IsAny<CompteClient>())).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.PostCompteClient(newcompte);

            // Assert
            var createdAtActionResult = result.Result as CreatedAtActionResult;
            Assert.IsNotNull(createdAtActionResult);
            Assert.AreEqual("GetById", createdAtActionResult.ActionName);
            Assert.AreEqual(newcompte.ClientId, createdAtActionResult.RouteValues["id"]);
        }
        [TestMethod]
        public async Task PostCompteClient_ReturnsBadRequest_WhenModelIsInvalid()
        {
            // Arrange
            var newcompteClient = new CompteClient();
            _controller.ModelState.AddModelError("LoginClient", "Required");

            // Act
            var result = await _controller.PostCompteClient(newcompteClient);

            // Assert
            var badRequestResult = result.Result as BadRequestObjectResult;
            Assert.IsNotNull(badRequestResult);
        }
        [TestMethod]
        public async Task PutcompteClient_ReturnsNoContent_WhenCompteClientIsUpdated()
        {
            // Arrange
            var clientId = 1;
            var updatedcompteClient = new CompteClient
            {
                ClientId = 1,
                LoginClient = "Jean peplus",
            };
            _mockDataRepository.Setup(repo => repo.GetByIdAsync(clientId)).ReturnsAsync(updatedcompteClient);
            _mockDataRepository.Setup(repo => repo.UpdateAsync(It.IsAny<CompteClient>(), It.IsAny<CompteClient>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.PutCompteClient(clientId, updatedcompteClient);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }
        [TestMethod]
        public async Task PutCompteClient_ReturnsNotFound_WhenCompteClientDoesNotExist()
        {
            // Arrange
            var compteClientId = 1;
            var updatedcompteClient = new compteClient
            {
                compteClientId = 1,
                LibellecompteClient = "Véhicule volant",
                ImagecompteClient = "nothing.png"
            };
            _mockDataRepository.Setup(repo => repo.GetByIdAsync(compteClientId)).ReturnsAsync((compteClient)null);

            // Act
            var result = await _controller.PutcompteClient(compteClientId, updatedcompteClient);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
    }
}