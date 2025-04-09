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

namespace WsRest_UpWay.Models.DataManager.Tests;

[TestClass()]
[TestSubject(typeof(CodeReductionManager))]
public class CodeReductionManagerTests
{
    private S215UpWayContext ctx;
    private CodeReductionManager manager;

    [TestInitialize]
    public void Initialize()
    {
        var builder = new DbContextOptionsBuilder<S215UpWayContext>();
        builder.UseSqlite("Data Source=S215UpWay.db");

        ctx = new S215UpWayContext(builder.Options);
        ctx.Database.Migrate();
        ctx.Database.ExecuteSqlRaw(File.ReadAllText("inserts.sql"));

        manager = new CodeReductionManager(ctx);
    }

    [TestCleanup]
    public void Cleanup()
    {
        ctx.Database.EnsureDeleted();
    }

    [TestMethod()]
    public void AddAsyncTest()
    {
        var code = new CodeReduction
        {
            ReductionId = "123456789",
            ActifReduction = true,
            Reduction = 0
        };

        manager.AddAsync(code).Wait();

        var code2 = ctx.Codereductions.First(u => u.ReductionId == code.ReductionId);
        Assert.IsNotNull(code2);
    }

    [TestMethod()]
    public void DeleteAsyncTest()
    {
        var code = ctx.Codereductions.FirstOrDefault();
        Assert.IsNotNull(code);

        // cascade so it doesn't break foreign keys when deleting
        ctx.Entry(code).Collection(e => e.ListeInformations).Load();
        foreach(var info in code.ListeInformations)
        {
            ctx.Entry(info).State = EntityState.Modified;
            info.ReductionId = null;
        }

        manager.DeleteAsync(code).Wait();
        code = ctx.Codereductions.Find(code.ReductionId);
        Assert.IsNull(code);
    }

    [TestMethod()]
    public void GetAllAsyncTest()
    {
        var result = manager.GetAllAsync(0).Result;

        Assert.IsNotNull(result);
        Assert.IsNotNull(result.Value);
        CollectionAssert.AreEquivalent(ctx.Codereductions.Take(CodeReductionManager.PAGE_SIZE).ToList(),
            result.Value.ToList());
    }

    [TestMethod()]
    public void GetCountAsyncTest()
    {
        var result = manager.GetCountAsync().Result;
        Assert.IsNotNull(result);
        Assert.IsNotNull(result.Value);
        Assert.AreEqual(ctx.Codereductions.Count(), result.Value);
    }

    [TestMethod()]
    public void GetByStringAsyncTest()
    {
        var expected = ctx.Codereductions.FirstOrDefault();
        Assert.IsNotNull(expected);

        var result = manager.GetByStringAsync(expected.ReductionId).Result;

        Assert.IsNotNull(result);
        Assert.IsNotNull(result.Value);
        Assert.AreEqual(expected, result.Value);
    }

    [TestMethod()]
    public void UpdateAsyncTest()
    {
        var code = ctx.Codereductions.FirstOrDefault();
        Assert.IsNotNull(code);

        var newP = 50;
        code.Reduction = newP;

        manager.UpdateAsync(code, code).Wait();

        code = ctx.Codereductions.Find(code.ReductionId);
        Assert.IsNotNull(code);
        Assert.AreEqual(newP, code.Reduction);
    }
}