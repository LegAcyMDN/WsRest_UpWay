using Microsoft.VisualStudio.TestTools.UnitTesting;
using WsRest_UpWay.Controllers;
using Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WsRest_UpWay.Models.EntityFramework;
using WsRest_UpWay.Models.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.HttpResults;

namespace WsRest_UpWay.Controllers.Tests
{
    [TestClass()]
    public class ArticlesControllerTests
    {
        private ArticlesController _articlesController;
        private Mock<IDataArticles> _mockDataRepository;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockDataRepository = new Mock<IDataArticles>();
            _articlesController = new ArticlesController(_mockDataRepository.Object);
        }
        [TestMethod]
        public async Task GetArticles_ReturnsOkResult_WhenArticlesExist()
        {
            var articles = new List<Article>
            {
                new()
                {
                    ArticleId = 1,
                    CategorieArticleId = 1,
                },
                 new()
                {
                    ArticleId = 2,
                    CategorieArticleId = 2,
                }
            };
            _mockDataRepository.Setup(repo => repo.GetAllAsync(0)).ReturnsAsync(articles);

            var result = await _articlesController.GetArticles();

            var returnedArticles = result.Value as List<Article>;
            Assert.IsNotNull(returnedArticles);
            CollectionAssert.AreEquivalent(articles, returnedArticles);
        }
        //[TestMethod]
        //public async Task GetArticle_ReturnsNotFound_WhenArticleDoesNotExist()
        //{
        //var articleId = 5;

        //_mockDataRepository.Setup(repo => repo.GetByIdAsync(articleId)).ReturnsAsync((Article)null);


        //var result = await _articlesController.GetArticle(articleId);


        //Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        //}
        [TestMethod]
        public async Task GetArticle_ReturnsOkResult_WhenArticleIdExists()
        {
            // Arrange
            var articleId = 1;
            var article = new Article
            {
                ArticleId = articleId,
                CategorieArticleId = 1,
            };
            _mockDataRepository.Setup(repo => repo.GetByIdAsync(articleId)).ReturnsAsync(article);

            // Act
            var result = await _articlesController.GetArticle(articleId);

            // Assert
            var returnedArticle = result.Value;
            Assert.IsNotNull(returnedArticle);
            Assert.AreEqual(articleId, returnedArticle.ArticleId);
        }
        [TestMethod]
        public async Task PostArticle_ReturnsCreatedResult_WhenModelIsValid()
        {
            // Arrange
            var articleId = 1;
            var newArticle = new Article
            {
                ArticleId = articleId,
                CategorieArticleId = 1,
            };
            _mockDataRepository.Setup(repo => repo.AddAsync(It.IsAny<Article>())).Returns(Task.CompletedTask);

            // Act
            var result = await _articlesController.PostArticle(newArticle);

            // Assert
            var createdAtActionResult = result.Result as CreatedAtActionResult;
            Assert.IsNotNull(createdAtActionResult);
            Assert.AreEqual("Getarticle", createdAtActionResult.ActionName);
            Assert.AreEqual(newArticle.ArticleId, createdAtActionResult.RouteValues["id"]);
        }
        [TestMethod]
        public async Task PostArticle_ReturnsBadRequest_WhenModelIsInvalid()
        {
            // Arrange
            var newArticle = new Article();
            _articlesController.ModelState.AddModelError("NomArticle", "Required");

            // Act
            var result = await _articlesController.PostArticle(newArticle);

            // Assert
            var badRequestResult = result.Result as BadRequestObjectResult;
            Assert.IsNotNull(badRequestResult);
        }
        // Test pour PutArticle()
        [TestMethod]
        public async Task PutArticle_ReturnsNoContent_WhenArticleIsUpdated()
        {
            // Arrange
            var ArticleId = 1;
            var updatedArticle = new Article { ArticleId = ArticleId, CategorieArticleId = 2};
            _mockDataRepository.Setup(repo => repo.GetByIdAsync(ArticleId)).ReturnsAsync(updatedArticle);
            _mockDataRepository.Setup(repo => repo.UpdateAsync(It.IsAny<Article>(), It.IsAny<Article>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _articlesController.PutArticle(ArticleId, updatedArticle);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }
        [TestMethod]
        public async Task PutArticle_ReturnsNotFound_WhenArticleDoesNotExist()
        {
            // Arrange
            var ArticleId = 1;
            var updatedArticle = new Article { ArticleId = ArticleId, CategorieArticleId = 2 };
            _mockDataRepository.Setup(repo => repo.GetByIdAsync(ArticleId)).ReturnsAsync((Article)null);

            // Act
            var result = await _articlesController.PutArticle(ArticleId, updatedArticle);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
        [TestMethod]
        public async Task DeleteArticle_ReturnsNoContent_WhenArticleIsDeleted()
        {
            // Arrange
            var ArticleId = 1;
            var Article = new Article { ArticleId = ArticleId };
            _mockDataRepository.Setup(repo => repo.GetByIdAsync(ArticleId)).ReturnsAsync(Article);
            _mockDataRepository.Setup(repo => repo.DeleteAsync(It.IsAny<Article>())).Returns(Task.CompletedTask);

            // Act
            var result = await _articlesController.DeleteArticle(ArticleId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }
        [TestMethod]
        public async Task DeleteArticle_ReturnsNotFound_WhenArticleDoesNotExist()
        {
            // Arrange
            var ArticleId = 1;
            _mockDataRepository.Setup(repo => repo.GetByIdAsync(ArticleId)).ReturnsAsync((Article)null);

            // Act
            var result = await _articlesController.DeleteArticle(ArticleId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
    }
}