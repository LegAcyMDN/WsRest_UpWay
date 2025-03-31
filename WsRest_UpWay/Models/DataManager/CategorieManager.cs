using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WsRest_UpWay.Models.EntityFramework;
using WsRest_UpWay.Models.Repository;

namespace WsRest_UpWay.Models.DataManager;

public class CategorieManager : IDataRepository<Categorie>
{
    private readonly S215UpWayContext? s215UpWayContext;

    public CategorieManager()
    {
    }

    public CategorieManager(S215UpWayContext context)
    {
        s215UpWayContext = context;
    }

    public async Task AddAsync(Categorie catArticle)
    {
        await s215UpWayContext.Categories.AddAsync(catArticle);
        await s215UpWayContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Categorie catArticle)
    {
        s215UpWayContext.Categories.Remove(catArticle);
        await s215UpWayContext.SaveChangesAsync();
    }

    public async Task<ActionResult<IEnumerable<Categorie>>> GetAllAsync(int page)
    {
        return await s215UpWayContext.Categories.ToListAsync();
    }

    public async Task<ActionResult<int>> GetCountAsync()
    {
        return await s215UpWayContext.Categories.CountAsync();
    }

    public async Task<ActionResult<Categorie>> GetByIdAsync(int id)
    {
        return await s215UpWayContext.Categories.FirstOrDefaultAsync(u => u.CategorieId == id);
    }

    public async Task<ActionResult<Categorie>> GetByStringAsync(string lib)
    {
        return await s215UpWayContext.Categories.FirstOrDefaultAsync(u =>
            u.LibelleCategorie.ToUpper() == lib.ToUpper());
    }

    public async Task UpdateAsync(Categorie catToUpdate, Categorie cat)
    {
        s215UpWayContext.Entry(catToUpdate).State = EntityState.Modified;
        catToUpdate.CategorieId = cat.CategorieId;
        catToUpdate.LibelleCategorie = cat.LibelleCategorie;
        await s215UpWayContext.SaveChangesAsync();
    }
}