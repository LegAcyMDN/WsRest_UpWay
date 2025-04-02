using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WsRest_UpWay.Models.EntityFramework;
using WsRest_UpWay.Models.Repository;

namespace WsRest_UpWay.Models.DataManager;

public class MarquageVeloManager : IDataRepository<MarquageVelo>
{
    public const int PAGE_SIZE = 20;
    private readonly S215UpWayContext _ctx;

    public MarquageVeloManager(S215UpWayContext ctx)
    {
        _ctx = ctx;
    }

    public async Task<ActionResult<IEnumerable<MarquageVelo>>> GetAllAsync(int page = 0)
    {
        return await _ctx.Marquagevelos.Skip(PAGE_SIZE * page).Take(PAGE_SIZE).ToListAsync();
    }

    public async Task<ActionResult<int>> GetCountAsync()
    {
        return await _ctx.Marquagevelos.CountAsync();
    }

    public Task<ActionResult<MarquageVelo>> GetByIdAsync(int id)
    {
        throw new NotImplementedException("Use GeyByString instead.");
    }

    public async Task<ActionResult<MarquageVelo>> GetByStringAsync(string str)
    {
        return await _ctx.Marquagevelos.FirstOrDefaultAsync(x => x.CodeMarquage.Equals(str));
    }

    public async Task AddAsync(MarquageVelo entity)
    {
        await _ctx.Marquagevelos.AddAsync(entity);
        await _ctx.SaveChangesAsync();
    }

    public async Task UpdateAsync(MarquageVelo entityToUpdate, MarquageVelo entity)
    {
        _ctx.Entry(entityToUpdate).State = EntityState.Modified;
        entityToUpdate.PrixMarquage = entity.PrixMarquage;
        await _ctx.SaveChangesAsync();
    }

    public async Task DeleteAsync(MarquageVelo entity)
    {
        _ctx.Marquagevelos.Remove(entity);
        await _ctx.SaveChangesAsync();
    }
}