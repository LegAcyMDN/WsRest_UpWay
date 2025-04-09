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
public class PhotoAccessoiresControllerTests
{
    private Mock<IDataRepository<PhotoAccessoire>> _mockRepo;
    private PhotoAccessoiresController _controller;

    [TestInitialize]
    public void Setup()
    {
        _mockRepo = new Mock<IDataRepository<PhotoAccessoire>>();
        _controller = new PhotoAccessoiresController(_mockRepo.Object);
    }

    [TestMethod]
    public async Task GetPhotoAccessoireById_ExistingId_ReturnsPhotoAccessoire()
    {
        // Arrange
        var photo = new PhotoAccessoire { PhotoAcessoireId = 1, AccessoireId = 10, UrlPhotoAccessoire = "url" };
        _mockRepo.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(photo);

        // Act
        var result = await _controller.Get(1);

        // Assert
        Assert.IsNotNull(result.Value);
        Assert.AreEqual(photo.PhotoAcessoireId, result.Value.PhotoAcessoireId);
    }

    [TestMethod]
    public async Task GetPhotoAccessoireById_UnknownId_ReturnsNotFound()
    {
        // Arrange
        _mockRepo.Setup(x => x.GetByIdAsync(99)).ReturnsAsync((PhotoAccessoire)null!);

        // Act
        var result = await _controller.Get(99);

        // Assert
        Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
    }

    [TestMethod]
    public async Task GetAllPhotoAccessoires_ReturnsList()
    {
        // Arrange
        var list = new List<PhotoAccessoire> {
                new PhotoAccessoire { PhotoAcessoireId = 1, AccessoireId = 10 },
                new PhotoAccessoire { PhotoAcessoireId = 2, AccessoireId = 11 }
            };
        _mockRepo.Setup(x => x.GetAllAsync(0)).ReturnsAsync(list);

        // Act
        var result = await _controller.Gets();

        // Assert
        Assert.AreEqual(2, result.Value?.Count());
    }

    [TestMethod]
    public async Task PostPhotoAccessoire_Valid_ReturnsCreated()
    {
        // Arrange
        var photo = new PhotoAccessoire { PhotoAcessoireId = 1, AccessoireId = 2, UrlPhotoAccessoire = "img.jpg" };

        // Act
        var result = await _controller.PostPhotoAccessoire(photo);

        // Assert
        Assert.IsInstanceOfType(result.Result, typeof(CreatedAtActionResult));
        var created = result.Result as CreatedAtActionResult;
        Assert.AreEqual(photo, created?.Value);
    }

    [TestMethod]
    public async Task PutPhotoAccessoire_ValidUpdate_ReturnsNoContent()
    {
        // Arrange
        var original = new PhotoAccessoire { PhotoAcessoireId = 1, AccessoireId = 2 };
        var updated = new PhotoAccessoire { PhotoAcessoireId = 1, AccessoireId = 2, UrlPhotoAccessoire = "new.jpg" };
        _mockRepo.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(original);

        // Act
        var result = await _controller.PutPhotoAccessoire(1, updated);

        // Assert
        Assert.IsInstanceOfType(result, typeof(NoContentResult));
    }

    [TestMethod]
    public async Task PutPhotoAccessoire_IdMismatch_ReturnsBadRequest()
    {
        // Arrange
        var updated = new PhotoAccessoire { PhotoAcessoireId = 2 };

        // Act
        var result = await _controller.PutPhotoAccessoire(1, updated);

        // Assert
        Assert.IsInstanceOfType(result, typeof(BadRequestResult));
    }

    [TestMethod]
    public async Task DeletePhotoAccessoire_Existing_ReturnsNoContent()
    {
        // Arrange
        var photo = new PhotoAccessoire { PhotoAcessoireId = 1, AccessoireId = 2 };
        _mockRepo.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(photo);

        // Act
        var result = await _controller.DeletePhotoAccessoire(1);

        // Assert
        Assert.IsInstanceOfType(result, typeof(NoContentResult));
    }

    [TestMethod]
    public async Task DeletePhotoAccessoire_UnknownId_ReturnsNotFound()
    {
        // Arrange
        _mockRepo.Setup(x => x.GetByIdAsync(0)).ReturnsAsync((PhotoAccessoire)null!);

        // Act
        var result = await _controller.DeletePhotoAccessoire(0);

        // Assert
        Assert.IsInstanceOfType(result, typeof(NotFoundResult));
    }

    [TestMethod]
    public async Task GetPhotoAccessoiresCount_ReturnsCount()
    {
        // Arrange
        _mockRepo.Setup(x => x.GetCountAsync()).ReturnsAsync(5);

        // Act
        var result = await _controller.Count();

        // Assert
        Assert.AreEqual(5, result.Value);
    }
}