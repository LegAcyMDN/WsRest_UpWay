using System;
using System.Linq;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WsRest_UpWay.Models.DataManager;
using WsRest_UpWay.Models.EntityFramework;

namespace WsRest_UpWay.Tests.Models.DataManager;

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
        builder.UseNpgsql(Environment.GetEnvironmentVariable("DB_CONNECTION_URL"));

        ctx = new S215UpWayContext(builder.Options);
        manager = new MagasinManager(ctx);
    }

    [TestMethod]
    public void GetAllAsyncTest()
    {
        var result = manager.GetAllAsync().Result;

        Assert.IsNotNull(result);
        Assert.IsNotNull(result.Value);
        CollectionAssert.AreEquivalent(ctx.Magasins.ToList(), result.Value.ToList());
    }

    [TestMethod]
    public void GetByIdAsyncTest()
    {
        var expected = ctx.Magasins.FirstOrDefault();
        if (expected == null)
            return; // db is either empty or not working, stop the test without error to avoid false negative when db is empty

        var result = manager.GetByIdAsync(expected.MagasinId).Result;

        Assert.IsNotNull(result);
        Assert.IsNotNull(result.Value);
        Assert.AreEqual(expected, result.Value);
    }

    [TestMethod]
    public void GetByStringAsyncTest()
    {
        var expected = ctx.Magasins.FirstOrDefault();
        if (expected == null)
            return; // db is either empty or not working, stop the test without error to avoid false negative when db is empty

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

        ctx.Magasins.Remove(store2);
        ctx.SaveChanges();
    }

    [TestMethod]
    public void UpdateAsyncTest()
    {
        var store = ctx.Magasins.FirstOrDefault();
        if (store == null) return; // we can't test if the db is empty

        var orig = store.NomMagasin;
        var newP = "Marc et Brique";
        store.NomMagasin = newP;

        manager.UpdateAsync(store, store).Wait();

        store = ctx.Magasins.Find(store.MagasinId);
        Assert.IsNotNull(store);
        Assert.AreEqual(newP, store.NomMagasin);

        store.NomMagasin = orig;
        manager.UpdateAsync(store, store).Wait();
    }

    [TestMethod]
    public void RemoveAsyncTest()
    {
        var store = new Magasin
        {
            NomMagasin = "Brick et Marc",
            HoraireMagasin = "8h-19h",
            RueMagasin = "37 rue du bois",
            VilleMagasin = "Montaisse",
            CPMagasin = "00000"
        };

        store = ctx.Magasins.Add(store).Entity;
        ctx.SaveChanges();
        Assert.IsNotNull(store);

        manager.DeleteAsync(store).Wait();
        store = ctx.Magasins.Find(store.MagasinId);
        Assert.IsNull(store);
    }
}