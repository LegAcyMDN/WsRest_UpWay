using System.IO;
using System.Linq;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WsRest_UpWay.Models.DataManager;
using WsRest_UpWay.Models.EntityFramework;
using MemoryCache = WsRest_UpWay.Models.Cache.MemoryCache;

namespace WsRest_UpWay.Tests.Models.DataManager;

[TestClass]
[TestSubject(typeof(VeloManager))]
public class VeloManagerTest
{

    private S215UpWayContext ctx;
    private VeloManager manager;

    [TestInitialize]
    public void Initialize()
    {
        var builder = new DbContextOptionsBuilder<S215UpWayContext>();
        builder.UseSqlite("Data Source=S215UpWay.db");

        ctx = new S215UpWayContext(builder.Options);
        ctx.Database.Migrate();
        ctx.Database.ExecuteSqlRaw(File.ReadAllText("inserts.sql"));

        manager = new VeloManager (ctx, new MemoryCache(
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
        var result = manager.GetCountAsync().Result;
        Assert.IsNotNull(result);
        Assert.IsNotNull(result.Value);
        Assert.AreEqual(ctx.Velos.Count(), result.Value);
    }

    [TestMethod()]
    public void GetByIdAsyncTest()
    {
        var expected = ctx.Velos.FirstOrDefault();
        Assert.IsNotNull(expected);
        
        var result = manager.GetByIdAsync(expected.VeloId).Result;
        Assert.IsNotNull(result);
        Assert.IsNotNull(result.Value);
        Assert.AreEqual(expected, result.Value);
    }

    [TestMethod()]
    public void GetPhotosByIdAsyncTest()
    {
        var expected = ctx.Velos.FirstOrDefault();
        Assert.IsNotNull(expected);

        var result = manager.GetByIdAsync(expected.VeloId).Result;

        Assert.IsNotNull(result);
        Assert.IsNotNull(result.Value);
        Assert.AreEqual(expected, result.Value);
    }

    [TestMethod()]
    public void GetByStringAsyncTest()
    {
        var expected = ctx.Velos.FirstOrDefault();
        Assert.IsNotNull(expected);

        var result = manager.GetByStringAsync(expected.NomVelo).Result;

        Assert.IsNotNull(result);
        Assert.IsNotNull(result.Value);
        Assert.AreEqual(expected, result.Value);
    }

    [TestMethod()]
    public void AddAsyncTest()
    {
        var marque = ctx.Marques.FirstOrDefault();
        Assert.IsNotNull(marque);

        var categorie = ctx.Categories.FirstOrDefault();
        Assert.IsNotNull(categorie);

        var velo = new Velo()
        {
            MarqueId = marque.MarqueId,
            CategorieId = categorie.CategorieId,
            NomVelo = "Le velo atomique",
            PrixNeuf = 199.99m,
            PrixRemise = 199.99m,
            DescriptifVelo = "Le dernier velo que vous n'aurez a esseyer pour le restant de vos jours"
        };

        manager.AddAsync(velo).Wait();

        var velo2 = ctx.Velos.FirstOrDefault(u => u.NomVelo == velo.NomVelo);
        Assert.IsNotNull(velo2);
    }

    [TestMethod()]
    public void UpdateAsyncTest()
    {
        var store = ctx.Velos.FirstOrDefault();
        Assert.IsNotNull(store);

        var newP = "Le velo trompette";
        store.NomVelo = newP;

        manager.UpdateAsync(store, store).Wait();

        store = ctx.Velos.Find(store.VeloId);
        Assert.IsNotNull(store);
        Assert.AreEqual(newP, store.NomVelo);
    }

    [TestMethod()]
    public void DeleteAsyncTest()
    {
        var velo = ctx.Velos.FirstOrDefault();
        Assert.IsNotNull(velo);

        ctx.Entry(velo).Collection(e => e.ListePhotoVelos).Load();
        ctx.Entry(velo).Collection(e => e.ListeAlerteVelos).Load();
        ctx.Entry(velo).Collection(e => e.ListeEstRealises).Load();
        ctx.Entry(velo).Collection(e => e.ListeLignePaniers).Load();
        ctx.Entry(velo).Collection(e => e.ListeMentionVelos).Load();
        ctx.Entry(velo).Collection(e => e.ListeTestVelos).Load();
        ctx.Entry(velo).Collection(e => e.ListeUtilites).Load();
        ctx.Entry(velo).Collection(e => e.ListeAccessoires).Load();
        ctx.Entry(velo).Collection(e => e.ListeCaracteristiques).Load();
        ctx.Entry(velo).Collection(e => e.ListeMagasins).Load();

        foreach(var a in velo.ListePhotoVelos)
        {
            ctx.Remove(a);
        }
        
        foreach(var a in velo.ListeEstRealises)
        {
            ctx.Remove(a);
        }
        
        foreach(var a in velo.ListeAlerteVelos)
        {
            ctx.Remove(a);
        }
        
        foreach(var a in velo.ListeMentionVelos)
        {
            ctx.Remove(a);
        }
        
        foreach(var a in velo.ListeLignePaniers)
        {
            ctx.Remove(a);
        }
        
        foreach(var a in velo.ListeTestVelos)
        {
            ctx.Remove(a);
        }
        
        foreach(var a in velo.ListeUtilites)
        {
            ctx.Remove(a);
        }
        
        foreach(var a in velo.ListeAccessoires)
        {
            ctx.Remove(a);
        }

        foreach(var a in velo.ListeCaracteristiques)
        {
            ctx.Remove(a);
        }
        
        foreach(var a in velo.ListeMagasins)
        {
            ctx.Remove(a);
        }
        
        ctx.SaveChanges();
        manager.DeleteAsync(velo).Wait();
        velo = ctx.Velos.Find(velo.VeloId);
        Assert.IsNull(velo);
    }

    [TestMethod()]
    public void GetAllAsyncTest()
    {
        var result = manager.GetAllAsync(0).Result;

        Assert.IsNotNull(result);
        Assert.IsNotNull(result.Value);
        CollectionAssert.AreEquivalent(ctx.Velos.Take(AccessoireManager.PAGE_SIZE).ToList(),
            result.Value.ToList());
    }
}