using System.IO;
using System.Linq;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WsRest_UpWay.Models.EntityFramework;

namespace WsRest_UpWay.Models.DataManager.Tests;

[TestClass]
[TestSubject(typeof(MarqueManager))]
public class MarqueManagerTest
{
    private S215UpWayContext ctx;
    private MarqueManager manager;

    [TestInitialize]
    public void Initialize()
    {
        var builder = new DbContextOptionsBuilder<S215UpWayContext>();
        builder.UseSqlite("Data Source=S215UpWay.db");

        ctx = new S215UpWayContext(builder.Options);
        ctx.Database.Migrate();
        ctx.Database.ExecuteSqlRaw(File.ReadAllText("inserts.sql"));

        manager = new MarqueManager(ctx);
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
        CollectionAssert.AreEquivalent(ctx.Marques.Take(MarqueManager.PAGE_SIZE).ToList(), result.Value.ToList());
    }

    [TestMethod]
    public void GetAllAsyncPage1Test()
    {
        var result = manager.GetAllAsync(1).Result;

        Assert.IsNotNull(result);
        Assert.IsNotNull(result.Value);
        CollectionAssert.AreEquivalent(
            ctx.Marques.Skip(MarqueManager.PAGE_SIZE * 1).Take(MarqueManager.PAGE_SIZE).ToList(),
            result.Value.ToList());
    }

    [TestMethod]
    public void GetByIdAsyncTest()
    {
        var expected = ctx.Marques.FirstOrDefault();
        Assert.IsNotNull(expected);

        var result = manager.GetByIdAsync(expected.MarqueId).Result;

        Assert.IsNotNull(result);
        Assert.IsNotNull(result.Value);
        Assert.AreEqual(expected, result.Value);
    }

    [TestMethod]
    public void GetByStringAsyncTest()
    {
        var expected = ctx.Marques.FirstOrDefault();
        Assert.IsNotNull(expected);

        var result = manager.GetByStringAsync(expected.NomMarque).Result;

        Assert.IsNotNull(result);
        Assert.IsNotNull(result.Value);
        Assert.AreEqual(expected, result.Value);
    }

    [TestMethod]
    public void AddAsyncTest()
    {
        var brand = new Marque
        {
            NomMarque = "Cheese McQueen"
        };

        manager.AddAsync(brand).Wait();

        brand = ctx.Marques.FirstOrDefault(b => b.NomMarque == brand.NomMarque);
        Assert.IsNotNull(brand);
    }

    [TestMethod]
    public void UpdateAsyncTest()
    {
        var brand = ctx.Marques.FirstOrDefault();
        Assert.IsNotNull(brand);

        var newP = "Captain Sparkling";
        brand.NomMarque = newP;

        manager.UpdateAsync(brand, brand).Wait();

        brand = ctx.Marques.Find(brand.MarqueId);
        Assert.IsNotNull(brand);
        Assert.AreEqual(newP, brand.NomMarque);
    }

    [TestMethod]
    public void RemoveAsyncTest()
    {
        var brand = ctx.Marques.FirstOrDefault();
        Assert.IsNotNull(brand);

        manager.DeleteAsync(brand).Wait();
        brand = ctx.Marques.Find(brand.MarqueId);
        Assert.IsNull(brand);
    }
}