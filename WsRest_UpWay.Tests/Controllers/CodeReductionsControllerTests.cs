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
    public class CodeReductionsControllerTests
    {
        private CodeReductionsController _controller;
        private Mock<IDataRepository<CodeReduction>> _mockDataRepository;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockDataRepository = new Mock<IDataRepository<CodeReduction>>();
            _controller = new CodeReductionsController(_mockDataRepository.Object);
        }
        [TestMethod]
        public async Task GetCodeReduction_ReturnsOkResult_WhenCodeReductionExist()
        {
            // Arrange
            var codeReduction = new List<CodeReduction>
        {
            new() { ReductionId = "1", ActifReduction = true, },
            new() { ReductionId = "2", ActifReduction = false, }
        };
            _mockDataRepository.Setup(repo => repo.GetAllAsync(0)).ReturnsAsync(codeReduction);

            // Act
            var result = await _controller.GetCodereductions();

            // Assert
            var returnedCodeReduction = result.Value as List<CodeReduction>;
            Assert.IsNotNull(returnedCodeReduction);
            CollectionAssert.AreEquivalent(codeReduction, returnedCodeReduction);
        }
        [TestMethod]
        public async Task GetCodeReductionById_ExistingIdPassed_ReturnsRightItem_AvecMoq()
        {
            // Arrange
            var codeReductionId = "1";
            var codeReduction = new CodeReduction { ReductionId = "1", ActifReduction = true, };
            _mockDataRepository.Setup(repo => repo.GetByStringAsync(codeReductionId)).ReturnsAsync(codeReduction);

            // Act
            var result = await _controller.GetCodeReduction(codeReductionId);

            // Assert
            var returnedCodeReduction = result.Value;
            Assert.IsNotNull(returnedCodeReduction);
            Assert.AreEqual(codeReductionId, returnedCodeReduction.ReductionId);
        }
        [TestMethod]
        public async Task GetCodeReduction_ReturnsNotFound_WhenCodeReductionDoesNotExist()
        {
            var codeReductionId = "1";
            _mockDataRepository.Setup(repo => repo.GetByStringAsync(codeReductionId)).ReturnsAsync((CodeReduction)null);

            var result = await _controller.GetCodeReduction("J7S8Zd9");

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }
        [TestMethod]
        public async Task PostCodeReduction_ReturnsCreatedResult_WhenModelIsValid()
        {
            // Arrange
            // Arrange
            var newcodeReduction = new CodeReduction
            {
                ReductionId = "1",
                ActifReduction = true,
            };
            _mockDataRepository.Setup(repo => repo.AddAsync(It.IsAny<CodeReduction>())).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.PostCodeReduction(newcodeReduction);

            // Assert
            var createdAtActionResult = result.Result as CreatedAtActionResult;
            Assert.IsNotNull(createdAtActionResult);
            Assert.AreEqual("GetCodeReduction", createdAtActionResult.ActionName);
            Assert.AreEqual(newcodeReduction.ReductionId, createdAtActionResult.RouteValues["id"]);
        }
        [TestMethod]
        public async Task PostCodeReduction_ReturnsBadRequest_WhenModelIsInvalid()
        {
            // Arrange
            var newCodeReduction = new CodeReduction();
            _controller.ModelState.AddModelError("ActifReduction", "Required");

            // Act
            var result = await _controller.PostCodeReduction(newCodeReduction);

            // Assert
            var badRequestResult = result.Result as BadRequestObjectResult;
            Assert.IsNotNull(badRequestResult);
        }

        [TestMethod]
        public async Task PutCodeReduction_ReturnsNoContent_WhenCodeReductionIsUpdated()
        {
            // Arrange
            var codeReductionId = "1";
            var updatedCodeReduction = new CodeReduction
            {
                ReductionId = "1",
                ActifReduction = true,
            };
            _mockDataRepository.Setup(repo => repo.GetByStringAsync(codeReductionId)).ReturnsAsync(updatedCodeReduction);
            _mockDataRepository.Setup(repo => repo.UpdateAsync(It.IsAny<CodeReduction>(), It.IsAny<CodeReduction>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.PutCodeReduction(codeReductionId, updatedCodeReduction);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }
        [TestMethod]
        public async Task PutCodeReduction_ReturnsNotFound_WhenCodeReductionDoesNotExist()
        {
            // Arrange
            var codeReductionId = "1";
            var updatedCodeReduction = new CodeReduction
            {
                ReductionId = "1",
                ActifReduction = true,
            };
            _mockDataRepository.Setup(repo => repo.GetByStringAsync(codeReductionId)).ReturnsAsync((CodeReduction)null);

            // Act
            var result = await _controller.PutCodeReduction(codeReductionId, updatedCodeReduction);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
        [TestMethod]
        public async Task DeleteCodeReduction_ReturnsNoContent_WhenCodeReductionIsDeleted()
        {
            // Arrange
            var codeReductionId = "1";
            var CodeReduction = new CodeReduction
            {
                ReductionId = "1",
                ActifReduction = true,
            };
            _mockDataRepository.Setup(repo => repo.GetByStringAsync(codeReductionId)).ReturnsAsync(CodeReduction);
            _mockDataRepository.Setup(repo => repo.DeleteAsync(It.IsAny<CodeReduction>())).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.DeleteCodeReduction(codeReductionId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }
        [TestMethod]
        public async Task DeleteCodeReduction_ReturnsNotFound_WhenCodeReductionDoesNotExist()
        {
            // Arrange
            var codeReductionId = "1";
            _mockDataRepository.Setup(repo => repo.GetByStringAsync(codeReductionId)).ReturnsAsync((CodeReduction)null);

            // Act
            var result = await _controller.DeleteCodeReduction("D78I6Z");

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
    }
}