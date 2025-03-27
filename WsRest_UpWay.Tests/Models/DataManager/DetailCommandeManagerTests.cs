using System;
using System.IO;
using System.Linq;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WsRest_UpWay.Models.EntityFramework;

namespace WsRest_UpWay.Models.DataManager.Tests;

[TestClass]
[TestSubject(typeof(DetailCommandeManager))]
public class DetailCommandeManagerTests
{
    private S215UpWayContext ctx;
    private DetailCommandeManager manager;

    [TestInitialize]
    public void Initialize()
    {
        var builder = new DbContextOptionsBuilder<S215UpWayContext>();
        builder.UseSqlite("Data Source=S215UpWay.db");

        ctx = new S215UpWayContext(builder.Options);
        ctx.Database.Migrate();
        ctx.Database.ExecuteSqlRaw(File.ReadAllText("inserts.sql"));

        manager = new DetailCommandeManager(ctx);
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
        CollectionAssert.AreEquivalent(ctx.Detailcommandes.Take(DetailCommandeManager.PAGE_SIZE).ToList(),
            result.Value.ToList());
    }

    [TestMethod]
    public void GetAllAsyncPage1Test()
    {
        var result = manager.GetAllAsync(1).Result;

        Assert.IsNotNull(result);
        Assert.IsNotNull(result.Value);
        CollectionAssert.AreEquivalent(
            ctx.Detailcommandes.Skip(DetailCommandeManager.PAGE_SIZE * 1).Take(DetailCommandeManager.PAGE_SIZE)
                .ToList(), result.Value.ToList());
    }

    [TestMethod]
    public void GetByIdAsyncTest()
    {
        var expected = ctx.Detailcommandes.FirstOrDefault();
        Assert.IsNotNull(expected);

        var result = manager.GetByIdAsync(expected.CommandeId).Result;

        Assert.IsNotNull(result);
        Assert.IsNotNull(result.Value);
        Assert.AreEqual(expected, result.Value);
    }

    [TestMethod]
    public void AddAsyncTest()
    {
        var address_fact = ctx.Adressefacturations.FirstOrDefault();
        Assert.IsNotNull(address_fact);

        var state = ctx.Etatcommandes.FirstOrDefault();
        Assert.IsNotNull(state);

        var date = DateTime.Now;

        var command = new DetailCommande
        {
            ClientId = address_fact.ClientId,
            AdresseFactId = address_fact.AdresseExpId,
            DateAchat = date,
            EtatCommandeId = state.EtatCommandeId,
            RetraitMagasinId = null,
            PanierId = null,
            MoyenPaiement = "Carte Banquaire",
            ModeExpedition = "Expeditiuon"
        };

        manager.AddAsync(command).Wait();

        var store2 = ctx.Detailcommandes.FirstOrDefault(u => u.DateAchat == date);
        Assert.IsNotNull(store2);
    }

    [TestMethod]
    public void UpdateAsyncTest()
    {
        var store = ctx.Detailcommandes.FirstOrDefault();
        Assert.IsNotNull(store);

        var newP = "Carte Bancaire";
        store.MoyenPaiement = newP;

        manager.UpdateAsync(store, store).Wait();

        store = ctx.Detailcommandes.Find(store.CommandeId);
        Assert.IsNotNull(store);
        Assert.AreEqual(newP, store.MoyenPaiement);
    }

    [TestMethod]
    public void RemoveAsyncTest()
    {
        var command = ctx.Detailcommandes.FirstOrDefault();
        Assert.IsNotNull(command);

        // cascade so it doesn't break foreign keys when deleting
        ctx.Entry(command).Collection(e => e.ListePaniers).Load();

        manager.DeleteAsync(command).Wait();
        command = ctx.Detailcommandes.Find(command.CommandeId);
        Assert.IsNull(command);
    }
}