using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WsRest_UpWay.Models.EntityFramework;
using WsRest_UpWay.Models.Repository;

namespace WsRest_UpWay.Models.DataManager;

public class LignePanierManager : IDataRepository<LignePanier>
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

    public async Task<ActionResult<LignePanier>> GetByIdAsync(int id)
    {
        return await _ctx.Lignepaniers.FindAsync(id);
    }

    public async Task<ActionResult<LignePanier>> GetByStringAsync(string str)
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
    }
}