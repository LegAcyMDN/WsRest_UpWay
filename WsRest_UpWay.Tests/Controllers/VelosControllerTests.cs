using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WsRest_UpWay.Models.DataManager;
using WsRest_UpWay.Models.EntityFramework;
using WsRest_UpWay.Models.Repository;

namespace WsRest_UpWay.Controllers.Tests;

[TestClass]
public class VelosControllerTests
{
    private IDataVelo data;
    private S215UpWayContext _context { get; set; }
    private VelosController _controller { get; set; }

    [TestInitialize]
    public void Init()
    {
        var builder = new DbContextOptionsBuilder<S215UpWayContext>().UseNpgsql("");
        _context = new S215UpWayContext(builder.Options);
        data = new VeloManager(_context);
        _controller = new VelosController(data);
    }

    [TestMethod]
    public void GetVelosTest()
    {
        var result = _controller.GetVelos().Result.Value;
        var velos = _context.Velos.ToList();

        Assert.IsInstanceOfType(result, typeof(List<Velo>));
        CollectionAssert.AreEqual(result.ToList(), velos);
    }

    [TestMethod]
    public void GetVeloByIdAvecMoq()
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
        var mockRepository = new Mock<IDataVelo>();
        mockRepository.Setup(x => x.GetByIdAsync(1).Result).Returns(velo);
        var velocontroller = new VelosController(mockRepository.Object);

        var actionResult = velocontroller.GetVelo(1).Result;
    }

    public void GetVeloByFiltresAvecMoq()
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
            TailleMin = "150",
            TailleMax = "180",
            NombreKms = "347",
            PrixRemise = 150,
            PrixNeuf = 200,
            PourcentageReduction = 0,
            DescriptifVelo = "Ce vélo est bien mais en vrai il est pas ouf",
            QuantiteVelo = 1,
            PositionMoteur = "Avant",
            CapaciteBatterie = "120 Wh"
        };
        var mockRepository = new Mock<IDataVelo>();
        mockRepository
            .Setup(x => x.GetByFiltresAsync("150", 1, 1, 1, 2021, "347", "Avant", "Kryptonite", null, "120 Wh", null,
                null, null, 10)).ReturnsAsync(new ActionResult<IEnumerable<Velo>>(new List<Velo> { velo }));
        var velocontroller = new VelosController(mockRepository.Object);

        var actionResult = velocontroller.GetVelo(1).Result;
    }
}