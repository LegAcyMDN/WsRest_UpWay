using Microsoft.VisualStudio.TestTools.UnitTesting;
using WsRest_UpWay.Models.DataManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using WsRest_UpWay.Models.EntityFramework;
using System.IO;
using Mono.TextTemplating;

namespace WsRest_UpWay.Models.DataManager.Tests;

[TestClass()]
[TestSubject(typeof(EstRealiseManager))]
public class EstRealiseManagerTests
{
    private S215UpWayContext ctx;
    private EstRealiseManager manager;

    [TestInitialize]
    public void Initialize()
    {
        var builder = new DbContextOptionsBuilder<S215UpWayContext>();
        builder.UseSqlite("Data Source=S215UpWay.db");

        ctx = new S215UpWayContext(builder.Options);
        ctx.Database.Migrate();
        ctx.Database.ExecuteSqlRaw(File.ReadAllText("inserts.sql"));

        manager = new EstRealiseManager(ctx);
    }

    [TestCleanup]
    public void Cleanup()
    {
        ctx.Database.EnsureDeleted();
    }

    [TestMethod()]
    public void AddAsyncTest()
    {
        var velo = ctx.Velos.FirstOrDefault();
        Assert.IsNotNull(velo);

        var inspection = ctx.Rapportinspections.FirstOrDefault();
        Assert.IsNotNull(inspection);

        var reparation = ctx.Reparationvelos.FirstOrDefault();
        Assert.IsNotNull(reparation);

        var date = DateTime.Now.ToString();

        var estRealise = new EstRealise
        {
            VeloId = velo.VeloId,
            InspectionId = inspection.InspectionId,
            ReparationId = reparation.ReparationId,
            DateInspection = date,
            CommentaireInspection = "R.A.S",
            HistoriqueInspection = date
        };

        manager.AddAsync(estRealise).Wait();

        var estRealise2 = ctx.Estrealises.FirstOrDefault(u => u.DateInspection == date);
        Assert.IsNotNull(estRealise2);
    }

    [TestMethod()]
    public void DeleteAsyncTest()
    {
        var estRealise = ctx.Estrealises.FirstOrDefault();
        Assert.IsNotNull(estRealise);

        ctx.SaveChanges();
        manager.DeleteAsync(estRealise).Wait();
        estRealise = ctx.Estrealises.Find(estRealise.VeloId, estRealise.InspectionId, estRealise.ReparationId);
        Assert.IsNull(estRealise);
    }

    [TestMethod()]
    public void GetAllAsyncTest()
    {
        var result = manager.GetAllAsync(0).Result;

        Assert.IsNotNull(result);
        Assert.IsNotNull(result.Value);
        CollectionAssert.AreEquivalent(ctx.Estrealises.ToList(),result.Value.ToList());
    }

    [TestMethod()]
    public void GetByIdsAsyncTest()
    {
        var expected = ctx.Estrealises.FirstOrDefault();
        Assert.IsNotNull(expected);

        var result = manager.GetByIdsAsync(expected.VeloId, expected.InspectionId, expected.ReparationId).Result;

        Assert.IsNotNull(result);
        Assert.IsNotNull(result.Value);
        Assert.AreEqual(expected, result.Value);
    }

    [TestMethod()]
    public void GetByIdVeloAsyncTest()
    {
        var expected = ctx.Estrealises.FirstOrDefault();
        Assert.IsNotNull(expected);

        var result = manager.GetByIdVeloAsync(expected.VeloId, expected.EstRealiseRapportInspection.TypeInspection).Result;

        Assert.IsNotNull(result);
        Assert.IsNotNull(result.Value);
        //Assert.AreEqual(expected, result.Value);
        Assert.Fail();
    }

    [TestMethod()]
    public void UpdateAsyncTest()
    {
        var store = ctx.Estrealises.FirstOrDefault();
        Assert.IsNotNull(store);

        var newP = "Un gros problème !";
        store.CommentaireInspection = newP;

        manager.UpdateAsync(store, store).Wait();

        store = ctx.Estrealises.Find(store.VeloId, store.InspectionId, store.ReparationId);
        Assert.IsNotNull(store);
        Assert.AreEqual(newP, store.CommentaireInspection);
    }
}