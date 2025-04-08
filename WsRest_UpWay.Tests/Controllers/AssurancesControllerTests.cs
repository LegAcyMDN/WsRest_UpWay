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
        private AssurancesController _controller;
        private Mock<IDataRepository<Assurance>> _mockDataRepository;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockDataRepository = new Mock<IDataRepository<Assurance>>();
            _controller = new AssurancesController(_mockDataRepository.Object);
        }

        [TestMethod]
        public async Task GetAssurances_ReturnsOkResult_WhenAssurancesExist()
        {
            // Arrange
            var assurances = new List<Assurance>
        {
            new() { AssuranceId = 1, TitreAssurance = "Assurance 1" },
            new() { AssuranceId = 2, TitreAssurance = "Assurance 2" }
        };
            _mockDataRepository.Setup(repo => repo.GetAllAsync(0)).ReturnsAsync(assurances);

            // Act
            var result = await _controller.Gets();

            // Assert
            var returnedAssurances = result.Value as List<Assurance>;
            Assert.IsNotNull(returnedAssurances);
            CollectionAssert.AreEquivalent(assurances, returnedAssurances);
        }
        [TestMethod]
        public async Task Getassurance_ReturnsNotFound_WhenAssuranceDoesNotExist()
        {
            // Arrange
            var assuranceId = 1;
            _mockDataRepository.Setup(repo => repo.GetByIdAsync(assuranceId)).ReturnsAsync((Assurance)null);

            // Act
            var result = await _controller.GetAssuranceById(assuranceId);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }
        [TestMethod]
        public async Task GetAssurance_ReturnsOkResult_WhenAssuranceIdExists()
        {
            // Arrange
            var assuranceId = 1;
            var assurance = new Assurance { AssuranceId = assuranceId, TitreAssurance = "Assurance 1" };
            _mockDataRepository.Setup(repo => repo.GetByIdAsync(assuranceId)).ReturnsAsync(assurance);

            // Act
            var result = await _controller.GetAssuranceById(assuranceId);

            // Assert
            var returnedassurance = result.Value;
            Assert.IsNotNull(returnedassurance);
            Assert.AreEqual(assuranceId, returnedassurance.AssuranceId);
        }
        [TestMethod]
        public async Task Postassurance_ReturnsCreatedResult_WhenModelIsValid()
        {
            // Arrange
            var newAssurance = new Assurance { AssuranceId = 1, TitreAssurance = "Bonne Assurance" };
            _mockDataRepository.Setup(repo => repo.AddAsync(It.IsAny<Assurance>())).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.PostAssurance(newAssurance);

            // Assert
            var createdAtActionResult = result.Result as CreatedAtActionResult;
            Assert.IsNotNull(createdAtActionResult);
            Assert.AreEqual("GetById", createdAtActionResult.ActionName);
            Assert.AreEqual(newAssurance.AssuranceId, createdAtActionResult.RouteValues["id"]);
        }
        [TestMethod]
        public async Task PostAssurance_ReturnsBadRequest_WhenModelIsInvalid()
        {
            // Arrange
            var newAssurance = new Assurance();
            _controller.ModelState.AddModelError("TitreAssurance", "Required");

            // Act
            var result = await _controller.PostAssurance(newAssurance);

            // Assert
            var badRequestResult = result.Result as BadRequestObjectResult;
            Assert.IsNotNull(badRequestResult);
        }

        [TestMethod]
        public async Task PutAssurance_ReturnsNoContent_WhenAssuranceIsUpdated()
        {
            // Arrange
            var assuranceId = 1;
            var updatedAssurance = new Assurance { AssuranceId = assuranceId, TitreAssurance = "Updated Assurance" };
            _mockDataRepository.Setup(repo => repo.GetByIdAsync(assuranceId)).ReturnsAsync(updatedAssurance);
            _mockDataRepository.Setup(repo => repo.UpdateAsync(It.IsAny<Assurance>(), It.IsAny<Assurance>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.PutAssurance(assuranceId, updatedAssurance);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }
        [TestMethod]
        public async Task PutAssurance_ReturnsNotFound_WhenAssuranceDoesNotExist()
        {
            // Arrange
            var assuranceId = 1;
            var updatedAssurance = new Assurance { AssuranceId = assuranceId, TitreAssurance = "Updated Assurance" };
            _mockDataRepository.Setup(repo => repo.GetByIdAsync(assuranceId)).ReturnsAsync((Assurance)null);

            // Act
            var result = await _controller.PutAssurance(assuranceId, updatedAssurance);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
        [TestMethod]
        public async Task DeleteAssurance_ReturnsNoContent_WhenAssuranceIsDeleted()
        {
            // Arrange
            var assuranceId = 1;
            var Assurance = new Assurance { AssuranceId = assuranceId };
            _mockDataRepository.Setup(repo => repo.GetByIdAsync(assuranceId)).ReturnsAsync(Assurance);
            _mockDataRepository.Setup(repo => repo.DeleteAsync(It.IsAny<Assurance>())).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.DeleteAssurance(assuranceId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }
        [TestMethod]
        public async Task DeleteAssurance_ReturnsNotFound_WhenAssuranceDoesNotExist()
        {
            // Arrange
            var assuranceId = 1;
            _mockDataRepository.Setup(repo => repo.GetByIdAsync(assuranceId)).ReturnsAsync((Assurance)null);

            // Act
            var result = await _controller.DeleteAssurance(assuranceId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
    }
}