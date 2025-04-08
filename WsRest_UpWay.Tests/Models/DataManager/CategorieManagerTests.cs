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
[TestSubject(typeof(CategorieManager))]
public class CategorieManagerTests
{
    private S215UpWayContext ctx;
    private CategorieManager manager;

    [TestInitialize]
    public void Initialize()
    {
        var builder = new DbContextOptionsBuilder<S215UpWayContext>();
        builder.UseSqlite("Data Source=S215UpWay.db");

        ctx = new S215UpWayContext(builder.Options);
        ctx.Database.Migrate();
        ctx.Database.ExecuteSqlRaw(File.ReadAllText("inserts.sql"));

        manager = new CategorieManager(ctx,
            new MemoryCache(new Microsoft.Extensions.Caching.Memory.MemoryCache(new MemoryCacheOptions()),
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
        CollectionAssert.AreEquivalent(ctx.Categories.ToList(), result.Value.ToList());
    }

    [TestMethod]
    public void GetByIdAsyncTest()
    {
        var expected = ctx.Categories.FirstOrDefault();
        Assert.IsNotNull(expected);

        var result = manager.GetByIdAsync(expected.CategorieId).Result;

        Assert.IsNotNull(result);
        Assert.IsNotNull(result.Value);
        Assert.AreEqual(expected, result.Value);
    }

    [TestMethod]
    public void AddAsyncTest()
    {
        var store = new Categorie
        {
            LibelleCategorie = "Toutes les informations et détails à savoir !"
        };

        manager.AddAsync(store).Wait();

        var store2 = ctx.Categories.First(u => u.LibelleCategorie == store.LibelleCategorie);
        Assert.IsNotNull(store2);
    }

    [TestMethod]
    public void UpdateAsyncTest()
    {
        var store = ctx.Categories.FirstOrDefault();
        Assert.IsNotNull(store);

        var newP = "Achat de vélo";
        store.LibelleCategorie = newP;

        manager.UpdateAsync(store, store).Wait();

        store = ctx.Categories.Find(store.CategorieId);
        Assert.IsNotNull(store);
        Assert.AreEqual(newP, store.LibelleCategorie);
    }

    [TestMethod]
    public void RemoveAsyncTest()
    {
        var category = new Categorie
        {
            LibelleCategorie = "Truc a delete"
        };
        category = ctx.Categories.Add(category).Entity;
        ctx.SaveChanges();
        Assert.IsNotNull(category);

        manager.DeleteAsync(category).Wait();
        category = ctx.Categories.Find(category.CategorieId);
        Assert.IsNull(category);
    }
}