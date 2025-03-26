using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using WsRest_UpWay.Models.EntityFramework;
using WsRest_UpWay.Models.Repository;

namespace WsRest_UpWay.Controllers.Tests;

[TestClass]
public class InformationsControllerTests
{
    [TestMethod]
    public void GetInformationById_ExistingIdPassed_ReturnsRightItem_AvecMoq()
    {
        // Arrange
        var inf = new Information
        {
            InformationId = 1,
            ReductionId = "100",
            AdresseExpeId = 1,
            PanierId = 1,
            ContactInformations = "Carte Bancaire",
            OffreEmail = true,
            ModeLivraison = "Expédition",
            InformationPays = "France",
            InformationRue = "16 rue de l'Arc en Ciel"
        };
        var mockRepository = new Mock<IDataRepository<Information>>();
        mockRepository.Setup(x => x.GetByIdAsync(1).Result).Returns(inf);
        var infController = new InformationsController(mockRepository.Object);

        // Act
        var actionResult = infController.GetInformation(1).Result;

        // Assert   
        Assert.IsNotNull(actionResult);
        Assert.IsNotNull(actionResult.Value);
        Assert.AreEqual(inf, actionResult.Value);
    }

    [TestMethod]
    public void GetInformationById_UnknownIdPassed_ReturnsNotFoundResult_AvecMoq()
    {
        var mockRepository = new Mock<IDataRepository<Information>>();
        var infController = new InformationsController(mockRepository.Object);

        // Act
        var actionResult = infController.GetInformation(0).Result;

        // Assert
        Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));
    }

    [TestMethod]
    public void GetInformationByModeLivraison_ExistingInePassed_AvecMoq()
    {
        throw new NotImplementedException();
    }

    [TestMethod]
    public void GetInformationByModeLivraison_UnknownInePassed_AvecMoq()
    {
        throw new NotImplementedException();
    }

    [TestMethod]
    public void PutInformationTest_AvecMoq()
    {
        var infToEdit = new Information
        {
            InformationId = 1,
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

        // Arrange
        var infEdited = new Information
        {
            InformationId = 1,
            ReductionId = "20",
            RetraitMagasinId = 1,
            AdresseExpeId = 1,
            PanierId = 1,
            ContactInformations = "Apple Pay",
            OffreEmail = false,
            ModeLivraison = "Expédition",
            InformationPays = "France",
            InformationRue = "9 rue du Parapluie"
        };
        var mockRepository = new Mock<IDataRepository<Information>>();
        mockRepository.Setup(x => x.GetByIdAsync(1).Result).Returns(infToEdit);
        var infController = new InformationsController(mockRepository.Object);

        // Act
        var actionResult = infController.PutInformation(1, infEdited).Result;

        // Assert
        Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult");
    }

    [TestMethod]
    public void PostInformationTest_ModelValidated_CreationOK()
    {
        // Arrange
        var mockRepository = new Mock<IDataRepository<Information>>();
        var infController = new InformationsController(mockRepository.Object);
        var inf = new Information
        {
            InformationId = 2,
            ReductionId = "1",
            RetraitMagasinId = 3,
            AdresseExpeId = 4,
            PanierId = 8,
            ContactInformations = "Carte Bancaire",
            OffreEmail = true,
            ModeLivraison = "Expédition",
            InformationPays = "France",
            InformationRue = "16 rue de l'Arc en Ciel"
        };

        // Act
        var actionResult = infController.PostInformation(inf).Result;

        // Assert
        Assert.IsInstanceOfType(actionResult, typeof(ActionResult<Information>), "Pas un ActionResult<Information>");
        Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");
        var result = actionResult.Result as CreatedAtActionResult;
        Assert.IsInstanceOfType(result.Value, typeof(Information), "Pas un Information");
        inf.InformationId = ((Information)result.Value).InformationId;
        Assert.AreEqual(inf, (Information)result.Value, "Informations pas identiques");
    }


    [TestMethod]
    public void DeleteInformationTest_AvecMoq()
    {
        // Arrange
        var inf = new Information
        {
            InformationId = 1,
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
        var mockRepository = new Mock<IDataRepository<Information>>();
        mockRepository.Setup(x => x.GetByIdAsync(2).Result).Returns(inf);
        var infController = new InformationsController(mockRepository.Object);

        // Act
        var actionResult = infController.DeleteInformation(2).Result;

        // Assert
        Assert.IsInstanceOfType(actionResult, typeof(NoContentResult),
            "Pas un NoContentResult"); // Test du type de retour
    }
}