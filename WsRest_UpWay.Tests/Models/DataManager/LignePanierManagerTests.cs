using Microsoft.VisualStudio.TestTools.UnitTesting;
using WsRest_UpWay.Models.DataManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using WsRest_UpWay.Models.EntityFramework;

namespace WsRest_UpWay.Models.DataManager.Tests;


[TestClass()]
[TestSubject(typeof(LignePanierManager))]
public class LignePanierManagerTests
{
    private S215UpWayContext ctx;
    private LignePanierManager manager;

    [TestInitialize]
    public void Initialize()
    {
        var builder = new DbContextOptionsBuilder<S215UpWayContext>();
        builder.UseSqlite("Data Source=S215UpWay.db");

        ctx = new S215UpWayContext(builder.Options);
        ctx.Database.Migrate();
        ctx.Database.ExecuteSqlRaw(File.ReadAllText("inserts.sql"));

        manager = new LignePanierManager(ctx);
    }
    
    [TestCleanup]
    public void Cleanup()
    {
        ctx.Database.EnsureDeleted();
    }

    [TestMethod()]
    public void GetAllAsyncTest()
    {
        var result = manager.GetAllAsync(0).Result;

        Assert.IsNotNull(result);
        Assert.IsNotNull(result.Value);
        CollectionAssert.AreEquivalent(ctx.Lignepaniers.ToList(), result.Value.ToList());
    }

    [TestMethod()]
    public void GetCountAsyncTest()
    {
        var result = manager.GetCountAsync().Result;
        Assert.IsNotNull(result);
        Assert.IsNotNull(result.Value);
        Assert.AreEqual(ctx.Lignepaniers.Count(), result.Value);
    }

    [TestMethod()]
    public void AddAsyncTest()
    {
        var panier = ctx.Paniers.FirstOrDefault();
        Assert.IsNotNull(panier);

        var velo = ctx.Velos.FirstOrDefault();
        Assert.IsNotNull(velo);

        var assurance = ctx.Assurances.FirstOrDefault();
        Assert.IsNotNull(assurance);

        var ligne = new LignePanier
        {
            PanierId = panier.PanierId,
            VeloId = velo.VeloId,
            AssuranceId = assurance.AssuranceId,
            QuantitePanier = 20,
            PrixQuantite = 20.21M
        };

        manager.AddAsync(ligne).Wait();

        var store2 = ctx.Lignepaniers.FirstOrDefault(u => u.PanierId == ligne.PanierId);
        Assert.IsNotNull(store2);
    }

    [TestMethod()]
    public void UpdateAsyncTest()
    {
        var ligne = ctx.Lignepaniers.FirstOrDefault();
        Assert.IsNotNull(ligne);

        var newP = 20.25M;
        ligne.PrixQuantite = newP;

        manager.UpdateAsync(ligne, ligne).Wait();

        ligne = ctx.Lignepaniers.FirstOrDefault(l => l.PanierId == ligne.PanierId && l.VeloId == ligne.VeloId);
        Assert.IsNotNull(ligne);
        Assert.AreEqual(newP, ligne.PrixQuantite);
    }

    [TestMethod()]
    public void DeleteAsyncTest()
    {
        var ligne = ctx.Lignepaniers.FirstOrDefault();
        Assert.IsNotNull(ligne);

        // cascade so it doesn't break foreign keys when deleting
        ctx.Entry(ligne).Collection(e => e.ListeMarquageVelos).Load();
        foreach (var marquage in ligne.ListeMarquageVelos)
        {
            ctx.Entry(marquage).State = EntityState.Modified;
            marquage.PanierId = 0;
        }

        ctx.SaveChanges();
        manager.DeleteAsync(ligne).Wait();
        ligne = ctx.Lignepaniers.FirstOrDefault(l => l.PanierId == ligne.PanierId && l.VeloId ==  ligne.VeloId);
        Assert.IsNull(ligne);
    }

    [TestMethod()]
    public void GetByIdsAsyncTest()
    {
        var expected = ctx.Lignepaniers.FirstOrDefault();
        Assert.IsNotNull(expected);

        var result = manager.GetByIdsAsync(expected.PanierId, expected.VeloId).Result;

        Assert.IsNotNull(result);
        Assert.IsNotNull(result.Value);
        Assert.AreEqual(expected, result.Value);
    }
}