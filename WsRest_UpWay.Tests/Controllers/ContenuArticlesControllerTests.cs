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
public class ContenuArticlesControllerTests
{

    private ContenuArticlesController _controller;
    private Mock<IDataContenuArticles> _mockDataRepository;

    [TestInitialize]
    public void Initialize()
    {
        _mockDataRepository = new Mock<IDataContenuArticles>();
        _controller = new ContenuArticlesController(_mockDataRepository.Object);
    }

    [TestMethod]
    public async Task GetContenuArticles_ReturnsAllContenus()
    {
        var contenus = new List<ContenuArticle>
            {
                new() { ContenueId = 1, ArticleId = 1 },
                new() { ContenueId = 2, ArticleId = 1 }
            };

        _mockDataRepository.Setup(repo => repo.GetAllAsync(0)).ReturnsAsync(contenus);

        var result = await _controller.GetContenuArticles();

        var returned = result.Value.ToList();
        Assert.IsNotNull(returned);
        Assert.AreEqual(2, returned.Count);
    }

    [TestMethod]
    public async Task GetContenuArticle_ReturnsCorrectItem_WhenFound()
    {
        var contenu = new ContenuArticle { ContenueId = 1, ArticleId = 1 };
        _mockDataRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(contenu);

        var result = await _controller.GetContenuArticle(1);

        Assert.IsNotNull(result.Value);
        Assert.AreEqual(1, result.Value.ContenueId);
    }

    /*[TestMethod]
    public async Task GetContenuArticle_ReturnsNotFound_WhenNotFound()
    {
        _mockDataRepository.Setup(repo => repo.GetByIdAsync(99)).ReturnsAsync((ContenuArticle)null!);

        var result = await _controller.GetContenuArticle(99);

        Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
    }*/

    [TestMethod]
    public async Task GetByArticleId_ReturnsContent_WhenExists()
    {
        var contenus = new List<ContenuArticle> { new() { ContenueId = 1, ArticleId = 5 } };
        _mockDataRepository.Setup(repo => repo.GetByArticleIdAsync(5)).ReturnsAsync(contenus);

        var result = await _controller.GetByArticleId(5);

        Assert.IsNotNull(result.Value);
        Assert.AreEqual(1, result.Value.Count());
    }

    /*[TestMethod]
    public async Task GetByArticleId_ReturnsNotFound_WhenEmpty()
    {
        var contenuArticleId = 999;
        _mockDataRepository.Setup(repo => repo.GetByArticleIdAsync(contenuArticleId)).ReturnsAsync(new List<ContenuArticle>());

        var result = await _controller.GetByArticleId(contenuArticleId);

        Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
    }*/

    [TestMethod]
    public async Task PostContenuArticle_ReturnsCreatedAtAction_WhenValid()
    {
        var contenu = new ContenuArticle { ContenueId = 1, ArticleId = 1 };

        _mockDataRepository.Setup(repo => repo.AddAsync(It.IsAny<ContenuArticle>())).Returns(Task.CompletedTask);

        var result = await _controller.PostContenuArticle(contenu);

        var createdResult = result.Result as CreatedAtActionResult;
        Assert.IsNotNull(createdResult);
        Assert.AreEqual("GetContenuArticle", createdResult.ActionName);
        Assert.AreEqual(contenu.ContenueId, createdResult.RouteValues["id"]);
    }

    [TestMethod]
    public async Task PostContenuArticle_ReturnsBadRequest_WhenModelInvalid()
    {
        var contenu = new ContenuArticle();
        _controller.ModelState.AddModelError("TypeContenu", "Required");

        var result = await _controller.PostContenuArticle(contenu);

        Assert.IsInstanceOfType(result.Result, typeof(BadRequestObjectResult));
    }

    [TestMethod]
    public async Task PutContenuArticle_ReturnsNoContent_WhenSuccessful()
    {
        var contenu = new ContenuArticle { ContenueId = 1, ArticleId = 1 };
        _mockDataRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(contenu);
        _mockDataRepository.Setup(repo => repo.UpdateAsync(It.IsAny<ContenuArticle>(), It.IsAny<ContenuArticle>())).Returns(Task.CompletedTask);

        var result = await _controller.PutContenuArticle(1, contenu);

        Assert.IsInstanceOfType(result, typeof(NoContentResult));
    }

    [TestMethod]
    public async Task PutContenuArticle_ReturnsNotFound_WhenMissing()
    {
        var contenu = new ContenuArticle { ContenueId = 1, ArticleId = 1 };
        _mockDataRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((ContenuArticle)null!);

        var result = await _controller.PutContenuArticle(1, contenu);

        Assert.IsInstanceOfType(result, typeof(NotFoundResult));
    }

    [TestMethod]
    public async Task PutContenuArticle_ReturnsBadRequest_WhenIdMismatch()
    {
        var contenu = new ContenuArticle { ContenueId = 2, ArticleId = 1 };

        var result = await _controller.PutContenuArticle(1, contenu);

        Assert.IsInstanceOfType(result, typeof(BadRequestResult));
    }

    [TestMethod]
    public async Task DeleteContenuArticle_ReturnsNoContent_WhenDeleted()
    {
        var contenu = new ContenuArticle { ContenueId = 1, ArticleId = 1 };
        _mockDataRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(contenu);
        _mockDataRepository.Setup(repo => repo.DeleteAsync(contenu)).Returns(Task.CompletedTask);

        var result = await _controller.DeleteContenuArticle(1);

        Assert.IsInstanceOfType(result, typeof(NoContentResult));
    }

    [TestMethod]
    public async Task DeleteContenuArticle_ReturnsNotFound_WhenMissing()
    {
        _mockDataRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((ContenuArticle)null!);

        var result = await _controller.DeleteContenuArticle(1);

        Assert.IsInstanceOfType(result, typeof(NotFoundResult));
    }
}