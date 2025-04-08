using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WsRest_UpWay.Models.EntityFramework;
using WsRest_UpWay.Models.Repository;

namespace WsRest_UpWay.Models.DataManager;

public class AjouterAccessoireManager : IDataRepository<AjouterAccessoire>
{
    public const int PAGE_SIZE = 20;
    private readonly S215UpWayContext upwaysDbContext;

    public AjouterAccessoireManager()
    {
    }

    public AjouterAccessoireManager(S215UpWayContext context)
    {
        upwaysDbContext = context;
    }

    public async Task AddAsync(AjouterAccessoire ajoAcs)
    {
        await upwaysDbContext.Ajouteraccessoires.AddAsync(ajoAcs);
        await upwaysDbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(AjouterAccessoire ajoAcs)
    {
        upwaysDbContext.Ajouteraccessoires.Remove(ajoAcs);
        await upwaysDbContext.SaveChangesAsync();
    }

    public async Task<ActionResult<IEnumerable<AjouterAccessoire>>> GetAllAsync(int page = 0)
    {
        return await upwaysDbContext.Ajouteraccessoires.Skip(page * PAGE_SIZE).Take(PAGE_SIZE).ToListAsync();
    }

    public async Task<ActionResult<AjouterAccessoire>> GetByIdAsync(int id)
    {
        return await upwaysDbContext.Ajouteraccessoires.FirstOrDefaultAsync(a => a.AccessoireId == id);
    }

    public async Task<ActionResult<AjouterAccessoire>> GetByStringAsync(string str)
    {
        throw new NotImplementedException();
    }

    public async Task<ActionResult<int>> GetCountAsync()
    {
        throw new NotImplementedException();
    }

    public async Task UpdateAsync(AjouterAccessoire ajoAcsToUpdate, AjouterAccessoire ajoAcs)
    {
        upwaysDbContext.Entry(ajoAcsToUpdate).State = EntityState.Modified;
        ajoAcsToUpdate.QuantiteAccessoire = ajoAcs.QuantiteAccessoire;
        await upwaysDbContext.SaveChangesAsync();
    }
}