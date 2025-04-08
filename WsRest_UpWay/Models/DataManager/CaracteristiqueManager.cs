using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WsRest_UpWay.Models.Cache;
using WsRest_UpWay.Models.EntityFramework;

namespace WsRest_UpWay.Models.DataManager;

public class CaracteristiquesManager
{
    public const int PAGE_SIZE = 20;
    private readonly ICache _cache;
    private readonly S215UpWayContext? upwaysDbContext;

    public CaracteristiquesManager(S215UpWayContext context, ICache cache)
    {
        upwaysDbContext = context;
        _cache = cache;
    }

    public async Task<ActionResult<int>> GetCountAsync()
    {
        return await _cache.GetOrCreateAsync("caracteristics:count",
            async () => await upwaysDbContext.Caracteristiques.CountAsync());
    }

    public async Task<ActionResult<Caracteristique>> GetByIdAsync(int id)
    {
        return await _cache.GetOrCreateAsync("caracteristics:all/" + id,
            async () => await upwaysDbContext.Caracteristiques.FirstOrDefaultAsync(u => u.CaracteristiqueId == id));
    }

    public async Task<ActionResult<Caracteristique>> GetByStringAsync(string nom)
    {
        return await _cache.GetOrCreateAsync("caracteristics:" + HtmlEncoder.Create().Encode(nom), async () =>
            await upwaysDbContext.Caracteristiques.FirstOrDefaultAsync(u =>
                u.LibelleCaracteristique.ToUpper().Equals(nom.ToUpper())));
    }

    public async Task AddAsync(Caracteristique entity)
    {
        await upwaysDbContext.Caracteristiques.AddAsync(entity);
        await upwaysDbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Caracteristique caracteristique, Caracteristique entity)
    {
        upwaysDbContext.Entry(caracteristique).State = EntityState.Modified;
        caracteristique.CaracteristiqueId = entity.CaracteristiqueId;
        caracteristique.LibelleCaracteristique = entity.LibelleCaracteristique;
        caracteristique.ImageCaracteristique = entity.ImageCaracteristique;
        await upwaysDbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Caracteristique Caracteristiques)
    {
        upwaysDbContext.Caracteristiques.Remove(Caracteristiques);
        await upwaysDbContext.SaveChangesAsync();
    }

    public async Task<ActionResult<IEnumerable<Caracteristique>>> GetAllAsync(int page)
    {
        return await _cache.GetOrCreateAsync("caracteristics:all/" + page,
            async () => await upwaysDbContext.Caracteristiques.Skip(page * PAGE_SIZE).Take(PAGE_SIZE).ToListAsync());
    }
}