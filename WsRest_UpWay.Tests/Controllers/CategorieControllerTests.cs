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
    public class CategoriesControllerTests
    {
        [TestMethod]
        public void GetCategorieById_ExistingIdPassed_ReturnsRightItem_AvecMoq()
        {
            // Arrange
            Categorie cat = new Categorie
            {
                CategorieId = 1,
                LibelleCategorie = "nothing.png"
            };
            var mockRepository = new Mock<IDataRepository<Categorie>>();
            mockRepository.Setup(x => x.GetByIdAsync(1).Result).Returns(cat);
            var catController = new CategoriesController(mockRepository.Object);

            // Act
            var actionResult = catController.GetCategorieById(1).Result;

            // Assert   
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            Assert.AreEqual(cat, actionResult.Value as Categorie);
        }

        [TestMethod]
        public void GetCategorieById_UnknownIdPassed_ReturnsNotFoundResult_AvecMoq()
        {
            var mockRepository = new Mock<IDataRepository<Categorie>>();
            var catController = new CategoriesController(mockRepository.Object);

            // Act
            var actionResult = catController.GetCategorieById(0).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void GetCategorieByTitre_ExistingTitrePassed_AvecMoq()
        {
            // Arrange
            Categorie cat = new Categorie
            {
                CategorieId = 1,
                LibelleCategorie = "nothing.png"
            };
            var mockRepository = new Mock<IDataRepository<Categorie>>();
            mockRepository.Setup(x => x.GetByStringAsync("Revente vélo").Result).Returns(cat);
            var catController = new CategoriesController(mockRepository.Object);

            // Act
            var actionResult = catController.GetCategorieByTitre("Revente vélo").Result;

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            Assert.AreEqual(cat, actionResult.Value as Categorie);
        }

        [TestMethod]
        public void GetCategorieByTitre_UnknownTitrePassed_AvecMoq()
        {
            var mockRepository = new Mock<IDataRepository<Categorie>>();
            var catController = new CategoriesController(mockRepository.Object);

            // Act
            var actionResult = catController.GetCategorieByTitre("On ne vend pas de switch !").Result;

            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void PutCategorieTest_AvecMoq()
        {
            Categorie catToEdit = new Categorie
            {
                CategorieId = 1,
                LibelleCategorie = "nothing.png"
            };
            // Arrange
            Categorie catEdited = new Categorie
            {
                CategorieId = 1,
                LibelleCategorie = "nothing.png"
            };
            var mockRepository = new Mock<IDataRepository<Categorie>>();
            mockRepository.Setup(x => x.GetByIdAsync(1).Result).Returns(catToEdit);
            var catController = new CategoriesController(mockRepository.Object);

            // Act
            var actionResult = catController.PutCategorie(1, catEdited).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult");
        }

        [TestMethod]
        public void PostCategorieTest_ModelValidated_CreationOK()
        {
            // Arrange
            var mockRepository = new Mock<IDataRepository<Categorie>>();
            var catController = new CategoriesController(mockRepository.Object);
            Categorie cat = new Categorie
            {
                CategorieId = 2,
                LibelleCategorie = "nothing.png"
            };

            // Act
            var actionResult = catController.PostCategorie(cat).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<Categorie>), "Pas un ActionResult<Categorie>");
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");
            var result = actionResult.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(result.Value, typeof(Categorie), "Pas un Categorie");
            cat.CategorieId = ((Categorie)result.Value).CategorieId;
            Assert.AreEqual(cat, (Categorie)result.Value, "Categories pas identiques");
        }


        [TestMethod]
        public void DeleteCategorieTest_AvecMoq()
        {
            // Arrange
            Categorie cat = new Categorie
            {
                CategorieId = 2,
                LibelleCategorie = "nothing.png"
            };
            var mockRepository = new Mock<IDataRepository<Categorie>>();
            mockRepository.Setup(x => x.GetByIdAsync(2).Result).Returns(cat);
            var catController = new CategoriesController(mockRepository.Object);

            // Act
            var actionResult = catController.DeleteCategorie(2).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult"); // Test du type de retour
        }
    }
}
