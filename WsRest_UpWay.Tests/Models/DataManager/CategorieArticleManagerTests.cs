using System.IO;
using System.Linq;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WsRest_UpWay.Models.EntityFramework;
using MemoryCache = WsRest_UpWay.Models.Cache.MemoryCache;

namespace WsRest_UpWay.Models.DataManager.Tests;

[TestClass]
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
        ctx.Database.ExecuteSqlRaw(File.ReadAllText("inserts.sql"));

        manager = new CategorieArticleManager(ctx, new MemoryCache(
            new Microsoft.Extensions.Caching.Memory.MemoryCache(new MemoryCacheOptions()),
            new ConfigurationManager()));
    }

    [TestCleanup]
    public void Cleanup()
    {
        ctx.Database.EnsureDeleted();
    }

    [TestMethod]
    public void GetAllAsyncTest()
    {
        var result = manager.GetAllAsync(0).Result;

        Assert.IsNotNull(result);
        Assert.IsNotNull(result.Value);
        CollectionAssert.AreEquivalent(ctx.CategorieArticles.ToList(), result.Value.ToList());
    }

    [TestMethod]
    public void GetByIdAsyncTest()
    {
        var expected = ctx.CategorieArticles.FirstOrDefault();
        Assert.IsNotNull(expected, "Expected CategoryArticle object to exist.");

        var result = manager.GetByIdAsync(expected.CategorieArticleId).Result;

        Assert.IsNotNull(result);
        Assert.IsNotNull(result.Value);
        Assert.AreEqual(expected, result.Value);
    }

    [TestMethod]
    public void GetByStringAsyncTest()
    {
        var expected = ctx.CategorieArticles.FirstOrDefault();
        Assert.IsNotNull(expected, "Expected CategoryArticle object to exist.");

        var result = manager.GetByStringAsync(expected.TitreCategorieArticle).Result;

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
    }

    [TestMethod]
    public void UpdateAsyncTest()
    {
        var store = ctx.CategorieArticles.FirstOrDefault();
        Assert.IsNotNull(store, "Expected CategoryArticle object to exist.");

        var newP = "Achat de vélo";
        store.TitreCategorieArticle = newP;

        manager.UpdateAsync(store, store).Wait();

        store = ctx.CategorieArticles.Find(store.CategorieArticleId);
        Assert.IsNotNull(store);
        Assert.AreEqual(newP, store.TitreCategorieArticle);
    }

    [TestMethod]
    public void RemoveAsyncTest()
    {
        var category = new CategorieArticle
        {
            TitreCategorieArticle = "Titre de categorie",
            ContenuCategorieArticle = "Contenu de categorie",
            ImageCategorie = "nothing.png"
        };
        category = ctx.CategorieArticles.Add(category).Entity;
        ctx.SaveChanges();
        Assert.IsNotNull(category);

        manager.DeleteAsync(category).Wait();
        category = ctx.CategorieArticles.Find(category.CategorieArticleId);
        Assert.IsNull(category);
    }
}