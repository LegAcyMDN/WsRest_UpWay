using System;
using System.IO;
using System.Linq;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WsRest_UpWay.Models.EntityFramework;
using MemoryCache = WsRest_UpWay.Models.Cache.MemoryCache;

namespace WsRest_UpWay.Models.DataManager.Tests
{
    [TestClass()]
    [TestSubject(typeof(ContenuArticlesManager))]
    public class ContenuArticlesManagerTests
    {
        private S215UpWayContext ctx;
        private ContenuArticlesManager manager;

        [TestInitialize]
        public void Initialize()
        {
            var builder = new DbContextOptionsBuilder<S215UpWayContext>();
            builder.UseSqlite("Data Source=S215UpWay.db");

            ctx = new S215UpWayContext(builder.Options);
            ctx.Database.Migrate();
            ctx.Database.ExecuteSqlRaw(File.ReadAllText("inserts.sql"));

            manager = new ContenuArticlesManager(ctx, new MemoryCache(
                new Microsoft.Extensions.Caching.Memory.MemoryCache(new MemoryCacheOptions()),
                new ConfigurationManager()));
        }

        [TestCleanup]
        public void Cleanup()
        {
            ctx.Database.EnsureDeleted();
        }

        [TestMethod()]
        public void AddAsyncTest()
        {
            var article = ctx.Articles.FirstOrDefault();
            Assert.IsNotNull(article);

            var contenu = new ContenuArticle
            {
                ArticleId = article.ArticleId,
                PrioriteContenu = 1,
                TypeContenu = "h1",
                Contenu = "J'en ai marre de cette SAE, Lukas tu as fait un contenu article merdique !!!",
            };

            manager.AddAsync(contenu).Wait();

            var contenu2 = ctx.ContenuArticles.FirstOrDefault(u => u.ContenueId == contenu.ContenueId);
            Assert.IsNotNull(contenu2);
        }

        [TestMethod()]
        public void DeleteAsyncTest()
        {
            var contenu = ctx.ContenuArticles.FirstOrDefault();
            Assert.IsNotNull(contenu);

            ctx.SaveChanges();
            manager.DeleteAsync(contenu).Wait();
            contenu = ctx.ContenuArticles.Find(contenu.ContenueId);
            Assert.IsNull(contenu);
        }

        [TestMethod()]
        public void GetAllAsyncTest()
        {
            var result = manager.GetAllAsync(0).Result;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Value);
            CollectionAssert.AreEquivalent(ctx.ContenuArticles.Take(ContenuArticlesManager.PAGE_SIZE).ToList(),
                result.Value.ToList());
        }

        [TestMethod()]
        public void GetCountAsyncTest()
        {
            var result = manager.GetCountAsync().Result;
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Value);
            Assert.AreEqual(ctx.ContenuArticles.Count(), result.Value);
        }

        [TestMethod()]
        public void GetByIdAsyncTest()
        {
            var expected = ctx.ContenuArticles.FirstOrDefault();
            Assert.IsNotNull(expected);

            var result = manager.GetByIdAsync(expected.ContenueId).Result;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Value);
            Assert.AreEqual(expected, result.Value);
        }

        [TestMethod()]
        public void GetByArticleIdAsyncTest()
        {
            var expected = ctx.ContenuArticles.FirstOrDefault();
            Assert.IsNotNull(expected);

            var result = manager.GetByIdAsync(expected.ArticleId).Result;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Value);
            Assert.AreEqual(expected, result.Value);
        }

        [TestMethod()]
        public void GetByStringAsyncTest()
        {
            var expected = ctx.ContenuArticles.FirstOrDefault();
            Assert.IsNotNull(expected);

            var result = manager.GetByStringAsync(expected.TypeContenu).Result;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Value);
            Assert.AreEqual(expected, result.Value);
        }

        [TestMethod()]
        public void UpdateAsyncTest()
        {
            var store = ctx.ContenuArticles.FirstOrDefault();
            Assert.IsNotNull(store);

            var newP = "a";
            store.TypeContenu = newP;

            manager.UpdateAsync(store, store).Wait();

            store = ctx.ContenuArticles.Find(store.ContenueId);
            Assert.IsNotNull(store);
            Assert.AreEqual(newP, store.TypeContenu);
        }
    }
}