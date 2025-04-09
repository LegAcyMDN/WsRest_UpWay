using Microsoft.VisualStudio.TestTools.UnitTesting;
using WsRest_UpWay.Models.DataManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using WsRest_UpWay.Models.EntityFramework;

namespace WsRest_UpWay.Models.DataManager.Tests;

[TestClass()]
[TestSubject(typeof(PanierManager))]
public class PanierManagerTests
{
    private S215UpWayContext ctx;
    private PanierManager manager;

    [TestInitialize]
    public void Initialize()
    {
        var builder = new DbContextOptionsBuilder<S215UpWayContext>();
        builder.UseSqlite("Data Source=S215UpWay.db");

        ctx = new S215UpWayContext(builder.Options);
        ctx.Database.Migrate();
        ctx.Database.ExecuteSqlRaw(File.ReadAllText("inserts.sql"));

        manager = new PanierManager(ctx);
    }

    [TestCleanup]
    public void Cleanup()
    {
        ctx.Database.EnsureDeleted();
    }

    [TestMethod()]
    public void AddAsyncTest()
    {
        var client = ctx.Compteclients.FirstOrDefault();
        Assert.IsNotNull(client);

        var commande = ctx.Detailcommandes.FirstOrDefault();
        Assert.IsNotNull(commande);

        var panier = new Panier
        {
            ClientId = client.ClientId,
            CommandeId = commande.CommandeId,
            Cookie = "JeSuisUnCookieFondantAuPépiteDeChocolat",
            PrixPanier = 5000
        };

        manager.AddAsync(panier).Wait();

        var panier2 = ctx.Paniers.First(u => u.PanierId == panier.PanierId);
        Assert.IsNotNull(panier2);
    }

    [TestMethod()]
    public void DeleteAsyncTest()
    {
        var panier = ctx.Paniers.FirstOrDefault();
        Assert.IsNotNull(panier);

        // cascade so it doesn't break foreign keys when deleting
        ctx.Entry(panier).Collection(e => e.ListeLignePaniers).Load();
        ctx.Entry(panier).Collection(e => e.ListeAjouterAccessoires).Load();
        ctx.Entry(panier).Collection(e => e.ListeDetailCommandes).Load();
        ctx.Entry(panier).Collection(e => e.ListeInformations).Load();

        foreach (var ligne in panier.ListeLignePaniers)
        {
            ctx.Entry(ligne).State = EntityState.Modified;
            ligne.PanierId = 0;
        }

        foreach (var accessoire in panier.ListeAjouterAccessoires)
        {
            ctx.Entry(accessoire).State = EntityState.Modified;
            accessoire.PanierId = 0;
        }

        foreach (var commande in panier.ListeDetailCommandes)
        {
            ctx.Entry(commande).State = EntityState.Modified;
            commande.PanierId = null;
        }

        foreach (var info in panier.ListeInformations)
        {
            ctx.Entry(info).State = EntityState.Modified;
            info.PanierId = 0;
        }

        ctx.SaveChanges();
        manager.DeleteAsync(panier).Wait();
        panier = ctx.Paniers.Find(panier.PanierId);
        Assert.IsNull(panier);
    }

    [TestMethod()]
    public void GetAllAsyncTest()
    {
        var result = manager.GetAllAsync(0).Result;

        Assert.IsNotNull(result);
        Assert.IsNotNull(result.Value);
        CollectionAssert.AreEquivalent(ctx.Paniers.Take(PanierManager.PAGE_SIZE).ToList(),
            result.Value.ToList());
    }

    [TestMethod()]
    public void GetCountAsyncTest()
    {
        var result = manager.GetCountAsync().Result;
        Assert.IsNotNull(result);
        Assert.IsNotNull(result.Value);
        Assert.AreEqual(ctx.Paniers.Count(), result.Value);
    }

    [TestMethod()]
    public void GetByIdAsyncTest()
    {
        var expected = ctx.Paniers.FirstOrDefault();
        Assert.IsNotNull(expected);

        var result = manager.GetByIdAsync(expected.PanierId).Result;

        Assert.IsNotNull(result);
        Assert.IsNotNull(result.Value);
        Assert.AreEqual(expected, result.Value);
    }

    [TestMethod()]
    public void GetByStringAsyncTest()
    {
        var expected = ctx.Paniers.FirstOrDefault();
        Assert.IsNotNull(expected);

        var result = manager.GetByStringAsync(expected.Cookie).Result;

        Assert.IsNotNull(result);
        Assert.IsNotNull(result.Value);
        Assert.AreEqual(expected, result.Value);
    }

    [TestMethod()]
    public void UpdateAsyncTest()
    {
        var store = ctx.Paniers.FirstOrDefault();
        Assert.IsNotNull(store);

        var newP = 2000;
        store.PrixPanier = newP;

        manager.UpdateAsync(store, store).Wait();

        store = ctx.Paniers.Find(store.PanierId);
        Assert.IsNotNull(store);
        Assert.AreEqual(newP, store.PrixPanier);
    }

    [TestMethod()]
    public void GetByUserTest()
    {
        var expected = ctx.Paniers.FirstOrDefault();
        Assert.IsNotNull(expected);

        var result = manager.GetByUser(expected.PanierId).Result;

        Assert.IsNotNull(result);
        Assert.IsNotNull(result.Value);
        Assert.AreEqual(expected, result.Value);
    }
}