using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WsRest_UpWay.Models.EntityFramework;
using WsRest_UpWay.Models.Repository;

namespace WsRest_UpWay.Controllers.Tests;

[TestClass]
public class DetailCommandesControllerTests
{
    [TestMethod]
    public void GetDetailCommandeById_ExistingIdPassed_ReturnsRightItem_AvecMoq()
    {
        // Arrange
        var detCommande = new DetailCommande
        {
            CommandeId = 1,
            RetraitMagasinId = 1,
            AdresseFactId = 1,
            EtatCommandeId = 1,
            ClientId = 1,
            PanierId = 1,
            MoyenPaiement = "Carte Bancaire",
            ModeExpedition = "Retrait Magasin",
            DateAchat = DateTime.Now
        };
        var mockRepository = new Mock<IDataRepository<DetailCommande>>();
        mockRepository.Setup(x => x.GetByIdAsync(1).Result).Returns(detCommande);
        var detCommandeController = new DetailCommandesController(mockRepository.Object);

        // Act
        var actionResult = detCommandeController.GetDetailCommande(1).Result;

        // Assert   
        Assert.IsNotNull(actionResult);
        Assert.IsNotNull(actionResult.Value);
        Assert.AreEqual(detCommande, actionResult.Value);
    }

    [TestMethod]
    public void GetDetailCommandeById_UnknownIdPassed_ReturnsNotFoundResult_AvecMoq()
    {
        var mockRepository = new Mock<IDataRepository<DetailCommande>>();
        mockRepository.Setup(x => x.GetByIdAsync(0).Result).Returns((DetailCommande)null);
        var detCommandeController = new DetailCommandesController(mockRepository.Object);

        // Act
        var actionResult = detCommandeController.GetDetailCommande(0).Result;

        // Assert
        Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));
    }

    [TestMethod]
    public void PutDetailCommandeTest_AvecMoq()
    {
        var detCommandeToEdit = new DetailCommande
        {
            CommandeId = 1,
            RetraitMagasinId = 1,
            AdresseFactId = 1,
            EtatCommandeId = 1,
            ClientId = 1,
            PanierId = 1,
            MoyenPaiement = "Carte Bancaire",
            ModeExpedition = "Retrait Magasin",
            DateAchat = DateTime.Now
        };
        // Arrange
        var detCommandeEdited = new DetailCommande
        {
            CommandeId = 1,
            RetraitMagasinId = 1,
            AdresseFactId = 1,
            EtatCommandeId = 1,
            ClientId = 1,
            PanierId = 1,
            MoyenPaiement = "Apple Pay",
            ModeExpedition = "Expédition",
            DateAchat = DateTime.Now
        };
        var mockRepository = new Mock<IDataRepository<DetailCommande>>();
        mockRepository.Setup(x => x.GetByIdAsync(1).Result).Returns(detCommandeToEdit);
        var detCommandeController = new DetailCommandesController(mockRepository.Object);

        // Act
        var actionResult = detCommandeController.PutDetailCommande(1, detCommandeEdited).Result;

        // Assert
        Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult");
    }

    [TestMethod]
    public void PostDetailCommandeTest_ModelValidated_CreationOK()
    {
        // Arrange
        var mockRepository = new Mock<IDataRepository<DetailCommande>>();
        var detCommandeController = new DetailCommandesController(mockRepository.Object);
        var detCommande = new DetailCommande
        {
            CommandeId = 2,
            RetraitMagasinId = 15,
            AdresseFactId = 12,
            EtatCommandeId = 3,
            ClientId = 23,
            PanierId = 24,
            MoyenPaiement = "Carte Bancaire",
            ModeExpedition = "Retrait Magasin",
            DateAchat = DateTime.Now
        };

        // Act
        var actionResult = detCommandeController.PostDetailCommande(detCommande).Result;

        // Assert
        Assert.IsInstanceOfType(actionResult, typeof(ActionResult<DetailCommande>),
            "Pas un ActionResult<DetailCommande>");
        Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");
        var result = actionResult.Result as CreatedAtActionResult;
        Assert.IsInstanceOfType(result.Value, typeof(DetailCommande), "Pas un DetailCommande");
        detCommande.CommandeId = ((DetailCommande)result.Value).CommandeId;
        Assert.AreEqual(detCommande, (DetailCommande)result.Value, "DetailCommandes pas identiques");
    }


    [TestMethod]
    public void DeleteDetailCommandeTest_AvecMoq()
    {
        // Arrange
        var detCommande = new DetailCommande
        {
            CommandeId = 3,
            RetraitMagasinId = 1,
            AdresseFactId = 1,
            EtatCommandeId = 1,
            ClientId = 1,
            PanierId = 1,
            MoyenPaiement = "Carte Bancaire",
            ModeExpedition = "Retrait Magasin",
            DateAchat = DateTime.Now
        };
        var mockRepository = new Mock<IDataRepository<DetailCommande>>();
        mockRepository.Setup(x => x.GetByIdAsync(2).Result).Returns(detCommande);
        var detCommandeController = new DetailCommandesController(mockRepository.Object);

        // Act
        var actionResult = detCommandeController.DeleteDetailCommande(2).Result;

        // Assert
        Assert.IsInstanceOfType(actionResult, typeof(NoContentResult),
            "Pas un NoContentResult"); // Test du type de retour
    }
}