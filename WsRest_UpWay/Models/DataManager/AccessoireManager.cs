using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WsRest_UpWay.Models.EntityFramework;
using WsRest_UpWay.Models.Repository;

namespace WsRest_UpWay.Models.DataManager;

public class AccessoireManager : IDataAccessoire
{
    public const int PAGE_SIZE = 20;

    private readonly S215UpWayContext? upwaysDbContext;

    public AccessoireManager()
    {
    }

    public AccessoireManager(S215UpWayContext context)
    {
        upwaysDbContext = context;
    }

    public async Task<ActionResult<int>> GetCountAsync()
    {
        return await upwaysDbContext.Accessoires.CountAsync();
    }

    public async Task<ActionResult<Accessoire>> GetByIdAsync(int id)
    {
        return await upwaysDbContext.Accessoires.FirstOrDefaultAsync(u => u.AccessoireId == id);
    }

    public async Task<ActionResult<IEnumerable<PhotoAccessoire>>> GetPhotosByIdAsync(int id)
    {
        return await upwaysDbContext.Photoaccessoires.Where(u => u.AccessoireId == id).ToListAsync();
    }

    public async Task<ActionResult<Accessoire>> GetByStringAsync(string nom)
    {
        return await upwaysDbContext.Accessoires.FirstOrDefaultAsync(u => u.NomAccessoire.ToUpper() == nom.ToUpper());
    }

    public async Task<ActionResult<IEnumerable<Accessoire>>> GetByCategoryPrixAsync(int? categoryId, int min,
    int max, int page)
    {
        IQueryable<Accessoire> accessoirefilt = upwaysDbContext.Accessoires;
        if (categoryId != null) accessoirefilt = accessoirefilt.Where(p => p.CategorieId == categoryId);
        return accessoirefilt.Where(a => a.PrixAccessoire < max && a.PrixAccessoire > min).Skip(page * PAGE_SIZE)
            .Take(PAGE_SIZE).ToList();
    }

    public async Task AddAsync(Accessoire entity)
    {
        await upwaysDbContext.Accessoires.AddAsync(entity);
        await upwaysDbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Accessoire accessoire, Accessoire entity)
    {
        upwaysDbContext.Entry(accessoire).State = EntityState.Modified;
        accessoire.AccessoireId = entity.AccessoireId;
        accessoire.MarqueId = entity.MarqueId;
        accessoire.CategorieId = entity.CategorieId;
        accessoire.NomAccessoire = entity.NomAccessoire;
        accessoire.PrixAccessoire = entity.PrixAccessoire;
        accessoire.DescriptionAccessoire = entity.DescriptionAccessoire;
        await upwaysDbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Accessoire accessoire)
    {
        upwaysDbContext.Accessoires.Remove(accessoire);
        await upwaysDbContext.SaveChangesAsync();
    }

    public async Task<ActionResult<IEnumerable<Accessoire>>> GetAllAsync(int page)
    {
        return await upwaysDbContext.Accessoires.Skip(page * PAGE_SIZE).Take(PAGE_SIZE).ToListAsync();
    }
}