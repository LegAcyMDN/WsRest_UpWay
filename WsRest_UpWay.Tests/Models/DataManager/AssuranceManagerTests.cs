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
[TestSubject(typeof(AssuranceManager))]
public class AssuranceManagerTests
{
    private S215UpWayContext ctx;
    private AssuranceManager manager;

    [TestInitialize]
    public void Initialize()
    {
        var builder = new DbContextOptionsBuilder<S215UpWayContext>();
        builder.UseSqlite("Data Source=S215UpWay.db");

        ctx = new S215UpWayContext(builder.Options);
        ctx.Database.Migrate();
        ctx.Database.ExecuteSqlRaw(File.ReadAllText("inserts.sql"));

        manager = new AssuranceManager(ctx);
    }

    [TestCleanup]
    public void Cleanup()
    {
        ctx.Database.EnsureDeleted();
    }

    [TestMethod()]
    public void AddAsyncTest()
    {
        var assurance = new Assurance
        {
            TitreAssurance = "Vol",
            DescriptionAssurance = "Assuré contre les vols",
            PrixAssurance = 9.99M
        };

        manager.AddAsync(assurance).Wait();

        var assurance2 = ctx.Assurances.First(u => u.TitreAssurance == assurance.TitreAssurance);
        Assert.IsNotNull(assurance2);
    }

    [TestMethod()]
    public void DeleteAsyncTest()
    {
        var assurance = ctx.Assurances.FirstOrDefault();
        Assert.IsNotNull(assurance);

        ctx.Entry(assurance).Collection(x => x.ListeLignePaniers).Load();

        foreach (var ligne in assurance.ListeLignePaniers)
        {
            ctx.Lignepaniers.Remove(ligne);
        }

        ctx.SaveChanges();
        manager.DeleteAsync(assurance).Wait();
        assurance = ctx.Assurances.Find(assurance.AssuranceId);
        Assert.IsNull(assurance);
    }

    [TestMethod()]
    public void GetAllAsyncTest()
    {
        var result = manager.GetAllAsync(0).Result;

        Assert.IsNotNull(result);
        Assert.IsNotNull(result.Value);
        CollectionAssert.AreEquivalent(ctx.Assurances.Take(AssuranceManager.PAGE_SIZE).ToList(), result.Value.ToList());
    }

    [TestMethod()]
    public void GetByIdAsyncTest()
    {
        var expected = ctx.Assurances.FirstOrDefault();
        Assert.IsNotNull(expected);

        var result = manager.GetByIdAsync(expected.AssuranceId).Result;

        Assert.IsNotNull(result);
        Assert.IsNotNull(result.Value);
        Assert.AreEqual(expected, result.Value);
    }

    [TestMethod()]
    public void GetByStringAsyncTest()
    {
        var expected = ctx.Assurances.FirstOrDefault();
        Assert.IsNotNull(expected);

        var result = manager.GetByStringAsync(expected.TitreAssurance).Result;

        Assert.IsNotNull(result);
        Assert.IsNotNull(result.Value);
        Assert.AreEqual(expected, result.Value);
    }

    [TestMethod()]
    public void UpdateAsyncTest()
    {
        var store = ctx.Assurances.FirstOrDefault();
        Assert.IsNotNull(store);

        var newP = "Vol et casse";
        store.TitreAssurance = newP;

        manager.UpdateAsync(store, store).Wait();

        store = ctx.Assurances.Find(store.AssuranceId);
        Assert.IsNotNull(store);
        Assert.AreEqual(newP, store.TitreAssurance);
    }
}