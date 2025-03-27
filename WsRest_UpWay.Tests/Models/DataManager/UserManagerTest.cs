using System.IO;
using System.Linq;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WsRest_UpWay.Models.EntityFramework;

namespace WsRest_UpWay.Models.DataManager.Tests;

[TestClass]
[TestSubject(typeof(UserManager))]
public class UserManagerTest
{
    private S215UpWayContext ctx;
    private UserManager manager;

    [TestInitialize]
    public void Initialize()
    {
        var builder = new DbContextOptionsBuilder<S215UpWayContext>();
        builder.UseSqlite("Data Source=S215UpWay.db");

        ctx = new S215UpWayContext(builder.Options);
        ctx.Database.Migrate();
        ctx.Database.ExecuteSqlRaw(File.ReadAllText("inserts.sql"));

        manager = new UserManager(ctx);
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
        CollectionAssert.AreEquivalent(ctx.Compteclients.Take(UserManager.PAGE_SIZE).ToList(), result.Value.ToList());
    }

    [TestMethod]
    public void GetAllAsyncPage1Test()
    {
        var result = manager.GetAllAsync(1).Result;

        Assert.IsNotNull(result);
        Assert.IsNotNull(result.Value);
        CollectionAssert.AreEquivalent(
            ctx.Compteclients.Skip(UserManager.PAGE_SIZE * 1).Take(UserManager.PAGE_SIZE).ToList(),
            result.Value.ToList());
    }

    [TestMethod]
    public void GetByIdAsyncTest()
    {
        var expected = ctx.Compteclients.FirstOrDefault();
        Assert.IsNotNull(expected);

        var result = manager.GetByIdAsync(expected.ClientId).Result;

        Assert.IsNotNull(result);
        Assert.IsNotNull(result.Value);
        Assert.AreEqual(expected, result.Value);
    }

    [TestMethod]
    public void GetByStringAsyncTest()
    {
        var expected = ctx.Compteclients.FirstOrDefault();
        Assert.IsNotNull(expected);

        var result = manager.GetByStringAsync(expected.EmailClient).Result;

        Assert.IsNotNull(result);
        Assert.IsNotNull(result.Value);
        Assert.AreEqual(expected, result.Value);
    }

    [TestMethod]
    public void AddAsyncTest()
    {
        var user = new CompteClient
        {
            LoginClient = "jean.patrick",
            EmailClient = "jean.patrick@gmail.com",
            PrenomClient = "Jean",
            NomClient = "Patrick",
            MotDePasseClient = new PasswordHasher<CompteClient>().HashPassword(null, "Jean@Patrick!"),
            Usertype = Policies.User
        };

        manager.AddAsync(user).Wait();

        var usr = ctx.Compteclients.First(u => u.EmailClient == user.EmailClient);
        Assert.IsNotNull(usr);
    }

    [TestMethod]
    public void UpdateAsyncTest()
    {
        var user = ctx.Compteclients.FirstOrDefault();
        Assert.IsNotNull(user);

        var newP = "Bernard";
        user.PrenomClient = newP;

        manager.UpdateAsync(user, user).Wait();

        var usr = ctx.Compteclients.FirstOrDefault(u => u.EmailClient == user.EmailClient);
        Assert.IsNotNull(usr);
        Assert.AreEqual(newP, usr.PrenomClient);
    }

    [TestMethod]
    public void RemoveAsyncTest()
    {
        var user = ctx.Compteclients.FirstOrDefault();
        Assert.IsNotNull(user);

        manager.DeleteAsync(user).Wait();
        user = ctx.Compteclients.FirstOrDefault(u => u.EmailClient == user.EmailClient);
        Assert.IsNull(user);
    }
}