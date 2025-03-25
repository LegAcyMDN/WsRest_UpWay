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
        private CategorieArticlesController _controller;
        private S215UpWayContext _context;
        private IDataRepository<CategorieArticle> _dataRepository;

        [TestInitialize]
        public void Inititialize()
        {
            var builder = new DbContextOptionsBuilder<S215UpWayContext>().UseNpgsql("");
            _context = new S215UpWayContext(builder.Options);
            _dataRepository = new CategorieArticleManager(_context);
            _controller = new CategorieArticlesController(_dataRepository);
        }

        [TestMethod()]
        public void GetCategoriesArticlesTest()
        {
            // Arrange
            List<CategorieArticle> expected = _context.CategorieArticles.ToList();
            // Act
            var res = _controller.GetCategoriesArticles().Result;
            // Assert
            CollectionAssert.AreEqual(expected, res.Value.ToList(), "les listes ne sont pas identiques");
        }

        [TestMethod()]
        public void GetCategoriesArticleByIdTest_Success()
        {
            // Arrange
            int id = 1;
            CategorieArticle expected = _context.CategorieArticles.Find(id);
            // Act
            var res = _controller.GetCategorieArticleById(1).Result;
            // Assert
            Assert.AreEqual(expected, res.Value);
        }

        [TestMethod()]
        public void GetCategoriesArticleByIdTest_NotFound()
        {
            // Arrange
            int id = -1;
            // Act
            var res = _controller.GetCategorieArticleById(id).Result;
            // Assert
            Assert.IsNull(res.Value);
        }

        [TestMethod()]
        public void GetCategoriesArticleByTitreTest_Success()
        {
            // Arrange
            String titre = "";
            CategorieArticle expected = _context.CategorieArticles.FirstOrDefault(u => u.TitreCategorieArticle == titre);
            // Act
            var res = _controller.GetCategorieArticleByTitre(titre).Result;
            // Assert
            Assert.AreEqual(expected, res.Value);
        }

        [TestMethod()]
        public void GetCategoriesArticleByTitreTest_NotFound()
        {
            // Arrange
            String titre = "";
            // Act
            var res = _controller.GetCategorieArticleByTitre(titre).Result;
            // Assert
            Assert.IsNull(res.Value);
        }

        [TestMethod()]
        public void PutCategorieArticleTest()
        {
            // Arrange
            String titre = "";
            int id = 1;
            CategorieArticle catArticleATester = _context.CategorieArticles.Find(id);
            catArticleATester.TitreCategorieArticle = $"{titre}";

            // Act
            var res = _controller.PutCategorieArticle(id, catArticleATester);

            // Arrange
            CategorieArticle catArticleMisAJour = _context.CategorieArticles.Find(id);
            Assert.AreEqual(catArticleATester, catArticleMisAJour);
        }

        [TestMethod()]
        public void DeleteEtudiantTest()
        {
            // Arrange
            CategorieArticle catArticleATester = new CategorieArticle
            {
                CategorieArticleId = 100,
                TitreCategorieArticle = "Méthode d'insertion",
                ContenuCategorieArticle = "",
                ImageCategorie = "",
            };
            _context.CategorieArticles.Add(catArticleATester);
            _context.SaveChanges();

            // Act
            CategorieArticle deletedcatArticle = _context.CategorieArticles.FirstOrDefault(u => u.TitreCategorieArticle == catArticleATester.TitreCategorieArticle);
            _ = _controller.DeleteCategorieArticle(deletedcatArticle.CategorieArticleId).Result;

            // Arrange
            CategorieArticle res = _context.CategorieArticles.FirstOrDefault(u => u.TitreCategorieArticle == catArticleATester.TitreCategorieArticle);
            Assert.IsNull(res, "etudiant non supprimé");
        }
    }
}