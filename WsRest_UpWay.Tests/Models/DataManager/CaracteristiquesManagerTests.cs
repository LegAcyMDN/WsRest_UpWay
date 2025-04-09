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
[TestSubject(typeof(CaracteristiquesManager))]
public class CaracteristiquesManagerTests
{
    private S215UpWayContext ctx;
    private CaracteristiquesManager manager;

    [TestInitialize]
    public void Initialize()
    {
        var builder = new DbContextOptionsBuilder<S215UpWayContext>();
        builder.UseSqlite("Data Source=S215UpWay.db");

        ctx = new S215UpWayContext(builder.Options);
        ctx.Database.Migrate();
        ctx.Database.ExecuteSqlRaw(File.ReadAllText("inserts.sql"));

        manager = new CaracteristiquesManager(ctx, new MemoryCache(
            new Microsoft.Extensions.Caching.Memory.MemoryCache(new MemoryCacheOptions()),
            new ConfigurationManager()));
    }

    [TestCleanup]
    public void Cleanup()
    {
        ctx.Database.EnsureDeleted();
    }

    [TestMethod()]
    public void GetCountAsyncTest()
    {
        Assert.Fail();
    }

    [TestMethod()]
    public void GetByIdAsyncTest()
    {
        var expected = ctx.Caracteristiques.FirstOrDefault();
        Assert.IsNotNull(expected);

        var result = manager.GetByIdAsync(expected.CaracteristiqueId).Result;

        Assert.IsNotNull(result);
        Assert.IsNotNull(result.Value);
        Assert.AreEqual(expected, result.Value);
    }

    [TestMethod()]
    public void GetByStringAsyncTest()
    {
        var expected = ctx.Caracteristiques.FirstOrDefault();
        Assert.IsNotNull(expected);

        var result = manager.GetByStringAsync(expected.LibelleCaracteristique).Result;

        Assert.IsNotNull(result);
        Assert.IsNotNull(result.Value);
        Assert.AreEqual(expected, result.Value);
    }

    [TestMethod()]
    public void AddAsyncTest()
    {
        var caracteristique = new Caracteristique
        {
            LibelleCaracteristique = "Vitesse",
            ImageCaracteristique = "Nothing.png"
        };

        manager.AddAsync(caracteristique).Wait();

        var caracteristique2 = ctx.Caracteristiques.First(u => u.LibelleCaracteristique == caracteristique.LibelleCaracteristique);
        Assert.IsNotNull(caracteristique2);
    }

    [TestMethod()]
    public void UpdateAsyncTest()
    {
        var caracteristique = ctx.Caracteristiques.FirstOrDefault();
        Assert.IsNotNull(caracteristique);

        var newP = "Supension";
        caracteristique.LibelleCaracteristique = newP;

        manager.UpdateAsync(caracteristique, caracteristique).Wait();

        caracteristique = ctx.Caracteristiques.Find(caracteristique.CaracteristiqueId);
        Assert.IsNotNull(caracteristique);
        Assert.AreEqual(newP, caracteristique.LibelleCaracteristique);
    }

    [TestMethod()]
    public void DeleteAsyncTest()
    {
        var caracteristique = ctx.Caracteristiques.FirstOrDefault();
        Assert.IsNotNull(caracteristique);

        // cascade so it doesn't break foreign keys when deleting
        ctx.Entry(caracteristique).Collection(e => e.ListeSousCaracteristiques).Load();

        manager.DeleteAsync(caracteristique).Wait();
        caracteristique = ctx.Caracteristiques.Find(caracteristique.CaracteristiqueId);
        Assert.IsNull(caracteristique);
    }

    [TestMethod()]
    public void GetAllAsyncTest()
    {
        var result = manager.GetAllAsync(0).Result;

        Assert.IsNotNull(result);
        Assert.IsNotNull(result.Value);
        CollectionAssert.AreEquivalent(ctx.Caracteristiques.Take(CaracteristiquesManager.PAGE_SIZE).ToList(), result.Value.ToList());
    }
}