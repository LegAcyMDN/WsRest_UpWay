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

namespace WsRest_UpWay.Models.DataManager.Tests
{
    [TestClass()]
    [TestSubject(typeof(AjouterAccessoireManager))]
    public class AjouterAccessoireManagerTests
    {
        private S215UpWayContext ctx;
        private AjouterAccessoireManager manager;

        [TestInitialize]
        public void Initialize()
        {
            var builder = new DbContextOptionsBuilder<S215UpWayContext>();
            builder.UseSqlite("Data Source=S215UpWay.db");

            ctx = new S215UpWayContext(builder.Options);
            ctx.Database.Migrate();
            ctx.Database.ExecuteSqlRaw(File.ReadAllText("inserts.sql"));

            manager = new AjouterAccessoireManager(ctx);
        }

        [TestCleanup]
        public void Cleanup()
        {
            ctx.Database.EnsureDeleted();
        }

        [TestMethod()]
        public void AddAsyncTest()
        {
            var accessoire = ctx.Accessoires.FirstOrDefault();
            Assert.IsNotNull(accessoire);

            var panier = ctx.Paniers.FirstOrDefault();
            Assert.IsNotNull(panier);

            var ajoutAccessoire = new AjouterAccessoire
            {
                AccessoireId = accessoire.AccessoireId,
                PanierId = panier.PanierId,
                QuantiteAccessoire = 3
            };

            manager.AddAsync(ajoutAccessoire).Wait();

            var ajoutAccessoire2 = ctx.Ajouteraccessoires.FirstOrDefault(u => u.AccessoireId == ajoutAccessoire.AccessoireId);
            Assert.IsNotNull(ajoutAccessoire2);
        }

        [TestMethod()]
        public void DeleteAsyncTest()
        {
            var ajoutAccessoire = ctx.Ajouteraccessoires.FirstOrDefault();
            Assert.IsNotNull(ajoutAccessoire);

            manager.DeleteAsync(ajoutAccessoire).Wait();
            ajoutAccessoire = ctx.Ajouteraccessoires.Find(ajoutAccessoire.AccessoireId, ajoutAccessoire.PanierId);
            Assert.IsNull(ajoutAccessoire);
        }

        [TestMethod()]
        public void GetAllAsyncTest()
        {
            var result = manager.GetAllAsync(0).Result;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Value);
            CollectionAssert.AreEquivalent(ctx.Ajouteraccessoires.Take(AjouterAccessoireManager.PAGE_SIZE).ToList(),
                result.Value.ToList());
        }

        [TestMethod()]
        public void GetByIdAsyncTest()
        {
            var expected = ctx.Ajouteraccessoires.FirstOrDefault();
            Assert.IsNotNull(expected);

            var result = manager.GetByIdAsync(expected.AccessoireId).Result;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Value);
            Assert.AreEqual(expected, result.Value);
        }

        [TestMethod()]
        public void UpdateAsyncTest()
        {
            var ajoutAccessoire = ctx.Ajouteraccessoires.FirstOrDefault();
            Assert.IsNotNull(ajoutAccessoire);

            var newP = 4;
            ajoutAccessoire.QuantiteAccessoire = newP;

            manager.UpdateAsync(ajoutAccessoire, ajoutAccessoire).Wait();

            ajoutAccessoire = ctx.Ajouteraccessoires.Find(ajoutAccessoire.AccessoireId, ajoutAccessoire.PanierId);
            Assert.IsNotNull(ajoutAccessoire);
            Assert.AreEqual(newP, ajoutAccessoire.QuantiteAccessoire);
        }
    }
}