using System.Linq;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WsRest_UpWay.Models.DataManager;
using WsRest_UpWay.Models.EntityFramework;

namespace WsRest_UpWay.Tests.Models.DataManager;

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
        builder.UseNpgsql("Server=localhost;port=5432;Database=upway;uid=postgres;password=postgres;SearchPath=upways");

        ctx = new S215UpWayContext(builder.Options);
        manager = new MarqueManager(ctx);
    }

    [TestMethod]
    public void GetAllAsyncTest()
    {
        var result = manager.GetAllAsync().Result;

        Assert.IsNotNull(result);
        Assert.IsNotNull(result.Value);
        CollectionAssert.AreEquivalent(ctx.Marques.ToList(), result.Value.ToList());
    }

    [TestMethod]
    public void GetByIdAsyncTest()
    {
        var expected = ctx.Marques.FirstOrDefault();
        if (expected == null)
            return; // db is either empty or not working, stop the test without error to avoid false negative when db is empty

        var result = manager.GetByIdAsync(expected.MarqueId).Result;

        Assert.IsNotNull(result);
        Assert.IsNotNull(result.Value);
        Assert.AreEqual(expected, result.Value);
    }

    [TestMethod]
    public void GetByStringAsyncTest()
    {
        var expected = ctx.Marques.FirstOrDefault();
        if (expected == null)
            return; // db is either empty or not working, stop the test without error to avoid false negative when db is empty

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

        ctx.Marques.Remove(brand);
        ctx.SaveChanges();
    }

    [TestMethod]
    public void UpdateAsyncTest()
    {
        var brand = ctx.Marques.FirstOrDefault();
        if (brand == null) return; // we can't test if the db is empty

        var orig = brand.NomMarque;
        var newP = "Captain Sparkling";
        brand.NomMarque = newP;

        manager.UpdateAsync(brand, brand).Wait();

        brand = ctx.Marques.Find(brand.MarqueId);
        Assert.IsNotNull(brand);
        Assert.AreEqual(newP, brand.NomMarque);

        brand.NomMarque = orig;
        manager.UpdateAsync(brand, brand).Wait();
    }

    [TestMethod]
    public void RemoveAsyncTest()
    {
        var brand = new Marque
        {
            NomMarque = "Cheese McQueen"
        };

        brand = ctx.Marques.Add(brand).Entity;
        ctx.SaveChanges();
        Assert.IsNotNull(brand);

        manager.DeleteAsync(brand).Wait();
        brand = ctx.Marques.Find(brand.MarqueId);
        Assert.IsNull(brand);
    }
}