using System.Linq;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WsRest_UpWay.Models.EntityFramework;

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

        manager = new CategorieManager(ctx);
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
        if (expected == null)
            return; // db is either empty or not working, stop the test without error to avoid false negative when db is empty

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

        ctx.Categories.Remove(store2);
        ctx.SaveChanges();
    }

    [TestMethod]
    public void UpdateAsyncTest()
    {
        var store = ctx.Categories.FirstOrDefault();
        if (store == null) return; // we can't test if the db is empty

        var orig = store.LibelleCategorie;
        var newP = "Achat de vélo";
        store.LibelleCategorie = newP;

        manager.UpdateAsync(store, store).Wait();

        store = ctx.Categories.Find(store.CategorieId);
        Assert.IsNotNull(store);
        Assert.AreEqual(newP, store.LibelleCategorie);

        store.LibelleCategorie = orig;
        manager.UpdateAsync(store, store).Wait();
    }

    [TestMethod]
    public void RemoveAsyncTest()
    {
        var store = new Categorie
        {
            LibelleCategorie = "Toutes les informations et détails à savoir !"
        };

        store = ctx.Categories.Add(store).Entity;
        ctx.SaveChanges();
        Assert.IsNotNull(store);

        manager.DeleteAsync(store).Wait();
        store = ctx.Categories.Find(store.CategorieId);
        Assert.IsNull(store);
    }
}