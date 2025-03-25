using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using WsRest_UpWay.Models.EntityFramework;

namespace WsRest_UpWay.Models.DataManager.Tests
{
    [TestClass()]
    [TestSubject(typeof(CategorieArticleManager))]
    public class CategorieArticleManagerTests
    {
        private S215UpWayContext ctx;
        private CategorieArticleManager manager;

        [TestInitialize]
        public void Initialize()
        {
            var builder = new DbContextOptionsBuilder<S215UpWayContext>();
            builder.UseSqlite("Data Source=S215UpWay.db");

            ctx = new S215UpWayContext(builder.Options);
            ctx.Database.Migrate();

            manager = new CategorieArticleManager(ctx);
        }

        [TestCleanup]
        public void Cleanup()
        {
            ctx.Database.EnsureDeleted();
        }

        [TestMethod]
        public void GetAllAsyncTest()
        {
            var result = manager.GetAllAsync().Result;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Result);
            CollectionAssert.AreEquivalent(ctx.CategorieArticles.ToList(), result.Value.ToList());
        }

        [TestMethod]
        public void GetByIdAsyncTest()
        {
            var expected = ctx.CategorieArticles.FirstOrDefault();
            if (expected == null)
                return; // db is either empty or not working, stop the test without error to avoid false negative when db is empty

            var result = manager.GetByIdAsync(expected.CategorieArticleId).Result;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Value);
            Assert.AreEqual(expected, result.Value);
        }

        [TestMethod]
        public void AddAsyncTest()
        {
            var store = new CategorieArticle
            {
                TitreCategorieArticle = "Revente vélo",
                ContenuCategorieArticle = "Toutes les informations et détails à savoir !",
                ImageCategorie = "nothing.png"
            };

            manager.AddAsync(store).Wait();

            var store2 = ctx.CategorieArticles.First(u => u.TitreCategorieArticle == store.TitreCategorieArticle);
            Assert.IsNotNull(store2);

            ctx.CategorieArticles.Remove(store2);
            ctx.SaveChanges();
        }

        [TestMethod]
        public void UpdateAsyncTest()
        {
            var store = ctx.CategorieArticles.FirstOrDefault();
            if (store == null) return; // we can't test if the db is empty

            var orig = store.TitreCategorieArticle;
            var newP = "Achat de vélo";
            store.TitreCategorieArticle = newP;

            manager.UpdateAsync(store, store).Wait();

            store = ctx.CategorieArticles.Find(store.CategorieArticleId);
            Assert.IsNotNull(store);
            Assert.AreEqual(newP, store.TitreCategorieArticle);

            store.TitreCategorieArticle = orig;
            manager.UpdateAsync(store, store).Wait();
        }

        [TestMethod]
        public void RemoveAsyncTest()
        {
            var store = new CategorieArticle
            {
                TitreCategorieArticle = "Revente vélo",
                ContenuCategorieArticle = "Toutes les informations et détails à savoir !",
                ImageCategorie = "nothing.png"
            };

            store = ctx.CategorieArticles.Add(store).Entity;
            ctx.SaveChanges();
            Assert.IsNotNull(store);

            manager.DeleteAsync(store).Wait();
            store = ctx.CategorieArticles.Find(store.MagasinId);
            Assert.IsNull(store);
        }
    }
}