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
[TestSubject(typeof(CaracteristiqueVeloManager))]
public class CaracteristiqueVeloManagerTests
{
    private S215UpWayContext ctx;
    private CaracteristiqueVeloManager manager;

    [TestInitialize]
    public void Initialize()
    {
        var builder = new DbContextOptionsBuilder<S215UpWayContext>();
        builder.UseSqlite("Data Source=S215UpWay.db");

        ctx = new S215UpWayContext(builder.Options);
        ctx.Database.Migrate();
        ctx.Database.ExecuteSqlRaw(File.ReadAllText("inserts.sql"));

        manager = new CaracteristiqueVeloManager(ctx, new MemoryCache(
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
        var caractVelo = new CaracteristiqueVelo
        {
            Poids = 100,
            TubeSelle = 2,
            TypeSuspension = "Pneumatique",
            Couleur = "Orange",
            TypeCargo = "Non",
            EtatBatterie = "Neuve",
            NombreCycle = 3,
            Materiau = "Aluminium",
            Fourche = "Courbe",
            Debattement = 9,
            Amortisseur = "Normal",
            DebattementAmortisseur = 2,
            ModelTransmission = "Chaîne",
            Freins = "Disques",
            Pneus = "Normal",
            SelleTelescopique = true
        };

        manager.AddAsync(caractVelo).Wait();

        var caractVelo2 = ctx.Caracteristiques.First(u => u.CaracteristiqueId == caractVelo.CaracteristiqueVeloId);
        Assert.IsNotNull(caractVelo2);
    }

    [TestMethod()]
    public void DeleteAsyncTest()
    {
        var caractVelo = ctx.Caracteristiquevelos.FirstOrDefault();
        Assert.IsNotNull(caractVelo);

        // cascade so it doesn't break foreign keys when deleting
        ctx.Entry(caractVelo).Collection(e => e.ListeVeloModifiers).Load();

        manager.DeleteAsync(caractVelo).Wait();
        caractVelo = ctx.Caracteristiquevelos.Find(caractVelo.CaracteristiqueVeloId);
        Assert.IsNull(caractVelo);
    }

    [TestMethod()]
    public void GetAllAsyncTest()
    {
        var result = manager.GetAllAsync(0).Result;

        Assert.IsNotNull(result);
        Assert.IsNotNull(result.Value);
        CollectionAssert.AreEquivalent(ctx.Caracteristiquevelos.Take(CaracteristiqueVeloManager.PAGE_SIZE).ToList(),
            result.Value.ToList());
    }

    [TestMethod()]
    public void GetByIdAsyncTest()
    {
        var expected = ctx.Caracteristiquevelos.FirstOrDefault();
        Assert.IsNotNull(expected);

        var result = manager.GetByIdAsync(expected.CaracteristiqueVeloId).Result;

        Assert.IsNotNull(result);
        Assert.IsNotNull(result.Value);
        Assert.AreEqual(expected, result.Value);
    }

    [TestMethod()]
    public void GetCountAsyncTest()
    {
        Assert.Fail();
    }

    [TestMethod()]
    public void UpdateAsyncTest()
    {
        var store = ctx.Caracteristiquevelos.FirstOrDefault();
        Assert.IsNotNull(store);

        var newP = "Paladium";
        store.Materiau = newP;

        manager.UpdateAsync(store, store).Wait();

        store = ctx.Caracteristiquevelos.Find(store.CaracteristiqueVeloId);
        Assert.IsNotNull(store);
        Assert.AreEqual(newP, store.Materiau);
    }
}