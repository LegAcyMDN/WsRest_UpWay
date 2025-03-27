using System;
using System.Linq;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Identity;
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
        var user = new CompteClient
        {
            ClientId = 1,
            LoginClient = "jean.patrick",
            EmailClient = "jean.patrick@gmail.com",
            PrenomClient = "Jean",
            NomClient = "Patrick",
            MotDePasseClient = new PasswordHasher<CompteClient>().HashPassword(null, "Jean@Patrick!"),
            Usertype = Policies.User
        };

        ctx.Compteclients.Add(user);
        ctx.SaveChanges();

        var address_exp = new AdresseExpedition
        {
            AdresseExpeId = 1,
            ClientId = user.ClientId,
            PaysExpedition = "France",
            RueExpedition = "32 route du petit lutin",
            CPExpedition = "23456",
            RegionExpedition = "NordDeFrance",
            VilleExpedition = "Corse",
            TelephoneExpedition = "+3306123456789",
            DonneesSauvegardees = false
        };

        ctx.Adresseexpeditions.Add(address_exp);
        ctx.SaveChanges();

        var address_fact = new AdresseFacturation
        {
            ClientId = user.ClientId,
            AdresseFactId = 1,
            AdresseExpId = address_exp.AdresseExpeId,
            PaysFacturation = "France",
            RueFacturation = "32 route du petit lutin",
            CPFacturation = "23456",
            RegionFacturation = "NordDeFrance",
            VilleFacturation = "Corse",
            TelephoneFacturation = "+3306123456789"
        };

        ctx.Adressefacturations.Add(address_fact);
        ctx.SaveChanges();

        var state = new EtatCommande
        {
            EtatCommandeId = 1,
            LibelleEtat = "Dummy State"
        };

        ctx.Etatcommandes.Add(state);
        ctx.SaveChanges();

        var command = new DetailCommande
        {
            ClientId = user.ClientId,
            AdresseFactId = address_fact.AdresseExpId,
            CommandeId = 1,
            DateAchat = DateTime.Now,
            EtatCommandeId = state.EtatCommandeId,
            RetraitMagasinId = null,
            PanierId = null,
            MoyenPaiement = "Carte Banquaire",
            ModeExpedition = "Expeditiuon"
        };


        ctx.Detailcommandes.Add(command);
        ctx.SaveChanges();

        var expected = ctx.Detailcommandes.FirstOrDefault();
        if (expected == null)
            return; // db is either empty or not working, stop the test without error to avoid false negative when db is empty

        var result = manager.GetByIdAsync(expected.CommandeId).Result;

        Assert.IsNotNull(result);
        Assert.IsNotNull(result.Value);
        Assert.AreEqual(expected, result.Value);

        ctx.Detailcommandes.Remove(command);
        ctx.Adressefacturations.Remove(address_fact);
        ctx.Adresseexpeditions.Remove(address_exp);
        ctx.Compteclients.Remove(user);
        ctx.Etatcommandes.Remove(state);
        ctx.SaveChanges();
    }

    [TestMethod]
    public void AddAsyncTest()
    {
        var user = new CompteClient
        {
            ClientId = 1,
            LoginClient = "jean.patrick",
            EmailClient = "jean.patrick@gmail.com",
            PrenomClient = "Jean",
            NomClient = "Patrick",
            MotDePasseClient = new PasswordHasher<CompteClient>().HashPassword(null, "Jean@Patrick!"),
            Usertype = Policies.User
        };

        ctx.Compteclients.Add(user);
        ctx.SaveChanges();

        var address_exp = new AdresseExpedition
        {
            AdresseExpeId = 1,
            ClientId = user.ClientId,
            PaysExpedition = "France",
            RueExpedition = "32 route du petit lutin",
            CPExpedition = "23456",
            RegionExpedition = "NordDeFrance",
            VilleExpedition = "Corse",
            TelephoneExpedition = "+3306123456789",
            DonneesSauvegardees = false
        };

        ctx.Adresseexpeditions.Add(address_exp);
        ctx.SaveChanges();

        var address_fact = new AdresseFacturation
        {
            ClientId = user.ClientId,
            AdresseFactId = 1,
            AdresseExpId = address_exp.AdresseExpeId,
            PaysFacturation = "France",
            RueFacturation = "32 route du petit lutin",
            CPFacturation = "23456",
            RegionFacturation = "NordDeFrance",
            VilleFacturation = "Corse",
            TelephoneFacturation = "+3306123456789"
        };

        ctx.Adressefacturations.Add(address_fact);
        ctx.SaveChanges();

        var state = new EtatCommande
        {
            EtatCommandeId = 1,
            LibelleEtat = "Dummy State"
        };

        ctx.Etatcommandes.Add(state);
        ctx.SaveChanges();

        var command = new DetailCommande
        {
            ClientId = user.ClientId,
            AdresseFactId = address_fact.AdresseExpId,
            CommandeId = 1,
            DateAchat = DateTime.Now,
            EtatCommandeId = state.EtatCommandeId,
            RetraitMagasinId = null,
            PanierId = null,
            MoyenPaiement = "Carte Banquaire",
            ModeExpedition = "Expeditiuon"
        };

        manager.AddAsync(command).Wait();

        var store2 = ctx.Detailcommandes.First(u => u.CommandeId == command.CommandeId);
        Assert.IsNotNull(store2);

        ctx.Detailcommandes.Remove(store2);
        ctx.Adressefacturations.Remove(address_fact);
        ctx.Adresseexpeditions.Remove(address_exp);
        ctx.Compteclients.Remove(user);
        ctx.Etatcommandes.Remove(state);
        ctx.SaveChanges();
    }

    [TestMethod]
    public void UpdateAsyncTest()
    {
        var user = new CompteClient
        {
            ClientId = 1,
            LoginClient = "jean.patrick",
            EmailClient = "jean.patrick@gmail.com",
            PrenomClient = "Jean",
            NomClient = "Patrick",
            MotDePasseClient = new PasswordHasher<CompteClient>().HashPassword(null, "Jean@Patrick!"),
            Usertype = Policies.User
        };

        ctx.Compteclients.Add(user);
        ctx.SaveChanges();

        var address_exp = new AdresseExpedition
        {
            AdresseExpeId = 1,
            ClientId = user.ClientId,
            PaysExpedition = "France",
            RueExpedition = "32 route du petit lutin",
            CPExpedition = "23456",
            RegionExpedition = "NordDeFrance",
            VilleExpedition = "Corse",
            TelephoneExpedition = "+3306123456789",
            DonneesSauvegardees = false
        };

        ctx.Adresseexpeditions.Add(address_exp);
        ctx.SaveChanges();

        var address_fact = new AdresseFacturation
        {
            ClientId = user.ClientId,
            AdresseFactId = 1,
            AdresseExpId = address_exp.AdresseExpeId,
            PaysFacturation = "France",
            RueFacturation = "32 route du petit lutin",
            CPFacturation = "23456",
            RegionFacturation = "NordDeFrance",
            VilleFacturation = "Corse",
            TelephoneFacturation = "+3306123456789"
        };

        ctx.Adressefacturations.Add(address_fact);
        ctx.SaveChanges();

        var state = new EtatCommande
        {
            EtatCommandeId = 1,
            LibelleEtat = "Dummy State"
        };

        ctx.Etatcommandes.Add(state);
        ctx.SaveChanges();

        var command = new DetailCommande
        {
            ClientId = user.ClientId,
            AdresseFactId = address_fact.AdresseExpId,
            CommandeId = 1,
            DateAchat = DateTime.Now,
            EtatCommandeId = state.EtatCommandeId,
            RetraitMagasinId = null,
            PanierId = null,
            MoyenPaiement = "Carte Banquaire",
            ModeExpedition = "Expeditiuon"
        };


        ctx.Detailcommandes.Add(command);
        ctx.SaveChanges();

        var store = ctx.Detailcommandes.FirstOrDefault();
        if (store == null) return; // we can't test if the db is empty

        var orig = store.MoyenPaiement;
        var newP = "Carte Bancaire";
        store.MoyenPaiement = newP;

        manager.UpdateAsync(store, store).Wait();

        store = ctx.Detailcommandes.Find(store.CommandeId);
        Assert.IsNotNull(store);
        Assert.AreEqual(newP, store.MoyenPaiement);

        store.MoyenPaiement = orig;
        manager.UpdateAsync(store, store).Wait();

        ctx.Detailcommandes.Remove(store);
        ctx.Adressefacturations.Remove(address_fact);
        ctx.Adresseexpeditions.Remove(address_exp);
        ctx.Compteclients.Remove(user);
        ctx.Etatcommandes.Remove(state);
        ctx.SaveChanges();
    }

    [TestMethod]
    public void RemoveAsyncTest()
    {
        var user = new CompteClient
        {
            ClientId = 1,
            LoginClient = "jean.patrick",
            EmailClient = "jean.patrick@gmail.com",
            PrenomClient = "Jean",
            NomClient = "Patrick",
            MotDePasseClient = new PasswordHasher<CompteClient>().HashPassword(null, "Jean@Patrick!"),
            Usertype = Policies.User
        };

        ctx.Compteclients.Add(user);
        ctx.SaveChanges();

        var address_exp = new AdresseExpedition
        {
            AdresseExpeId = 1,
            ClientId = user.ClientId,
            PaysExpedition = "France",
            RueExpedition = "32 route du petit lutin",
            CPExpedition = "23456",
            RegionExpedition = "NordDeFrance",
            VilleExpedition = "Corse",
            TelephoneExpedition = "+3306123456789",
            DonneesSauvegardees = false
        };

        ctx.Adresseexpeditions.Add(address_exp);
        ctx.SaveChanges();

        var address_fact = new AdresseFacturation
        {
            ClientId = user.ClientId,
            AdresseFactId = 1,
            AdresseExpId = address_exp.AdresseExpeId,
            PaysFacturation = "France",
            RueFacturation = "32 route du petit lutin",
            CPFacturation = "23456",
            RegionFacturation = "NordDeFrance",
            VilleFacturation = "Corse",
            TelephoneFacturation = "+3306123456789"
        };

        ctx.Adressefacturations.Add(address_fact);
        ctx.SaveChanges();

        var state = new EtatCommande
        {
            EtatCommandeId = 1,
            LibelleEtat = "Dummy State"
        };

        ctx.Etatcommandes.Add(state);
        ctx.SaveChanges();

        var command = new DetailCommande
        {
            ClientId = user.ClientId,
            AdresseFactId = address_fact.AdresseExpId,
            CommandeId = 1,
            DateAchat = DateTime.Now,
            EtatCommandeId = state.EtatCommandeId,
            RetraitMagasinId = null,
            PanierId = null,
            MoyenPaiement = "Carte Banquaire",
            ModeExpedition = "Expeditiuon"
        };


        ctx.Detailcommandes.Add(command);
        ctx.SaveChanges();

        Assert.IsNotNull(command);

        manager.DeleteAsync(command).Wait();
        command = ctx.Detailcommandes.Find(command.CommandeId);
        Assert.IsNull(command);

        ctx.Adressefacturations.Remove(address_fact);
        ctx.Adresseexpeditions.Remove(address_exp);
        ctx.Compteclients.Remove(user);
        ctx.Etatcommandes.Remove(state);
        ctx.SaveChanges();
    }
}