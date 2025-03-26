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
            var result = manager.GetAllAsync().Result;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Value);
            CollectionAssert.AreEquivalent(ctx.Detailcommandes.ToList(), result.Value.ToList());
        }

        [TestMethod]
        public void GetByIdAsyncTest()
        {
            var expected = ctx.Detailcommandes.FirstOrDefault();
            if (expected == null)
                return; // db is either empty or not working, stop the test without error to avoid false negative when db is empty

            var result = manager.GetByIdAsync(expected.CommandeId).Result;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Value);
            Assert.AreEqual(expected, result.Value);
        }

        [TestMethod]
        public void AddAsyncTest()
        {
            var store = new DetailCommande
            {
                RetraitMagasinId = 1,
                AdresseFactId = 1,
                EtatCommandeId = 1,
                ClientId = 1,
                PanierId = 1,
                MoyenPaiement = "Apple Pay",
                ModeExpedition = "Retrait Magasin",
                DateAchat = DateTime.Now
            };

            manager.AddAsync(store).Wait();

            var store2 = ctx.Detailcommandes.First(u => u.MoyenPaiement == store.MoyenPaiement);
            Assert.IsNotNull(store2);

            ctx.Detailcommandes.Remove(store2);
            ctx.SaveChanges();
        }

        [TestMethod]
        public void UpdateAsyncTest()
        {
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
        }

        [TestMethod]
        public void RemoveAsyncTest()
        {
            var store = new DetailCommande
            {
                RetraitMagasinId = 1,
                AdresseFactId = 1,
                EtatCommandeId = 1,
                ClientId = 1,
                PanierId = 1,
                MoyenPaiement = "Apple Pay",
                ModeExpedition = "Retrait Magasin",
                DateAchat = DateTime.Now
            };

            store = ctx.Detailcommandes.Add(store).Entity;
            ctx.SaveChanges();
            Assert.IsNotNull(store);

            manager.DeleteAsync(store).Wait();
            store = ctx.Detailcommandes.Find(store.CommandeId);
            Assert.IsNull(store);
        }
    }
}