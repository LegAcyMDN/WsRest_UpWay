using System.Collections.Generic;
using System.Threading.Tasks;
using System.Composition;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WsRest_UpWay.Models.EntityFramework;
using WsRest_UpWay.Models.Repository;
using Braintree;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;

namespace WsRest_UpWay.Controllers.Tests;

[TestClass]
public class VelosControllerTests
{
    private VelosController _controller;
    private Mock<IDataVelo> _mockDataRepository;

    [TestInitialize]
    public void TestInitialize() 
    {
        _mockDataRepository = new Mock<IDataVelo>();
        _controller = new VelosController(_mockDataRepository.Object);
    }

    [TestMethod]
    public async Task GetVelos_AvecMoq()
    {
        var velos = new List<Velo>
        {
            new() { VeloId = 1,
            MarqueId = 1,
            CategorieId = 1,
            MoteurId = 1,
            CaracteristiqueVeloId = 1,
            NomVelo = "Macumba",
            AnneeVelo = 2021,
            TailleMin = "1m50",
            TailleMax = "1m80",
            PrixRemise = 150,
            PrixNeuf = 200,
            PourcentageReduction = 0,
            DescriptifVelo = "Ce vélo est bien mais en vrai il est pas ouf",
            QuantiteVelo = 1,
            PositionMoteur = "Avant",
            CapaciteBatterie = "120 Wh" },
            new() { VeloId = 2,
            MarqueId = 1,
            CategorieId = 1,
            MoteurId = 1,
            CaracteristiqueVeloId = 1,
            NomVelo = "Vélo 2",
            AnneeVelo = 2021,
            TailleMin = "1m50",
            TailleMax = "1m80",
            PrixRemise = 150,
            PrixNeuf = 200,
            PourcentageReduction = 0,
            DescriptifVelo = "Ce vélo est bien mais en vrai il est pas ouf",
            QuantiteVelo = 1,
            PositionMoteur = "Avant",
            CapaciteBatterie = "120 Wh" }
        };
        _mockDataRepository.Setup(x => x.GetAllAsync(0)).ReturnsAsync(velos);

        var actionresult = await _controller.GetVelos();

        var velosreturned = actionresult.Value as List<Velo>;

        Assert.IsNotNull(velosreturned);
        CollectionAssert.AreEquivalent(velos,velosreturned);
    }

