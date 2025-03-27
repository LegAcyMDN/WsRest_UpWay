using System.IO;
using System.Linq;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WsRest_UpWay.Models.EntityFramework;

namespace WsRest_UpWay.Models.DataManager.Tests;

[TestClass]
[TestSubject(typeof(InformationManager))]
public class InformationManagerTests
{
    private S215UpWayContext ctx;
    private InformationManager manager;

    [TestInitialize]
    public void Initialize()
    {
        var builder = new DbContextOptionsBuilder<S215UpWayContext>();
        builder.UseSqlite("Data Source=S215UpWay.db");

        ctx = new S215UpWayContext(builder.Options);
        ctx.Database.Migrate();
        ctx.Database.ExecuteSqlRaw(File.ReadAllText("inserts.sql"));

        manager = new InformationManager(ctx);
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
        CollectionAssert.AreEquivalent(ctx.Informations.Take(InformationManager.PAGE_SIZE).ToList(),
            result.Value.ToList());
    }

    [TestMethod]
    public void GetAllAsyncPage1Test()
    {
        var result = manager.GetAllAsync(1).Result;

        Assert.IsNotNull(result);
        Assert.IsNotNull(result.Value);
        CollectionAssert.AreEquivalent(
            ctx.Informations.Skip(InformationManager.PAGE_SIZE * 1).Take(InformationManager.PAGE_SIZE).ToList(),
            result.Value.ToList());
    }

    [TestMethod]
    public void GetByIdAsyncTest()
    {
        var expected = ctx.Informations.FirstOrDefault();
        Assert.IsNotNull(expected);

        var result = manager.GetByIdAsync(expected.InformationId).Result;

        Assert.IsNotNull(result);
        Assert.IsNotNull(result.Value);
        Assert.AreEqual(expected, result.Value);
    }

    [TestMethod]
    public void AddAsyncTest()
    {
        var reduction = ctx.Codereductions.FirstOrDefault();
        Assert.IsNotNull(reduction);

        var address_exp = ctx.Adresseexpeditions.FirstOrDefault();
        Assert.IsNotNull(address_exp);

        var order = ctx.Paniers.FirstOrDefault();
        Assert.IsNotNull(order);

        var store = new Information
        {
            InformationId = ctx.Informations.OrderBy(e => e.InformationId).Last().InformationId + 1,
            ReductionId = reduction.ReductionId,
            AdresseExpeId = address_exp.AdresseExpeId,
            PanierId = order.PanierId,
            ContactInformations = "Carte Bancaire",
            OffreEmail = true,
            ModeLivraison = "Expédition",
            InformationPays = "France",
            InformationRue = "16 rue de l'Arc en Ciel"
        };

        manager.AddAsync(store).Wait();

        var store2 = ctx.Informations.FirstOrDefault(u => u.InformationId == store.InformationId);
        Assert.IsNotNull(store2);
    }

    [TestMethod]
    public void UpdateAsyncTest()
    {
        var store = ctx.Informations.FirstOrDefault();
        Assert.IsNotNull(store);

        var newP = "Retrait Magasin";
        store.ModeLivraison = newP;

        manager.UpdateAsync(store, store).Wait();

        store = ctx.Informations.Find(store.InformationId);
        Assert.IsNotNull(store);
        Assert.AreEqual(newP, store.ModeLivraison);
    }

    [TestMethod]
    public void RemoveAsyncTest()
    {
        var store = ctx.Informations.FirstOrDefault();
        Assert.IsNotNull(store);

        manager.DeleteAsync(store).Wait();
        store = ctx.Informations.Find(store.InformationId);
        Assert.IsNull(store);
    }
}