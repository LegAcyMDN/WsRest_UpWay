using Microsoft.VisualStudio.TestTools.UnitTesting;
using WsRest_UpWay.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WsRest_UpWay.Models.EntityFramework;
using WsRest_UpWay.Models.Repository;
using Microsoft.EntityFrameworkCore;
using WsRest_UpWay.Models.DataManager;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace WsRest_UpWay.Controllers.Tests
{
    [TestClass()]
    public class CategorieArticlesControllerTests
    {
        [TestMethod]
        public void GetCategorieArticleById_ExistingIdPassed_ReturnsRightItem_AvecMoq()
        {
            // Arrange
            CategorieArticle catArticle = new CategorieArticle
            {
                CategorieArticleId = 1,
                TitreCategorieArticle = "Revente vélo",
                ContenuCategorieArticle = "Toutes les informations et détails à savoir !",
                ImageCategorie = "nothing.png"
            };
            var mockRepository = new Mock<IDataRepository<CategorieArticle>>();
            mockRepository.Setup(x => x.GetByIdAsync(1).Result).Returns(catArticle);
            var catArticleController = new CategorieArticlesController(mockRepository.Object);

            // Act
            var actionResult = catArticleController.GetCategorieArticleById(1).Result;

            // Assert   
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            Assert.AreEqual(catArticle, actionResult.Value as CategorieArticle);
        }

        [TestMethod]
        public void GetCategorieArticleById_UnknownIdPassed_ReturnsNotFoundResult_AvecMoq()
        {
            var mockRepository = new Mock<IDataRepository<CategorieArticle>>();
            var catArticleController = new CategorieArticlesController(mockRepository.Object);

            // Act
            var actionResult = catArticleController.GetCategorieArticleById(0).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void GetCategorieArticleByTitre_ExistingTitrePassed_AvecMoq()
        {
            // Arrange
            CategorieArticle catArticle = new CategorieArticle
            {
                CategorieArticleId = 1,
                TitreCategorieArticle = "Revente vélo",
                ContenuCategorieArticle = "Toutes les informations et détails à savoir !",
                ImageCategorie = "nothing.png"
            };
            var mockRepository = new Mock<IDataRepository<CategorieArticle>>();
            mockRepository.Setup(x => x.GetByStringAsync("Revente vélo").Result).Returns(catArticle);
            var catArticleController = new CategorieArticlesController(mockRepository.Object);

            // Act
            var actionResult = catArticleController.GetCategorieArticleByTitre("Revente vélo").Result;

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            Assert.AreEqual(catArticle, actionResult.Value as CategorieArticle);
        }

        [TestMethod]
        public void GetCategorieArticleByTitre_UnknownTitrePassed_AvecMoq()
        {
            var mockRepository = new Mock<IDataRepository<CategorieArticle>>();
            var catArticleController = new CategorieArticlesController(mockRepository.Object);

            // Act
            var actionResult = catArticleController.GetCategorieArticleByTitre("On ne vend pas de switch !").Result;

            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void PutCategorieArticleTest_AvecMoq()
        {
            CategorieArticle catArticleToEdit = new CategorieArticle
            {
                CategorieArticleId = 1,
                TitreCategorieArticle = "Revente vélo",
                ContenuCategorieArticle = "Toutes les informations et détails à savoir !",
                ImageCategorie = "nothing.png"
            };
            // Arrange
            CategorieArticle catArticleEdited = new CategorieArticle
            {
                CategorieArticleId = 1,
                TitreCategorieArticle = "Revente vélo",
                ContenuCategorieArticle = "Regarder notre vidéo pour plus d'informations et de détails !",
                ImageCategorie = "nothing.png"
            };
            var mockRepository = new Mock<IDataRepository<CategorieArticle>>();
            mockRepository.Setup(x => x.GetByIdAsync(1).Result).Returns(catArticleToEdit);
            var catArticleController = new CategorieArticlesController(mockRepository.Object);

            // Act
            var actionResult = catArticleController.PutCategorieArticle(1, catArticleEdited).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult");
        }

        [TestMethod]
        public void PostCategorieArticleTest_ModelValidated_CreationOK()
        {
            // Arrange
            var mockRepository = new Mock<IDataRepository<CategorieArticle>>();
            var catArticleController = new CategorieArticlesController(mockRepository.Object);
            CategorieArticle catArticle = new CategorieArticle
            {
                CategorieArticleId = 2,
                TitreCategorieArticle = "Revente vélo",
                ContenuCategorieArticle = "Toutes les informations et détails à savoir !",
                ImageCategorie = "nothing.png"
            };

            // Act
            var actionResult = catArticleController.PostCategorieArticle(catArticle).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<CategorieArticle>), "Pas un ActionResult<CategorieArticle>");
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");
            var result = actionResult.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(result.Value, typeof(CategorieArticle), "Pas un CategorieArticle");
            catArticle.CategorieArticleId = ((CategorieArticle)result.Value).CategorieArticleId;
            Assert.AreEqual(catArticle, (CategorieArticle)result.Value, "CategorieArticles pas identiques");
        }


        [TestMethod]
        public void DeleteCategorieArticleTest_AvecMoq()
        {
            // Arrange
            CategorieArticle catArticle = new CategorieArticle
            {
                CategorieArticleId = 2,
                TitreCategorieArticle = "Revente vélo",
                ContenuCategorieArticle = "Regarder notre vidéo pour plus d'informations et de détails !",
                ImageCategorie = "nothing.png"
            };
            var mockRepository = new Mock<IDataRepository<CategorieArticle>>();
            mockRepository.Setup(x => x.GetByIdAsync(2).Result).Returns(catArticle);
            var catArticleController = new CategorieArticlesController(mockRepository.Object);
            
            // Act
            var actionResult = catArticleController.DeleteCategorieArticle(2).Result;
            
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult"); // Test du type de retour
        }
    }
}