using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WsRest_UpWay.Models.EntityFramework;
using WsRest_UpWay.Models.Repository;

namespace WsRest_UpWay.Controllers.Tests;

[TestClass]
public class AccessoiresControllerTests
{
    private AccessoiresController _controller;
    private Mock<IDataAccessoire> _mockDataRepository;

    // Initialisation mocks
    [TestInitialize]
    public void TestInitialize()
    {
        _mockDataRepository = new Mock<IDataAccessoire>();
        _controller = new AccessoiresController(_mockDataRepository.Object);
    }

    // Test GetAccessoires() Exist
    [TestMethod]
    public async Task GetAccessoires_ReturnsOkResult_WhenAccessoiresExist()
    {
        // Arrange
        var accessoires = new List<Accessoire>
        {
            new() { AccessoireId = 1, NomAccessoire = "Accessoire 1" },
            new() { AccessoireId = 2, NomAccessoire = "Accessoire 2" }
        };
        _mockDataRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(accessoires);

        // Act
        var result = await _controller.GetAccessoires();

        // Assert
        var returnedAccessoires = result.Value as List<Accessoire>;
        Assert.IsNotNull(returnedAccessoires);
        CollectionAssert.AreEquivalent(accessoires, returnedAccessoires);
    }

    // Test GetAccessoire() NotExist
    [TestMethod]
    public async Task GetAccessoire_ReturnsNotFound_WhenAccessoireDoesNotExist()
    {
        // Arrange
        var accessoireId = 1;
        _mockDataRepository.Setup(repo => repo.GetByIdAsync(accessoireId)).ReturnsAsync((Accessoire)null);

        // Act
        var result = await _controller.GetAccessoire(accessoireId);

        // Assert
        Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
    }

    // Test GetAccessoireId() Ok
    [TestMethod]
    public async Task GetAccessoire_ReturnsOkResult_WhenAccessoireIdExists()
    {
        // Arrange
        var accessoireId = 1;
        var accessoire = new Accessoire { AccessoireId = accessoireId, NomAccessoire = "Accessoire 1" };
        _mockDataRepository.Setup(repo => repo.GetByIdAsync(accessoireId)).ReturnsAsync(accessoire);

        // Act
        var result = await _controller.GetAccessoire(accessoireId);

        // Assert
        var returnedAccessoire = result.Value;
        Assert.IsNotNull(returnedAccessoire);
        Assert.AreEqual(accessoireId, returnedAccessoire.AccessoireId);
    }

    // Test PostAccessoire() Valid
    [TestMethod]
    public async Task PostAccessoire_ReturnsCreatedResult_WhenModelIsValid()
    {
        // Arrange
        var newAccessoire = new Accessoire { AccessoireId = 1, NomAccessoire = "Accessoire 1" };
        _mockDataRepository.Setup(repo => repo.AddAsync(It.IsAny<Accessoire>())).Returns(Task.CompletedTask);

        // Act
        var result = await _controller.PostAccessoire(newAccessoire);

        // Assert
        var createdAtActionResult = result.Result as CreatedAtActionResult;
        Assert.IsNotNull(createdAtActionResult);
        Assert.AreEqual("GetById", createdAtActionResult.ActionName);
        Assert.AreEqual(newAccessoire.AccessoireId, createdAtActionResult.RouteValues["id"]);
    }

    // Test PostAccessoire() Invalid
    [TestMethod]
    public async Task PostAccessoire_ReturnsBadRequest_WhenModelIsInvalid()
    {
        // Arrange
        var newAccessoire = new Accessoire();
        _controller.ModelState.AddModelError("NomAccessoire", "Required");

        // Act
        var result = await _controller.PostAccessoire(newAccessoire);

        // Assert
        var badRequestResult = result.Result as BadRequestObjectResult;
        Assert.IsNotNull(badRequestResult);
    }

    // Test pour PutAccessoire()
    [TestMethod]
    public async Task PutAccessoire_ReturnsNoContent_WhenAccessoireIsUpdated()
    {
        // Arrange
        var accessoireId = 1;
        var updatedAccessoire = new Accessoire { AccessoireId = accessoireId, NomAccessoire = "Updated Accessoire" };
        _mockDataRepository.Setup(repo => repo.GetByIdAsync(accessoireId)).ReturnsAsync(updatedAccessoire);
        _mockDataRepository.Setup(repo => repo.UpdateAsync(It.IsAny<Accessoire>(), It.IsAny<Accessoire>()))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _controller.PutAccessoire(accessoireId, updatedAccessoire);

        // Assert
        Assert.IsInstanceOfType(result, typeof(NoContentResult));
    }

    // Test pour PutAccessoire() NotExist
    [TestMethod]
    public async Task PutAccessoire_ReturnsNotFound_WhenAccessoireDoesNotExist()
    {
        // Arrange
        var accessoireId = 1;
        var updatedAccessoire = new Accessoire { AccessoireId = accessoireId, NomAccessoire = "Updated Accessoire" };
        _mockDataRepository.Setup(repo => repo.GetByIdAsync(accessoireId)).ReturnsAsync((Accessoire)null);

        // Act
        var result = await _controller.PutAccessoire(accessoireId, updatedAccessoire);

        // Assert
        Assert.IsInstanceOfType(result, typeof(NotFoundResult));
    }

    // Test pour DeleteAccessoire()
    [TestMethod]
    public async Task DeleteAccessoire_ReturnsNoContent_WhenAccessoireIsDeleted()
    {
        // Arrange
        var accessoireId = 1;
        var accessoire = new Accessoire { AccessoireId = accessoireId };
        _mockDataRepository.Setup(repo => repo.GetByIdAsync(accessoireId)).ReturnsAsync(accessoire);
        _mockDataRepository.Setup(repo => repo.DeleteAsync(It.IsAny<Accessoire>())).Returns(Task.CompletedTask);

        // Act
        var result = await _controller.DeleteAccessoire(accessoireId);

        // Assert
        Assert.IsInstanceOfType(result, typeof(NoContentResult));
    }

    // Test pour DeleteAccessoire() NotExist
    [TestMethod]
    public async Task DeleteAccessoire_ReturnsNotFound_WhenAccessoireDoesNotExist()
    {
        // Arrange
        var accessoireId = 1;
        _mockDataRepository.Setup(repo => repo.GetByIdAsync(accessoireId)).ReturnsAsync((Accessoire)null);

        // Act
        var result = await _controller.DeleteAccessoire(accessoireId);

        // Assert
        Assert.IsInstanceOfType(result, typeof(NotFoundResult));
    }
}