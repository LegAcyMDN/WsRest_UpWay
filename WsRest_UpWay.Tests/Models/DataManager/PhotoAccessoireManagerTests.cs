using System;
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
[TestSubject(typeof(PhotoAccessoireManager))]
public class PhotoAccessoireManagerTests
{
    private S215UpWayContext ctx;
    private PhotoAccessoireManager manager;

    [TestInitialize]
    public void Initialize()
    {
        var builder = new DbContextOptionsBuilder<S215UpWayContext>();
        builder.UseSqlite("Data Source=S215UpWay.db");

        ctx = new S215UpWayContext(builder.Options);
        ctx.Database.Migrate();
        ctx.Database.ExecuteSqlRaw(File.ReadAllText("inserts.sql"));

        manager = new PhotoAccessoireManager(ctx, new MemoryCache(
            new Microsoft.Extensions.Caching.Memory.MemoryCache(new MemoryCacheOptions()),
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
        var accessoire = ctx.Accessoires.FirstOrDefault();
        Assert.IsNotNull(accessoire);

        var photoAccessoire = new PhotoAccessoire
        {
            AccessoireId = accessoire.AccessoireId,
            UrlPhotoAccessoire = "nothingIWantToDie.png"
        };

        manager.AddAsync(photoAccessoire).Wait();

        var photAccessoire2 = ctx.Photoaccessoires.FirstOrDefault(u => u.PhotoAcessoireId == photoAccessoire.PhotoAcessoireId);
        Assert.IsNotNull(photAccessoire2);
    }

    [TestMethod()]
    public void DeleteAsyncTest()
    {
        var photoAccessoire = ctx.Photoaccessoires.FirstOrDefault();
        Assert.IsNotNull(photoAccessoire);

        ctx.SaveChanges();
        manager.DeleteAsync(photoAccessoire).Wait();
        photoAccessoire = ctx.Photoaccessoires.Find(photoAccessoire.PhotoAcessoireId);
        Assert.IsNull(photoAccessoire);
    }

    [TestMethod()]
    public void GetAllAsyncTest()
    {
        var result = manager.GetAllAsync(0).Result;

        Assert.IsNotNull(result);
        Assert.IsNotNull(result.Value);
        CollectionAssert.AreEquivalent(ctx.Photoaccessoires.Take(PhotoAccessoireManager.PAGE_SIZE).ToList(), result.Value.ToList());
    }

    [TestMethod()]
    public void GetByIdAsyncTest()
    {
        var expected = ctx.Photoaccessoires.FirstOrDefault();
        Assert.IsNotNull(expected);

        var result = manager.GetByIdAsync(expected.PhotoAcessoireId).Result;

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
        Assert.AreEqual(ctx.Photoaccessoires.Count(), result.Value);
    }

    [TestMethod()]
    public void UpdateAsyncTest()
    {
        var photAccessoire = ctx.Photoaccessoires.FirstOrDefault();
        Assert.IsNotNull(photAccessoire);

        var newP = "IReallyWantToDieCestChiantLesTestsUnitairesEtMoq.png";
        photAccessoire.UrlPhotoAccessoire = newP;

        manager.UpdateAsync(photAccessoire, photAccessoire).Wait();

        photAccessoire = ctx.Photoaccessoires.Find(photAccessoire.PhotoAcessoireId);
        Assert.IsNotNull(photAccessoire);
        Assert.AreEqual(newP, photAccessoire.UrlPhotoAccessoire);
    }
}