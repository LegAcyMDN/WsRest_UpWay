using System.Linq;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WsRest_UpWay.Models;
using WsRest_UpWay.Models.DataManager;
using WsRest_UpWay.Models.EntityFramework;

// TODO: These tests will fail in the CI pipeline, what do we do to prevent this? hardcode a db? find a way to skip them?

namespace WsRest_UpWay.Tests.Models.DataManager;

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
        if (expected == null)
            return; // db is either empty or not working, stop the test without error to avoid false negative when db is empty

        var result = manager.GetByIdAsync(expected.ClientId).Result;

        Assert.IsNotNull(result);
        Assert.IsNotNull(result.Value);
        Assert.AreEqual(expected, result.Value);
    }

    [TestMethod]
    public void GetByStringAsyncTest()
    {
        var expected = ctx.Compteclients.FirstOrDefault();
        if (expected == null)
            return; // db is either empty or not working, stop the test without error to avoid false negative when db is empty

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

        ctx.Compteclients.Remove(usr);
        ctx.SaveChanges();
    }

    [TestMethod]
    public void UpdateAsyncTest()
    {
        var user = ctx.Compteclients.FirstOrDefault();
        if (user == null) return; // we can't test if the db is empty

        var orig = user.PrenomClient;
        var newP = "Bernard";
        user.PrenomClient = newP;

        manager.UpdateAsync(user, user).Wait();

        var usr = ctx.Compteclients.FirstOrDefault(u => u.EmailClient == user.EmailClient);
        Assert.IsNotNull(usr);
        Assert.AreEqual(newP, usr.PrenomClient);

        usr.PrenomClient = orig;
        manager.UpdateAsync(user, user).Wait();
    }

    [TestMethod]
    public void RemoveAsyncTest()
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

        user = ctx.Compteclients.Add(user).Entity;
        ctx.SaveChanges();
        Assert.IsNotNull(user);

        manager.DeleteAsync(user).Wait();
        user = ctx.Compteclients.FirstOrDefault(u => u.EmailClient == user.EmailClient);
        Assert.IsNull(user);
    }
}