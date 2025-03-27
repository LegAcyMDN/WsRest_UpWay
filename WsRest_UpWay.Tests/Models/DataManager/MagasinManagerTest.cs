using System.IO;
using System.Linq;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WsRest_UpWay.Models.EntityFramework;

namespace WsRest_UpWay.Models.DataManager.Tests;

[TestClass]
[TestSubject(typeof(MagasinManager))]
public class MagasinManagerTest
{
    private S215UpWayContext ctx;
    private MagasinManager manager;

    [TestInitialize]
    public void Initialize()
    {
        var builder = new DbContextOptionsBuilder<S215UpWayContext>();
        builder.UseSqlite("Data Source=S215UpWay.db");

        ctx = new S215UpWayContext(builder.Options);
        ctx.Database.Migrate();
        ctx.Database.ExecuteSqlRaw(File.ReadAllText("inserts.sql"));

        manager = new MagasinManager(ctx);
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
        CollectionAssert.AreEquivalent(ctx.Magasins.Take(MagasinManager.PAGE_SIZE).ToList(), result.Value.ToList());
    }

    [TestMethod]
    public void GetAllAsyncPage1Test()
    {
        var result = manager.GetAllAsync(1).Result;

        Assert.IsNotNull(result);
        Assert.IsNotNull(result.Value);
        CollectionAssert.AreEquivalent(
            ctx.Magasins.Skip(MagasinManager.PAGE_SIZE * 1).Take(MagasinManager.PAGE_SIZE).ToList(),
            result.Value.ToList());
    }

    [TestMethod]
    public void GetByIdAsyncTest()
    {
        var expected = ctx.Magasins.FirstOrDefault();
        Assert.IsNotNull(expected);

        var result = manager.GetByIdAsync(expected.MagasinId).Result;

        Assert.IsNotNull(result);
        Assert.IsNotNull(result.Value);
        Assert.AreEqual(expected, result.Value);
    }

    [TestMethod]
    public void GetByStringAsyncTest()
    {
        var expected = ctx.Magasins.FirstOrDefault();
        Assert.IsNotNull(expected);

        var result = manager.GetByStringAsync(expected.NomMagasin).Result;

        Assert.IsNotNull(result);
        Assert.IsNotNull(result.Value);
        Assert.AreEqual(expected, result.Value);
    }

    [TestMethod]
    public void AddAsyncTest()
    {
        var store = new Magasin
        {
            NomMagasin = "Brick et Marc",
            HoraireMagasin = "8h-19h",
            RueMagasin = "37 rue du bois",
            VilleMagasin = "Montaisse",
            CPMagasin = "00000"
        };

        manager.AddAsync(store).Wait();

        var store2 = ctx.Magasins.First(u => u.NomMagasin == store.NomMagasin);
        Assert.IsNotNull(store2);
    }

    [TestMethod]
    public void UpdateAsyncTest()
    {
        var store = ctx.Magasins.FirstOrDefault();
        Assert.IsNotNull(store);

        var newP = "Marc et Brique";
        store.NomMagasin = newP;

        manager.UpdateAsync(store, store).Wait();

        store = ctx.Magasins.Find(store.MagasinId);
        Assert.IsNotNull(store);
        Assert.AreEqual(newP, store.NomMagasin);
    }

    [TestMethod]
    public void RemoveAsyncTest()
    {
        var store = ctx.Magasins.FirstOrDefault();
        Assert.IsNotNull(store);

        manager.DeleteAsync(store).Wait();
        store = ctx.Magasins.Find(store.MagasinId);
        Assert.IsNull(store);
    }
}