using Microsoft.VisualStudio.TestTools.UnitTesting;
using WsRest_UpWay.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using WsRest_UpWay.Models.Repository;
using WsRest_UpWay.Models.EntityFramework;

namespace WsRest_UpWay.Controllers.Tests
{
    [TestClass()]
    public class CaracteristiquesControllerTests
    {
        [TestMethod]
        public void GetCaracteristiqueById_ExistingIdPassed_ReturnsRightItem_AvecMoq()
        {
            // Arrange
            var cara = new Caracteristique
            {
                CaracteristiqueId = 1,
                LibelleCaracteristique = "Véhicule volant",
                ImageCaracteristique = "caracteristique.pnj",
            };
            var mockRepository = new Mock<IDataRepository<Caracteristique>>();
            mockRepository.Setup(x => x.GetByIdAsync(1).Result).Returns(cara);
            var carController = new CaracteristiquesController(mockRepository.Object);

            // Act
            var actionResult = carController.GetCaracteristique(1).Result;

            // Assert   
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            Assert.AreEqual(cara, actionResult.Value);
        }
    }
}