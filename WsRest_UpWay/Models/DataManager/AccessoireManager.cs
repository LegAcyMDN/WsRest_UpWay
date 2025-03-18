using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WsRest_UpWay.Models.EntityFramework;
using WsRest_UpWay.Models.Repository;

namespace WsRest_UpWay.Models.DataManager;

public class AccessoireManager : IDataRepository<Accessoire>
{
    private readonly S215UpWayContext? upwaysDbContext;

    public AccessoireManager()
    {
    }

    public AccessoireManager(S215UpWayContext context)
    {
        upwaysDbContext = context;
    }

    public async Task<ActionResult<IEnumerable<Accessoire>>> GetAllAsync()
    {
        return await upwaysDbContext.Accessoires.ToListAsync();
    }

    public async Task<ActionResult<Accessoire>> GetByIdAsync(int id)
    {
        return await upwaysDbContext.Accessoires.FirstOrDefaultAsync(u => u.AccessoireId == id);
    }

    public async Task<ActionResult<Accessoire>> GetByStringAsync(string nom)
    {
        return await upwaysDbContext.Accessoires.FirstOrDefaultAsync(u => u.NomAccessoire.ToUpper() == nom.ToUpper());
    }
    /*
    public async Task<ActionResult<IEnumerable<Accessoire>>> GetByAntivolsAsync(string nom)
    {
        return await upwaysDbContext.Accessoires.Where(u => u.NomAccessoire.ToUpper() == nom.ToUpper()).ToListAsync();
    }
    
    public async Task<ActionResult<IEnumerable<Accessoire>>> GetByAntivolsAsync(string nom)
    {
        return await upwaysDbContext.Accessoires
            .Where(u => u.NomAccessoire.ToUpper() == nom.ToUpper() && u.Categorie.LibelleCategorie == "Antivols")
            .ToListAsync();
    }
    */
    public async Task<ActionResult<IEnumerable<Accessoire>>> GetByAntivolsAsync(string nom)
    {
        return await (from accessoire in upwaysDbContext.Accessoires
                      join categorie in upwaysDbContext.Categories
                      on accessoire.CategorieId equals categorie.CategorieId
                      where accessoire.NomAccessoire.ToUpper() == nom.ToUpper()
                            && categorie.LibelleCategorie == "Antivols"
                      select accessoire).ToListAsync();
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
}