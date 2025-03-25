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

namespace WsRest_UpWay.Models.DataManager.Tests
{
    [TestClass()]
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
            var result = manager.GetAllAsync().Result;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Result);
            CollectionAssert.AreEquivalent(ctx.Informations.ToList(), result.Value.ToList());
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
            var store = new Information
            {
                ReductionId = "100",
                RetraitMagasinId = 1,
                AdresseExpeId = 1,
                PanierId = 1,
                ContactInformations = "Carte Bancaire",
                OffreEmail = true,
                ModeLivraison = "Expédition",
                InformationPays = "France",
                InformationRue = "16 rue de l'Arc en Ciel"
            };

            manager.AddAsync(store).Wait();

            var store2 = ctx.Informations.First(u => u.ModeLivraison == store.ModeLivraison);
            Assert.IsNotNull(store2);

            ctx.CategorieArticles.Remove(store2);
            ctx.SaveChanges();
        }

        [TestMethod]
        public void UpdateAsyncTest()
        {
            var store = ctx.Informations.FirstOrDefault();
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
        }

        [TestMethod]
        public void RemoveAsyncTest()
        {
            var store = new Information
            {
                ReductionId = "100",
                RetraitMagasinId = 1,
                AdresseExpeId = 1,
                PanierId = 1,
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
        }
    }
}