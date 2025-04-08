using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WsRest_UpWay.Models.Cache;
using WsRest_UpWay.Models.EntityFramework;
using WsRest_UpWay.Models.Repository;

namespace WsRest_UpWay.Models.DataManager;

public class CaracteristiqueVeloManager : IDataRepository<CaracteristiqueVelo>
{
    public const int PAGE_SIZE = 20;
    private readonly ICache _cache;
    private readonly S215UpWayContext upwaysDbContext;

    public CaracteristiqueVeloManager(S215UpWayContext context, ICache cache)
    {
        upwaysDbContext = context;
        _cache = cache;
    }

    public async Task AddAsync(CaracteristiqueVelo cav)
    {
        await upwaysDbContext.Caracteristiquevelos.AddAsync(cav);
        await upwaysDbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(CaracteristiqueVelo cav)
    {
        upwaysDbContext.Caracteristiquevelos.Remove(cav);
        await upwaysDbContext.SaveChangesAsync();
    }

    public async Task<ActionResult<IEnumerable<CaracteristiqueVelo>>> GetAllAsync(int page = 0)
    {
        return await _cache.GetOrCreateAsync("caracteristics_velo:all/" + page,
            async () => await upwaysDbContext.Caracteristiquevelos.Skip(page * PAGE_SIZE).Take(PAGE_SIZE)
                .ToListAsync());
    }

    public async Task<ActionResult<CaracteristiqueVelo>> GetByIdAsync(int id)
    {
        return await _cache.GetOrCreateAsync("caracteristics_velo:" + id,
            async () => await upwaysDbContext.Caracteristiquevelos.FirstOrDefaultAsync(a =>
                a.CaracteristiqueVeloId == id));
    }

    public async Task<ActionResult<CaracteristiqueVelo>> GetByStringAsync(string str)
    {
        throw new NotImplementedException();
    }

    public async Task<ActionResult<int>> GetCountAsync()
    {
        return await _cache.GetOrCreateAsync("caracteristics_velo:count",
            async () => await upwaysDbContext.Caracteristiquevelos.CountAsync());
    }

    public async Task UpdateAsync(CaracteristiqueVelo cavToUpdate, CaracteristiqueVelo cav)
    {
        upwaysDbContext.Entry(cavToUpdate).State = EntityState.Modified;
        cavToUpdate.CaracteristiqueVeloId = cav.CaracteristiqueVeloId;
        cavToUpdate.Poids = cav.Poids;
        cavToUpdate.TubeSelle = cav.TubeSelle;
        cavToUpdate.TypeSuspension = cav.TypeSuspension;
        cavToUpdate.Couleur = cav.Couleur;
        cavToUpdate.TypeCargo = cav.TypeCargo;
        cavToUpdate.EtatBatterie = cav.EtatBatterie;
        cavToUpdate.NombreCycle = cav.NombreCycle;
        cavToUpdate.Materiau = cav.Materiau;
        cavToUpdate.Fourche = cav.Fourche;
        cavToUpdate.Debattement = cav.Debattement;
        cavToUpdate.Amortisseur = cav.Amortisseur;
        cavToUpdate.DebattementAmortisseur = cav.DebattementAmortisseur;
        cavToUpdate.ModelTransmission = cav.ModelTransmission;
        cavToUpdate.NombreVitesse = cav.NombreVitesse;
        cavToUpdate.Freins = cav.Freins;
        cavToUpdate.TaillesRoues = cav.TaillesRoues;
        cavToUpdate.Pneus = cav.Pneus;
        cavToUpdate.SelleTelescopique = cav.SelleTelescopique;
        await upwaysDbContext.SaveChangesAsync();
    }
}