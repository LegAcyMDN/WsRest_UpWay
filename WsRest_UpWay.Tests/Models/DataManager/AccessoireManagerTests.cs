using System.IO;
using System.Linq;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mono.TextTemplating;
using WsRest_UpWay.Models.EntityFramework;
using static System.Runtime.InteropServices.JavaScript.JSType;
using MemoryCache = WsRest_UpWay.Models.Cache.MemoryCache;

namespace WsRest_UpWay.Models.DataManager.Tests;

[TestClass()]
[TestSubject(typeof(AccessoireManager))]
public class AccessoireManagerTests
{
    private S215UpWayContext ctx;
    private AccessoireManager manager;

    [TestInitialize]
    public void Initialize()
    {
        var builder = new DbContextOptionsBuilder<S215UpWayContext>();
        builder.UseSqlite("Data Source=S215UpWay.db");

        ctx = new S215UpWayContext(builder.Options);
        ctx.Database.Migrate();
        ctx.Database.ExecuteSqlRaw(File.ReadAllText("inserts.sql"));

        manager = new AccessoireManager (ctx, new MemoryCache(
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
        Assert.AreEqual(ctx.Accessoires.Count(), result.Value);
    }

    [TestMethod()]
    public void GetByIdAsyncTest()
    {
        Assert.Fail();
    }

    [TestMethod()]
    public void GetPhotosByIdAsyncTest()
    {
        var expected = ctx.Accessoires.FirstOrDefault();
        Assert.IsNotNull(expected);

        var result = manager.GetByIdAsync(expected.AccessoireId).Result;

        Assert.IsNotNull(result);
        Assert.IsNotNull(result.Value);
        Assert.AreEqual(expected, result.Value);
    }

    [TestMethod()]
    public void GetByStringAsyncTest()
    {
        var expected = ctx.Accessoires.FirstOrDefault();
        Assert.IsNotNull(expected);

        var result = manager.GetByStringAsync(expected.NomAccessoire).Result;

        Assert.IsNotNull(result);
        Assert.IsNotNull(result.Value);
        Assert.AreEqual(expected, result.Value);
    }

    [TestMethod()]
    public void GetByCategoryPrixAsyncTest()
    {
        // Arrange
        var expected = ctx.Accessoires.FirstOrDefault();
        Assert.IsNotNull(expected);

        int? categoryId = expected.CategorieId;
        int minPrix = (int)expected.PrixAccessoire - 10;
        int maxPrix = (int)expected.PrixAccessoire + 10;
        int page = 0;

        var expectedList = ctx.Accessoires
            .Where(a => a.CategorieId == categoryId && a.PrixAccessoire > minPrix && a.PrixAccessoire < maxPrix)
            .Skip(page * AccessoireManager.PAGE_SIZE)
            .Take(AccessoireManager.PAGE_SIZE)
            .ToList();

        // Act
        var result = manager.GetByCategoryPrixAsync(categoryId, minPrix, maxPrix, page).Result;

        // Assert
        Assert.IsNotNull(result);
        Assert.IsNotNull(result.Value);
        CollectionAssert.AreEquivalent(expectedList, result.Value.ToList());
    }

    [TestMethod()]
    public void AddAsyncTest()
    {
        var marque = ctx.Marques.FirstOrDefault();
        Assert.IsNotNull(marque);

        var categorie = ctx.Categories.FirstOrDefault();
        Assert.IsNotNull(categorie);

        var accessoire = new Accessoire
        {
            MarqueId = marque.MarqueId,
            CategorieId = categorie.CategorieId,
            NomAccessoire = "Casque Test",
            PrixAccessoire = 199.99m,
            DescriptionAccessoire = "Un accessoire de test"
        };

        manager.AddAsync(accessoire).Wait();

        var accessoire2 = ctx.Accessoires.FirstOrDefault(u => u.AccessoireId == accessoire.AccessoireId);
        Assert.IsNotNull(accessoire2);
    }

    [TestMethod()]
    public void UpdateAsyncTest()
    {
        var store = ctx.Accessoires.FirstOrDefault();
        Assert.IsNotNull(store);

        var newP = "Casque Bunker";
        store.NomAccessoire = newP;

        manager.UpdateAsync(store, store).Wait();

        store = ctx.Accessoires.Find(store.AccessoireId);
        Assert.IsNotNull(store);
        Assert.AreEqual(newP, store.NomAccessoire);
    }

    [TestMethod()]
    public void DeleteAsyncTest()
    {
        var accessoire = ctx.Accessoires.FirstOrDefault();
        Assert.IsNotNull(accessoire);

        // cascade so it doesn't break foreign keys when deleting
        //ctx.Entry(accessoire).Collection(e => e.ListeAjoutAccessoires).Load();
        //ctx.Entry(accessoire).Collection(e => e.ListePhotoAccessoires).Load();
        ctx.Entry(accessoire).Collection(e => e.ListeVelos).Load();

        /*foreach (var velo in accessoire.ListeAjoutAccessoires)
        {
            ctx.Entry(velo).State = EntityState.Modified;
            velo.AccessoireId = null;
        }

        foreach (var velo in accessoire.ListePhotoAccessoires)
        {
            ctx.Entry(velo).State = EntityState.Modified;
            velo.AccessoireId = null;
        }*/

        foreach (var velo in accessoire.ListeVelos)
        {
            ctx.Entry(velo).State = EntityState.Modified;
            velo.CaracteristiqueVeloId = null;
        }

        ctx.SaveChanges();
        manager.DeleteAsync(accessoire).Wait();
        accessoire = ctx.Accessoires.Find(accessoire.AccessoireId);
        Assert.IsNull(accessoire);
    }

    [TestMethod()]
    public void GetAllAsyncTest()
    {
        var result = manager.GetAllAsync(0).Result;

        Assert.IsNotNull(result);
        Assert.IsNotNull(result.Value);
        CollectionAssert.AreEquivalent(ctx.Accessoires.Take(AccessoireManager.PAGE_SIZE).ToList(),
            result.Value.ToList());
    }
}