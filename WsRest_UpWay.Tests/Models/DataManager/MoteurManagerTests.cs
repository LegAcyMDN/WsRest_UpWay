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
[TestSubject(typeof(MoteurManager))]
public class MoteurManagerTests
{
    private S215UpWayContext ctx;
    private MoteurManager manager;

    [TestInitialize]
    public void Initialize()
    {
        var builder = new DbContextOptionsBuilder<S215UpWayContext>();
        builder.UseSqlite("Data Source=S215UpWay.db");

        ctx = new S215UpWayContext(builder.Options);
        ctx.Database.Migrate();
        ctx.Database.ExecuteSqlRaw(File.ReadAllText("inserts.sql"));

        manager = new MoteurManager(ctx, 
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
        var marque = ctx.Moteurs.FirstOrDefault();
        Assert.IsNotNull(marque);

        var moteur = new Moteur
        {
            MarqueId = marque.MarqueId,
            ModeleMoteur = "Moteur avant",
            CoupleMoteur = "150",
            VitesseMaximal = "50"
        };

        manager.AddAsync(moteur).Wait();

        var moteur2 = ctx.Moteurs.FirstOrDefault(u => u.MoteurId == moteur.MoteurId);
        Assert.IsNotNull(moteur2);
    }

    [TestMethod()]
    public void DeleteAsyncTest()
    {
        var moteur = ctx.Moteurs.FirstOrDefault();
        Assert.IsNotNull(moteur);

        manager.DeleteAsync(moteur).Wait();
        moteur = ctx.Moteurs.Find(moteur.MoteurId);
        Assert.IsNull(moteur);
    }

    [TestMethod()]
    public void GetAllAsyncTest()
    {
        var result = manager.GetAllAsync(0).Result;

        Assert.IsNotNull(result);
        Assert.IsNotNull(result.Value);
        CollectionAssert.AreEquivalent(ctx.Moteurs.ToList(), result.Value.ToList());
    }

    [TestMethod()]
    public void GetByIdAsyncTest()
    {
        var expected = ctx.Moteurs.FirstOrDefault();
        Assert.IsNotNull(expected);

        var result = manager.GetByIdAsync(expected.MoteurId).Result;

        Assert.IsNotNull(result);
        Assert.IsNotNull(result.Value);
        Assert.AreEqual(expected, result.Value);
    }

    [TestMethod()]
    public void UpdateAsyncTest()
    {
        var store = ctx.Moteurs.FirstOrDefault();
        Assert.IsNotNull(store);

        var newP = "Moteur à rotor";
        store.ModeleMoteur = newP;

        manager.UpdateAsync(store, store).Wait();

        store = ctx.Moteurs.Find(store.MoteurId);
        Assert.IsNotNull(store);
        Assert.AreEqual(newP, store.ModeleMoteur);
    }
}