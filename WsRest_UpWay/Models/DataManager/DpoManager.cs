using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WsRest_UpWay.Models.Cache;
using WsRest_UpWay.Models.EntityFramework;
using WsRest_UpWay.Models.Repository;

namespace WsRest_UpWay.Models.DataManager;

public class DpoManager : IDataRepository<Dpo>
{
    public const int PAGE_SIZE = 20;
    private readonly ICache _cache;
    private readonly S215UpWayContext upwaysDbContext;

    public DpoManager(S215UpWayContext context, ICache cache)
    {
        upwaysDbContext = context;
        _cache = cache;
    }

    public async Task AddAsync(Dpo dpo)
    {
        await upwaysDbContext.Dpos.AddAsync(dpo);
        await upwaysDbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Dpo dpo)
    {
        upwaysDbContext.Dpos.Remove(dpo);
        await upwaysDbContext.SaveChangesAsync();
    }

    public async Task<ActionResult<IEnumerable<Dpo>>> GetAllAsync(int page = 0)
    {
        return await _cache.GetOrCreateAsync("dpo:all/" + page,
            async () => await upwaysDbContext.Dpos.Skip(page * PAGE_SIZE).Take(PAGE_SIZE).ToListAsync());
    }

    public async Task<ActionResult<Dpo>> GetByIdAsync(int id)
    {
        return await _cache.GetOrCreateAsync("dpo:" + id,
            async () => await upwaysDbContext.Dpos.FirstOrDefaultAsync(a => a.DpoId == id));
    }

    public Task<ActionResult<Dpo>> GetByStringAsync(string str)
    {
        throw new NotImplementedException();
    }

    public async Task<ActionResult<int>> GetCountAsync()
    {
        return await _cache.GetOrCreateAsync("dpo:count", async () => await upwaysDbContext.Dpos.CountAsync());
    }

    public async Task UpdateAsync(Dpo dpoToUpdate, Dpo dpo)
    {
        upwaysDbContext.Entry(dpoToUpdate).State = EntityState.Modified;
        dpoToUpdate.DpoId = dpo.DpoId;
        dpoToUpdate.ClientId = dpo.ClientId;
        dpoToUpdate.TypeOperation = dpo.TypeOperation;
        dpoToUpdate.DateRequeteDpo = dpo.DateRequeteDpo;
        await upwaysDbContext.SaveChangesAsync();
    }
}