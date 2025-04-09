using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WsRest_UpWay.Models.EntityFramework;
using WsRest_UpWay.Models.Repository;

namespace WsRest_UpWay.Controllers.Tests;

[TestClass()]
public class PhotoVelosControllerTests
{
    private Mock<IDataRepository<PhotoVelo>> _mockRepo;
    private PhotoVelosController _controller;

    [TestInitialize]
    public void Setup()
    {
        _mockRepo = new Mock<IDataRepository<PhotoVelo>>();
        _controller = new PhotoVelosController(_mockRepo.Object);
    }

    [TestMethod]
    public async Task GetPhotoVeloById_ExistingId_ReturnsPhotoVelo()
    {
        // Arrange
        var photo = new PhotoVelo { PhotoVeloId = 1, VeloId = 10, UrlPhotoVelo = "url.jpg" };
        _mockRepo.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(photo);

        // Act
        var result = await _controller.Get(1);

        // Assert
        Assert.IsNotNull(result.Value);
        Assert.AreEqual(photo.PhotoVeloId, result.Value.PhotoVeloId);
    }

    [TestMethod]
    public async Task GetPhotoVeloById_UnknownId_ReturnsNotFound()
    {
        // Arrange
        _mockRepo.Setup(x => x.GetByIdAsync(999)).ReturnsAsync((PhotoVelo)null!);

        // Act
        var result = await _controller.Get(999);

        // Assert
        Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
    }

    [TestMethod]
    public async Task GetAllPhotoVelos_ReturnsList()
    {
        // Arrange
        var list = new List<PhotoVelo>
            {
                new PhotoVelo { PhotoVeloId = 1, VeloId = 1 },
                new PhotoVelo { PhotoVeloId = 2, VeloId = 2 }
            };
        _mockRepo.Setup(x => x.GetAllAsync(0)).ReturnsAsync(list);

        // Act
        var result = await _controller.Gets();

        // Assert
        Assert.AreEqual(2, result.Value?.Count());
    }

    [TestMethod]
    public async Task PostPhotoVelo_Valid_ReturnsCreated()
    {
        // Arrange
        var photo = new PhotoVelo { PhotoVeloId = 3, VeloId = 5, UrlPhotoVelo = "velo3.jpg" };

        // Act
        var result = await _controller.PostPhotoVelo(photo);

        // Assert
        Assert.IsInstanceOfType(result.Result, typeof(CreatedAtActionResult));
        var created = result.Result as CreatedAtActionResult;
        Assert.AreEqual(photo, created?.Value);
    }

    [TestMethod]
    public async Task PutPhotoVelo_ValidUpdate_ReturnsNoContent()
    {
        // Arrange
        var original = new PhotoVelo { PhotoVeloId = 1, VeloId = 10 };
        var updated = new PhotoVelo { PhotoVeloId = 1, VeloId = 10, UrlPhotoVelo = "updated.jpg" };

        _mockRepo.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(original);

        // Act
        var result = await _controller.PutPhotoVelo(1, updated);

        // Assert
        Assert.IsInstanceOfType(result, typeof(NoContentResult));
    }

    [TestMethod]
    public async Task PutPhotoVelo_IdMismatch_ReturnsBadRequest()
    {
        // Arrange
        var updated = new PhotoVelo { PhotoVeloId = 2 };

        // Act
        var result = await _controller.PutPhotoVelo(1, updated);

        // Assert
        Assert.IsInstanceOfType(result, typeof(BadRequestResult));
    }

    [TestMethod]
    public async Task DeletePhotoVelo_Existing_ReturnsNoContent()
    {
        // Arrange
        var photo = new PhotoVelo { PhotoVeloId = 1, VeloId = 10 };
        _mockRepo.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(photo);

        // Act
        var result = await _controller.DeletePhotoVelo(1);

        // Assert
        Assert.IsInstanceOfType(result, typeof(NoContentResult));
    }

    [TestMethod]
    public async Task DeletePhotoVelo_UnknownId_ReturnsNotFound()
    {
        // Arrange
        _mockRepo.Setup(x => x.GetByIdAsync(0)).ReturnsAsync((PhotoVelo)null!);

        // Act
        var result = await _controller.DeletePhotoVelo(0);

        // Assert
        Assert.IsInstanceOfType(result, typeof(NotFoundResult));
    }

    [TestMethod]
    public async Task GetPhotoVelosCount_ReturnsCount()
    {
        // Arrange
        _mockRepo.Setup(x => x.GetCountAsync()).ReturnsAsync(3);

        // Act
        var result = await _controller.Count();

        // Assert
        Assert.AreEqual(3, result.Value);
    }
}