        [TestMethod]
    public async Task GetVeloById_VeloExiste_AvecMoq()
    {
        var velo = new Velo
        {
            VeloId = 1,
            MarqueId = 1,
            CategorieId = 1,
            MoteurId = 1,
            CaracteristiqueVeloId = 1,
            NomVelo = "Macumba",
            AnneeVelo = 2021,
            TailleMin = "1m50",
            TailleMax = "1m80",
            PrixRemise = 150,
            PrixNeuf = 200,
            PourcentageReduction = 0,
            DescriptifVelo = "Ce vélo est bien mais en vrai il est pas ouf",
            QuantiteVelo = 1,
            PositionMoteur = "Avant",
            CapaciteBatterie = "120 Wh"
        };
        _mockDataRepository.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(velo);
        _controller = new VelosController(_mockDataRepository.Object);

        var actionResult = await _controller.GetVelo(1);
        Assert.IsNotNull(actionResult);
        Assert.AreEqual(velo, actionResult.Value as Velo);
    }
    [TestMethod]
    public async Task GetVeloById_VeloNonExistant_AvecMoq()
    {
        _mockDataRepository.Setup(x => x.GetByIdAsync(100000)).ReturnsAsync((Velo)null);
        
        var actionResult = await _controller.GetVelo(100000);


        Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));

    }
    [TestMethod]
    public async Task PutVelo_AvecMoq()
    {
        var velo = new Velo
        {
            VeloId = 1,
            MarqueId = 1,
            CategorieId = 1,
            MoteurId = 1,
            CaracteristiqueVeloId = 1,
            NomVelo = "Macumba",
            AnneeVelo = 2021,
            TailleMin = "1m50",
            TailleMax = "1m80",
            PrixRemise = 150,
            PrixNeuf = 200,
            PourcentageReduction = 0,
            DescriptifVelo = "Ce vélo est bien mais en vrai il est pas ouf",
            QuantiteVelo = 1,
            PositionMoteur = "Avant",
            CapaciteBatterie = "120 Wh"
        };
        var newvelo = new Velo
        {
            VeloId = 1,
            MarqueId = 1,
            CategorieId = 1,
            MoteurId = 1,
            CaracteristiqueVeloId = 1,
            NomVelo = "Tribu de Dana",
            AnneeVelo = 2021,
            TailleMin = "1m50",
            TailleMax = "1m80",
            PrixRemise = 150,
            PrixNeuf = 200,
            PourcentageReduction = 0,
            DescriptifVelo = "Ce vélo est bien mais en vrai il est pas ouf",
            QuantiteVelo = 1,
            PositionMoteur = "Avant",
            CapaciteBatterie = "120 Wh"
        };
        _mockDataRepository.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(newvelo);
        _mockDataRepository.Setup(x => x.UpdateAsync(It.IsAny<Velo>(), It.IsAny<Velo>())).Returns(Task.CompletedTask);

        var actionResult = await _controller.PutVelo(1, newvelo);

        Assert.IsInstanceOfType(actionResult, typeof(NoContentResult));

    }
    [TestMethod]
    public async Task PutVelo_ReturnNotFoundResult_AvecMoq()
    {
        var newvelo = new Velo
        {
            VeloId = 1,
            MarqueId = 1,
            CategorieId = 1,
            MoteurId = 1,
            CaracteristiqueVeloId = 1,
            NomVelo = "Tribu de Dana",
            AnneeVelo = 2021,
            TailleMin = "1m50",
            TailleMax = "1m80",
            PrixRemise = 150,
            PrixNeuf = 200,
            PourcentageReduction = 0,
            DescriptifVelo = "Ce vélo est bien mais en vrai il est pas ouf",
            QuantiteVelo = 1,
            PositionMoteur = "Avant",
            CapaciteBatterie = "120 Wh"
        };
        _mockDataRepository.Setup(x => x.GetByIdAsync(0));
        
        
        var actionResult = await _controller.PutVelo(1, newvelo);

        Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));

    }
    [TestMethod]
    public async Task PutVelo_ReturnBadRequestResult_AvecMoq()
    {
        var newvelo = new Velo
        {
            VeloId = 1,
            MarqueId = 1,
            CategorieId = 1,
            MoteurId = 1,
            CaracteristiqueVeloId = 1,
            NomVelo = "Tribu de Dana",
            AnneeVelo = 2021,
            TailleMin = "1m50",
            TailleMax = "1m80",
            PrixRemise = 150,
            PrixNeuf = 200,
            PourcentageReduction = 0,
            DescriptifVelo = "Ce vélo est bien mais en vrai il est pas ouf",
            QuantiteVelo = 1,
            PositionMoteur = "Avant",
            CapaciteBatterie = "120 Wh"
        };
        _mockDataRepository.Setup(x => x.GetByIdAsync(1)).ReturnsAsync((Velo)null);


        var actionResult = await _controller.PutVelo(0, newvelo);

        Assert.IsInstanceOfType(actionResult, typeof(BadRequestResult));

    }
    [TestMethod]
    public async Task PostVelo_ReturnCreatedResult_AvecMoq()
    {
        var newvelo = new Velo
        {
            VeloId = 1,
            MarqueId = 1,
            CategorieId = 1,
            MoteurId = 1,
            CaracteristiqueVeloId = 1,
            NomVelo = "Tribu de Dana",
            AnneeVelo = 2021,
            TailleMin = "1m50",
            TailleMax = "1m80",
            PrixRemise = 150,
            PrixNeuf = 200,
            PourcentageReduction = 0,
            DescriptifVelo = "Ce vélo est bien mais en vrai il est pas ouf",
            QuantiteVelo = 1,
            PositionMoteur = "Avant",
            CapaciteBatterie = "120 Wh"
        };
        _mockDataRepository.Setup(x => x.AddAsync(It.IsAny<Velo>())).Returns(Task.CompletedTask);


        var actionResult = await _controller.PostVelo(newvelo);

        Assert.IsInstanceOfType(actionResult, typeof (ActionResult<Velo>), "Pas un ActionResult");
        Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");
        var result = actionResult.Result as CreatedAtActionResult;
        Assert.IsInstanceOfType(result.Value, typeof(Velo), "Pas un Velo");
        newvelo.VeloId = ((Velo)result.Value).VeloId;
        Assert.AreEqual(newvelo, (Velo)result.Value, "Velos pas identiques");
    }
    [TestMethod]
    public async Task PostVelo_ReturnBadRequestObjectResult_AvecMoq()
    {
        var newvelo = new Velo();
        _controller.ModelState.AddModelError("NomVelo","Required");


        var actionResult = await _controller.PostVelo(newvelo);

        Assert.IsInstanceOfType(actionResult.Result, typeof(BadRequestObjectResult), "Pas un BadRequestResult");
    }
    [TestMethod]
    public async Task DeleteAccessoire_ReturnsNoContent_AvecMoq()
    {
        var velo = new Velo
        {
            VeloId = 1,
            MarqueId = 1,
            CategorieId = 1,
            MoteurId = 1,
            CaracteristiqueVeloId = 1,
            NomVelo = "Tribu de Dana",
            AnneeVelo = 2021,
            TailleMin = "1m50",
            TailleMax = "1m80",
            PrixRemise = 150,
            PrixNeuf = 200,
            PourcentageReduction = 0,
            DescriptifVelo = "Ce vélo est bien mais en vrai il est pas ouf",
            QuantiteVelo = 1,
            PositionMoteur = "Avant",
            CapaciteBatterie = "120 Wh"
        };
        _mockDataRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(velo);
        _mockDataRepository.Setup(repo => repo.DeleteAsync(It.IsAny<Velo>())).Returns(Task.CompletedTask);

        // Act
        var result = await _controller.DeleteVelo(1);

        // Assert
        Assert.IsInstanceOfType(result, typeof(NoContentResult));
    }
    [TestMethod]
    public async Task DeleteAccessoire_ReturnsNotFoundResult_AvecMoq()
    {
        _mockDataRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((Velo)null);
        _mockDataRepository.Setup(repo => repo.DeleteAsync(It.IsAny<Velo>())).Returns(Task.CompletedTask);

        // Act
        var result = await _controller.DeleteVelo(1);

        // Assert
        Assert.IsInstanceOfType(result, typeof(NotFoundResult));
    }
}