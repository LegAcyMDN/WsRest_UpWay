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

namespace WsRest_UpWay.Controllers.Tests;

[TestClass()]
public class EstRealisesControllerTests
{
    private EstRealisesController _controller;
    private Mock<IDataEstRealise> _mockDataEstRealise;

    [TestInitialize]
    public void TestInitialize()
    {
        _mockDataEstRealise = new Mock<IDataEstRealise>();
        _controller = new EstRealisesController(_mockDataEstRealise.Object);
    }

    [TestMethod]
    public async Task GetEstRealises_ReturnsOkResult_WhenEstRealiseExist()
    {
        // Arrange
        var estRealiseList = new List<EstRealise>
            {
                new EstRealise { VeloId = 1, InspectionId = 1, ReparationId = 1 },
                new EstRealise { VeloId = 2, InspectionId = 2, ReparationId = 2 }
            };
        _mockDataEstRealise.Setup(repo => repo.GetAllAsync(0)).ReturnsAsync(estRealiseList);

        // Act
        var result = await _controller.GetEstRealises();

        // Assert
        var returnedEstRealise = result.Value as List<EstRealise>;
        Assert.IsNotNull(returnedEstRealise);
        CollectionAssert.AreEquivalent(estRealiseList, returnedEstRealise);
    }

    [TestMethod]
    public async Task GetEstRealiseByIds_ReturnsOkResult_WhenEstRealiseExist()
    {
        // Arrange
        var estRealise = new EstRealise { VeloId = 1, InspectionId = 1, ReparationId = 1 };
        _mockDataEstRealise.Setup(repo => repo.GetByIdsAsync(1, 1, 1)).ReturnsAsync(estRealise);

        // Act
        var result = await _controller.GetEstRealiseByIds(1, 1, 1);

        // Assert
        var returnedEstRealise = result.Value;
        Assert.IsNotNull(returnedEstRealise);
        Assert.AreEqual(estRealise.VeloId, returnedEstRealise.VeloId);
    }

    /*[TestMethod]
    public async Task GetEstRealiseByIds_ReturnsNotFound_WhenEstRealiseDoesNotExist()
    {
        // Arrange
        _mockDataEstRealise.Setup(repo => repo.GetByIdsAsync(1, 1, 1)).ReturnsAsync((EstRealise)null);

        // Act
        var result = await _controller.GetEstRealiseByIds(1, 1, 1);

        // Assert
        Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
    }*/

    [TestMethod]
    public async Task PostEstRealise_ReturnsCreatedResult_WhenModelIsValid()
    {
        // Arrange
        var newEstRealise = new EstRealise
        {
            VeloId = 1,
            InspectionId = 1,
            ReparationId = 1
        };
        _mockDataEstRealise.Setup(repo => repo.AddAsync(It.IsAny<EstRealise>())).Returns(Task.CompletedTask);

        // Act
        var result = await _controller.PostVelo(newEstRealise);

        // Assert
        var createdAtActionResult = result.Result as CreatedAtActionResult;
        Assert.IsNotNull(createdAtActionResult);
        Assert.AreEqual("GetById", createdAtActionResult.ActionName);
        var returnedEstRealise = createdAtActionResult.Value as EstRealise;
        Assert.AreEqual(newEstRealise.VeloId, returnedEstRealise.VeloId);
        Assert.AreEqual(newEstRealise.InspectionId, returnedEstRealise.InspectionId);
        Assert.AreEqual(newEstRealise.ReparationId, returnedEstRealise.ReparationId);
    }

    [TestMethod]
    public async Task PostEstRealise_ReturnsBadRequest_WhenModelIsInvalid()
    {
        // Arrange
        var newEstRealise = new EstRealise();
        _controller.ModelState.AddModelError("VeloId", "Required");

        // Act
        var result = await _controller.PostVelo(newEstRealise);

        // Assert
        var badRequestResult = result.Result as BadRequestObjectResult;
        Assert.IsNotNull(badRequestResult);
    }

    [TestMethod]
    public async Task PutEstRealise_ReturnsNoContent_WhenEstRealiseIsUpdated()
    {
        // Arrange
        var updatedEstRealise = new EstRealise
        {
            VeloId = 1,
            InspectionId = 1,
            ReparationId = 1
        };
        _mockDataEstRealise.Setup(repo => repo.GetByIdsAsync(1, 1, 1)).ReturnsAsync(updatedEstRealise);
        _mockDataEstRealise.Setup(repo => repo.UpdateAsync(It.IsAny<EstRealise>(), It.IsAny<EstRealise>())).Returns(Task.CompletedTask);

        // Act
        var result = await _controller.PutEstRealise(1, 1, 1, updatedEstRealise);

        // Assert
        Assert.IsInstanceOfType(result, typeof(NoContentResult));
    }

    /*[TestMethod]
    public async Task PutEstRealise_ReturnsNotFound_WhenEstRealiseDoesNotExist()
    {
        // Arrange
        var updatedEstRealise = new EstRealise
        {
            VeloId = 99,
            InspectionId = 185,
            ReparationId = 1102
        };
        _mockDataEstRealise.Setup(repo => repo.GetByIdsAsync(99, 185, 1102)).ReturnsAsync((EstRealise)null);

        // Act
        var result = await _controller.PutEstRealise(99, 185, 1102, updatedEstRealise);

        // Assert
        Assert.IsInstanceOfType(result, typeof(NotFoundResult));
    }*/

    [TestMethod]
    public async Task DeleteEstRealise_ReturnsNoContent_WhenEstRealiseIsDeleted()
    {
        // Arrange
        var veloId = 1;
        var estRealise = new EstRealise
        {
            VeloId = 1,
            InspectionId = 1,
            ReparationId = 1
        };
        _mockDataEstRealise.Setup(repo => repo.GetByIdAsync(veloId)).ReturnsAsync(estRealise);
        _mockDataEstRealise.Setup(repo => repo.DeleteAsync(It.IsAny<EstRealise>())).Returns(Task.CompletedTask);

        // Act
        var result = await _controller.DeleteVelo(veloId);

        // Assert
        Assert.IsInstanceOfType(result, typeof(NoContentResult));
    }

    [TestMethod]
    public async Task DeleteEstRealise_ReturnsNotFound_WhenEstRealiseDoesNotExist()
    {
        // Arrange
        _mockDataEstRealise.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((EstRealise)null);

        // Act
        var result = await _controller.DeleteVelo(1);

        // Assert
        Assert.IsInstanceOfType(result, typeof(NotFoundResult));
    }
}