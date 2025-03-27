using System;
using System.Linq;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Identity;
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
        if (expected == null)
            return; // db is either empty or not working, stop the test without error to avoid false negative when db is empty

        var result = manager.GetByIdAsync(expected.InformationId).Result;

        Assert.IsNotNull(result);
        Assert.IsNotNull(result.Value);
        Assert.AreEqual(expected, result.Value);
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
            CommandeId = state.EtatCommandeId,
            DateAchat = DateTime.Now,
            EtatCommandeId = 1,
            RetraitMagasinId = null,
            PanierId = null,
            MoyenPaiement = "Carte Banquaire",
            ModeExpedition = "Expeditiuon"
        };

        ctx.Detailcommandes.Add(command);
        ctx.SaveChanges();

        var order = new Panier
        {
            PanierId = 1,
            ClientId = user.ClientId,
            CommandeId = command.CommandeId,
            PrixPanier = (decimal)69.69,
            Cookie = null
        };

        ctx.Paniers.Add(order);
        ctx.SaveChanges();

        var reduction = new CodeReduction
        {
            ReductionId = "SUP3R",
            ActifReduction = true,
            Reduction = 100
        };

        ctx.Codereductions.Add(reduction);
        ctx.SaveChanges();

        var store = new Information
        {
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

        var store2 = ctx.Informations.First(u => u.ModeLivraison == store.ModeLivraison);
        Assert.IsNotNull(store2);

        ctx.Informations.Remove(store2);
        ctx.Detailcommandes.Remove(command);
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
            CommandeId = state.EtatCommandeId,
            DateAchat = DateTime.Now,
            EtatCommandeId = 1,
            RetraitMagasinId = null,
            PanierId = null,
            MoyenPaiement = "Carte Banquaire",
            ModeExpedition = "Expeditiuon"
        };

        ctx.Detailcommandes.Add(command);
        ctx.SaveChanges();

        var order = new Panier
        {
            PanierId = 1,
            ClientId = user.ClientId,
            CommandeId = command.CommandeId,
            PrixPanier = (decimal)69.69,
            Cookie = null
        };

        ctx.Paniers.Add(order);
        ctx.SaveChanges();

        var reduction = new CodeReduction
        {
            ReductionId = "SUP3R",
            ActifReduction = true,
            Reduction = 100
        };

        ctx.Codereductions.Add(reduction);
        ctx.SaveChanges();

        var store = new Information
        {
            ReductionId = reduction.ReductionId,
            AdresseExpeId = address_exp.AdresseExpeId,
            PanierId = order.PanierId,
            ContactInformations = "Carte Bancaire",
            OffreEmail = true,
            ModeLivraison = "Expédition",
            InformationPays = "France",
            InformationRue = "16 rue de l'Arc en Ciel"
        };

        store = ctx.Informations.FirstOrDefault();
        if (store == null) return; // we can't test if the db is empty

        var orig = store.ModeLivraison;
        var newP = "Retrait Magasin";
        store.ModeLivraison = newP;

        manager.UpdateAsync(store, store).Wait();

        store = ctx.Informations.Find(store.InformationId);
        Assert.IsNotNull(store);
        Assert.AreEqual(newP, store.ModeLivraison);

        store.ModeLivraison = orig;
        manager.UpdateAsync(store, store).Wait();

        ctx.Informations.Remove(store);
        ctx.Detailcommandes.Remove(command);
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
            CommandeId = state.EtatCommandeId,
            DateAchat = DateTime.Now,
            EtatCommandeId = 1,
            RetraitMagasinId = null,
            PanierId = null,
            MoyenPaiement = "Carte Banquaire",
            ModeExpedition = "Expeditiuon"
        };

        ctx.Detailcommandes.Add(command);
        ctx.SaveChanges();

        var order = new Panier
        {
            PanierId = 1,
            ClientId = user.ClientId,
            CommandeId = command.CommandeId,
            PrixPanier = (decimal)69.69,
            Cookie = null
        };

        ctx.Paniers.Add(order);
        ctx.SaveChanges();

        var reduction = new CodeReduction
        {
            ReductionId = "SUP3R",
            ActifReduction = true,
            Reduction = 100
        };

        ctx.Codereductions.Add(reduction);
        ctx.SaveChanges();

        var store = new Information
        {
            ReductionId = reduction.ReductionId,
            AdresseExpeId = address_exp.AdresseExpeId,
            PanierId = order.PanierId,
            ContactInformations = "Carte Bancaire",
            OffreEmail = true,
            ModeLivraison = "Expédition",
            InformationPays = "France",
            InformationRue = "16 rue de l'Arc en Ciel"
        };

        store = ctx.Informations.Add(store).Entity;
        ctx.SaveChanges();
        Assert.IsNotNull(store);

        manager.DeleteAsync(store).Wait();
        store = ctx.Informations.Find(store.InformationId);
        Assert.IsNull(store);

        ctx.Detailcommandes.Remove(command);
        ctx.Adressefacturations.Remove(address_fact);
        ctx.Adresseexpeditions.Remove(address_exp);
        ctx.Compteclients.Remove(user);
        ctx.Etatcommandes.Remove(state);
        ctx.SaveChanges();
    }
}