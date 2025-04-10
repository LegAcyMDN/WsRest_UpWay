using System;
using System.IO;
using System.Linq;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WsRest_UpWay.Models.EntityFramework;
using System.Security.Cryptography;
using MemoryCache = WsRest_UpWay.Models.Cache.MemoryCache;

namespace WsRest_UpWay.Models.DataManager.Tests;

[TestClass()]
[TestSubject(typeof(PhotoVeloManager))]
public class PhotoVeloManagerTests
{
    private S215UpWayContext ctx;
    private PhotoVeloManager manager;

    [TestInitialize]
    public void Initialize()
    {
        var builder = new DbContextOptionsBuilder<S215UpWayContext>();
        builder.UseSqlite("Data Source=S215UpWay.db");

        ctx = new S215UpWayContext(builder.Options);
        ctx.Database.Migrate();
        ctx.Database.ExecuteSqlRaw(File.ReadAllText("inserts.sql"));

        manager = new PhotoVeloManager(ctx, new MemoryCache(
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
        byte[] randomBytes = new byte[256]; 
        RandomNumberGenerator.Fill(randomBytes);

        var velo = ctx.Velos.FirstOrDefault();
        Assert.IsNotNull(velo);

        var photoVelo = new PhotoVelo
        {
            VeloId = velo.VeloId,
            UrlPhotoVelo = "nothingIWantToDie.png",
            PhotoBytea = randomBytes
        };

        manager.AddAsync(photoVelo).Wait();

        var photAccessoire2 = ctx.Photovelos.FirstOrDefault(u => u.PhotoVeloId == photoVelo.PhotoVeloId);
        Assert.IsNotNull(photAccessoire2);
    }

    [TestMethod()]
    public void DeleteAsyncTest()
    {
        var photoVelo = ctx.Photovelos.FirstOrDefault();
        Assert.IsNotNull(photoVelo);

        ctx.SaveChanges();
        manager.DeleteAsync(photoVelo).Wait();
        photoVelo = ctx.Photovelos.Find(photoVelo.PhotoVeloId);
        Assert.IsNull(photoVelo);
    }

    [TestMethod()]
    public void GetAllAsyncTest()
    {
        var result = manager.GetAllAsync(0).Result;

        Assert.IsNotNull(result);
        Assert.IsNotNull(result.Value);
        CollectionAssert.AreEquivalent(ctx.Photovelos.Take(PhotoVeloManager.PAGE_SIZE).ToList(), result.Value.ToList());
    }

    [TestMethod()]
    public void GetByIdAsyncTest()
    {
        var expected = ctx.Photovelos.FirstOrDefault();
        Assert.IsNotNull(expected);

        var result = manager.GetByIdAsync(expected.PhotoVeloId).Result;

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
        Assert.AreEqual(ctx.Photovelos.Count(), result.Value);
    }

    [TestMethod()]
    public void UpdateAsyncTest()
    {
        var photoVelo = ctx.Photovelos.FirstOrDefault();
        Assert.IsNotNull(photoVelo);

        var newP = "IReallyWantToDieCestChiantLesTestsUnitairesEtMoq.png";
        photoVelo.UrlPhotoVelo = newP;

        manager.UpdateAsync(photoVelo, photoVelo).Wait();

        photoVelo = ctx.Photovelos.Find(photoVelo.PhotoVeloId);
        Assert.IsNotNull(photoVelo);
        Assert.AreEqual(newP, photoVelo.UrlPhotoVelo);
    }
}