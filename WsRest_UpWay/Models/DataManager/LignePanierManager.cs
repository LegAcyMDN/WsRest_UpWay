using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WsRest_UpWay.Models.EntityFramework;
using WsRest_UpWay.Models.Repository;

namespace WsRest_UpWay.Models.DataManager;

public class LignePanierManager : IDataLignePanier
{
    private readonly S215UpWayContext _ctx;

    public LignePanierManager(S215UpWayContext ctx)
    {
        _ctx = ctx;
    }

    public async Task<ActionResult<IEnumerable<LignePanier>>> GetAllAsync(int page = 0)
    {
        return await _ctx.Lignepaniers.ToListAsync();
    }

    public async Task<ActionResult<int>> GetCountAsync()
    {
        return await _ctx.Lignepaniers.CountAsync();
    }

    public Task<ActionResult<LignePanier>> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<ActionResult<LignePanier>> GetByStringAsync(string str)
    {
        throw new NotImplementedException();
    }

    public async Task AddAsync(LignePanier entity)
    {
        await _ctx.Lignepaniers.AddAsync(entity);
        await _ctx.SaveChangesAsync();
    }

    public async Task UpdateAsync(LignePanier entityToUpdate, LignePanier entity)
    {
        _ctx.Entry(entityToUpdate).State = EntityState.Modified;
        entityToUpdate.VeloId = entity.VeloId;
        entityToUpdate.AssuranceId = entity.AssuranceId;
        entityToUpdate.PrixQuantite = entity.PrixQuantite;
        entityToUpdate.QuantitePanier = entity.QuantitePanier;
        await _ctx.SaveChangesAsync();
    }

    public async Task DeleteAsync(LignePanier entity)
    {
        _ctx.Lignepaniers.Remove(entity);
        await _ctx.SaveChangesAsync();
    }

    public async Task<ActionResult<LignePanier>> GetByIdsAsync(int panierId, int veloId)
    {
        return await _ctx.Lignepaniers.FirstOrDefaultAsync(l => l.PanierId == panierId && l.VeloId == veloId);
    }
}