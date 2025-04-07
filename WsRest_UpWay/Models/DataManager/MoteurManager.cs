using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WsRest_UpWay.Models.Cache;
using WsRest_UpWay.Models.EntityFramework;
using WsRest_UpWay.Models.Repository;

namespace WsRest_UpWay.Models.DataManager;

public class MoteurManager : IDataRepository<Moteur>
{
    public const int PAGE_SIZE = 20;
    private readonly ICache _cache;
    private readonly S215UpWayContext upwaysDbContext;

    public MoteurManager(S215UpWayContext context, ICache cache)
    {
        upwaysDbContext = context;
        _cache = cache;
    }

    public async Task AddAsync(Moteur moteur)
    {
        await upwaysDbContext.Moteurs.AddAsync(moteur);
        await upwaysDbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Moteur moteur)
    {
        upwaysDbContext.Moteurs.Remove(moteur);
        await upwaysDbContext.SaveChangesAsync();
    }

    public async Task<ActionResult<IEnumerable<Moteur>>> GetAllAsync(int page = 0)
    {
        return await _cache.GetOrCreateAsync("engine:all/" + page,
            async () => await upwaysDbContext.Moteurs.Skip(page * PAGE_SIZE).Take(PAGE_SIZE).ToListAsync());
    }

    public async Task<ActionResult<Moteur>> GetByIdAsync(int id)
    {
        return await _cache.GetOrCreateAsync("engine:" + id,
            async () => await upwaysDbContext.Moteurs.FirstOrDefaultAsync(a => a.MoteurId == id));
    }

    public async Task<ActionResult<Moteur>> GetByStringAsync(string str)
    {
        throw new NotImplementedException();
    }

    public async Task<ActionResult<int>> GetCountAsync()
    {
        return await _cache.GetOrCreateAsync("engine:count", async () => await upwaysDbContext.Moteurs.CountAsync());
    }

    public async Task UpdateAsync(Moteur moteurToUpdate, Moteur moteur)
    {
        upwaysDbContext.Entry(moteurToUpdate).State = EntityState.Modified;
        moteurToUpdate.MoteurId = moteur.MoteurId;
        moteurToUpdate.MarqueId = moteur.MarqueId;
        moteurToUpdate.ModeleMoteur = moteur.ModeleMoteur;
        moteurToUpdate.CoupleMoteur = moteur.CoupleMoteur;
        moteurToUpdate.VitesseMaximal = moteur.VitesseMaximal;
        upwaysDbContext.SaveChangesAsync();
    }
}