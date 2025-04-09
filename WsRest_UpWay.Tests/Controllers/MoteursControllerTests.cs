using Microsoft.VisualStudio.TestTools.UnitTesting;
using WsRest_UpWay.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WsRest_UpWay.Models.EntityFramework;
using WsRest_UpWay.Models.Repository;

namespace WsRest_UpWay.Controllers.Tests;

[TestClass()]
public class MoteursControllerTests
{
    [TestMethod()]
    public async Task GetMoteurs_ReturnsListOfMoteurs()
    {
        // Arrange
        var moteurList = new List<Moteur>
            {
                new Moteur { MoteurId = 1, ModeleMoteur = "Model1", CoupleMoteur = "100Nm", VitesseMaximal = "50km/h" },
                new Moteur { MoteurId = 2, ModeleMoteur = "Model2", CoupleMoteur = "120Nm", VitesseMaximal = "55km/h" }
            };
        var mockRepo = new Mock<IDataRepository<Moteur>>();
        mockRepo.Setup(x => x.GetAllAsync(0)).ReturnsAsync(moteurList);
        var controller = new MoteursController(mockRepo.Object);

        // Act
        var result = await controller.GetMoteurs();

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(2, result.Value.Count());
    }

    [TestMethod()]
    public async Task GetMoteur_ExistingId_ReturnsMoteur()
    {
        // Arrange
        var moteur = new Moteur { MoteurId = 1, ModeleMoteur = "Model1", CoupleMoteur = "100Nm", VitesseMaximal = "50km/h" };
        var mockRepo = new Mock<IDataRepository<Moteur>>();
        mockRepo.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(moteur);
        var controller = new MoteursController(mockRepo.Object);

        // Act
        var result = await controller.GetMoteur(1);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(moteur, result.Value);
    }

    [TestMethod()]
    public async Task GetMoteur_UnknownId_ReturnsNotFound()
    {
        // Arrange
        var mockRepo = new Mock<IDataRepository<Moteur>>();
        mockRepo.Setup(x => x.GetByIdAsync(0)).ReturnsAsync((Moteur)null);  // Utiliser `ReturnsAsync` pour retourner null
        var controller = new MoteursController(mockRepo.Object);

        // Act
        var result = await controller.GetMoteur(0);

        // Assert
        Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
    }

    [TestMethod()]
    public async Task PutMoteur_ValidUpdate_ReturnsNoContent()
    {
        // Arrange
        var moteurBefore = new Moteur { MoteurId = 1, ModeleMoteur = "Model1", CoupleMoteur = "100Nm", VitesseMaximal = "50km/h" };
        var moteurAfter = new Moteur { MoteurId = 1, ModeleMoteur = "Model2", CoupleMoteur = "120Nm", VitesseMaximal = "55km/h" };
        var mockRepo = new Mock<IDataRepository<Moteur>>();
        mockRepo.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(moteurBefore);
        var controller = new MoteursController(mockRepo.Object);

        // Act
        var result = await controller.PutMoteur(1, moteurAfter);

        // Assert
        Assert.IsInstanceOfType(result, typeof(NoContentResult));
    }

    [TestMethod()]
    public async Task PutMoteur_IdMismatch_ReturnsBadRequest()
    {
        // Arrange
        var moteur = new Moteur { MoteurId = 2, ModeleMoteur = "Model1" };
        var mockRepo = new Mock<IDataRepository<Moteur>>();
        var controller = new MoteursController(mockRepo.Object);

        // Act
        var result = await controller.PutMoteur(1, moteur);

        // Assert
        Assert.IsInstanceOfType(result, typeof(BadRequestResult));
    }

    [TestMethod()]
    public async Task PostMoteur_ValidMoteur_ReturnsCreatedResult()
    {
        // Arrange
        var moteur = new Moteur { MoteurId = 1, ModeleMoteur = "Model1", CoupleMoteur = "100Nm", VitesseMaximal = "50km/h" };
        var mockRepo = new Mock<IDataRepository<Moteur>>();
        var controller = new MoteursController(mockRepo.Object);

        // Act
        var result = await controller.PostMoteur(moteur);

        // Assert
        Assert.IsInstanceOfType(result.Result, typeof(CreatedAtActionResult));
        var created = result.Result as CreatedAtActionResult;
        Assert.AreEqual(moteur, created.Value);
    }

    [TestMethod()]
    public async Task DeleteMoteur_ExistingMoteur_ReturnsNoContent()
    {
        // Arrange
        var moteur = new Moteur { MoteurId = 1, ModeleMoteur = "Model1" };
        var mockRepo = new Mock<IDataRepository<Moteur>>();
        mockRepo.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(moteur);
        var controller = new MoteursController(mockRepo.Object);

        // Act
        var result = await controller.DeleteMoteur(1);

        // Assert
        Assert.IsInstanceOfType(result, typeof(NoContentResult));
    }

    [TestMethod()]
    public async Task DeleteMoteur_UnknownId_ReturnsNotFound()
    {
        // Arrange
        var mockRepo = new Mock<IDataRepository<Moteur>>();
        mockRepo.Setup(x => x.GetByIdAsync(0)).ReturnsAsync((Moteur)null);
        var controller = new MoteursController(mockRepo.Object);

        // Act
        var result = await controller.DeleteMoteur(0);

        // Assert
        Assert.IsInstanceOfType(result, typeof(NotFoundResult));
    }
}