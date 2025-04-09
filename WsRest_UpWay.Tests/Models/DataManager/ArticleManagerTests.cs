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

[TestClass()]
[TestSubject(typeof(ArticleManager))]
public class ArticleManagerTests
{
    private S215UpWayContext ctx;
    private ArticleManager manager;

    [TestInitialize]
    public void Initialize()
    {
        var builder = new DbContextOptionsBuilder<S215UpWayContext>();
        builder.UseSqlite("Data Source=S215UpWay.db");

        ctx = new S215UpWayContext(builder.Options);
        ctx.Database.Migrate();
        ctx.Database.ExecuteSqlRaw(File.ReadAllText("inserts.sql"));

        manager = new ArticleManager(ctx,
            new MemoryCache(new Microsoft.Extensions.Caching.Memory.MemoryCache(new MemoryCacheOptions()),
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
        var catArticle = ctx.CategorieArticles.FirstOrDefault();
        Assert.IsNotNull(catArticle);

        var article = new Article
        {
            //ArticleId = ctx.Articles.OrderBy(e => e.ArticleId).Last().ArticleId + 1,
            CategorieArticleId = catArticle.CategorieArticleId
        };
        
        manager.AddAsync(article).Wait();

        var article2 = ctx.Articles.FirstOrDefault(u => u.ArticleId == article.ArticleId);
        Assert.IsNotNull(article2);
    }

    [TestMethod()]
    public void DeleteAsyncTest()
    {
        var article = ctx.Articles.FirstOrDefault();
        Assert.IsNotNull(article);

        ctx.Entry(article).Collection(e => e.ListeContenuArticles).Load();
        foreach (var content in article.ListeContenuArticles)
        {
            ctx.ContenuArticles.Remove(content);
        }
        ctx.SaveChanges();

        manager.DeleteAsync(article).Wait();
        article = ctx.Articles.Find(article.ArticleId);
        Assert.IsNull(article);
    }

    [TestMethod()]
    public void GetAllAsyncTest()
    {
        var result = manager.GetAllAsync(0).Result;

        Assert.IsNotNull(result);
        Assert.IsNotNull(result.Value);
        CollectionAssert.AreEquivalent(ctx.Articles.Take(ArticleManager.PAGE_SIZE).ToList(),
            result.Value.ToList());
    }

    [TestMethod()]
    public void GetByIdAsyncTest()
    {
        var expected = ctx.Articles.FirstOrDefault();
        Assert.IsNotNull(expected);

        var result = manager.GetByIdAsync(expected.ArticleId).Result;

        Assert.IsNotNull(result);
        Assert.IsNotNull(result.Value);
        Assert.AreEqual(expected, result.Value);
    }

    [TestMethod()]
    public void GetByCategoryIdAsyncTest()
    {
        var expected = ctx.Articles.FirstOrDefault();
        Assert.IsNotNull(expected);

        var result = manager.GetByIdAsync(expected.CategorieArticleId).Result;

        Assert.IsNotNull(result);
        Assert.IsNotNull(result.Value);
        Assert.AreEqual(expected, result.Value);
    }

    [TestMethod()]
    public void GetCountAsyncTest()
    {
        var result = manager.GetCountAsync().Result;
        Assert.IsNotNull(result);
        Assert.IsNotNull(result.Value);
        Assert.AreEqual(ctx.Articles.Count(), result.Value);
    }

    [TestMethod()]
    public void UpdateAsyncTest()
    {
        var store = ctx.Articles.FirstOrDefault();
        Assert.IsNotNull(store);

        var newP = 5;
        store.CategorieArticleId = newP;

        manager.UpdateAsync(store, store).Wait();

        store = ctx.Articles.Find(store.ArticleId);
        Assert.IsNotNull(store);
        Assert.AreEqual(newP, store.CategorieArticleId);
    }
}