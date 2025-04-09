using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WsRest_UpWay.Models.Cache;
using WsRest_UpWay.Models.EntityFramework;
using WsRest_UpWay.Models.Repository;

namespace WsRest_UpWay.Models.DataManager;

public class ArticleManager : IDataArticles
{
    public const int PAGE_SIZE = 20;
    private readonly ICache _cache;
    private readonly S215UpWayContext upwaysDbContext;


    public ArticleManager(S215UpWayContext context, ICache cache)
    {
        upwaysDbContext = context;
        _cache = cache;
    }

    public async Task AddAsync(Article art)
    {
        await upwaysDbContext.Articles.AddAsync(art);
        await upwaysDbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Article art)
    {
        upwaysDbContext.Articles.Remove(art);
        await upwaysDbContext.SaveChangesAsync();
    }

    public async Task<ActionResult<IEnumerable<Article>>> GetAllAsync(int page = 0)
    {
        return await upwaysDbContext.Articles.Skip(page * PAGE_SIZE).Take(PAGE_SIZE).ToListAsync();
    }

    public async Task<ActionResult<Article>> GetByIdAsync(int id)
    {
        return await _cache.GetOrCreateAsync("articles:" + id,
            async () => await upwaysDbContext.Articles.FirstOrDefaultAsync(a => a.ArticleId == id));
    }

    public async Task<ActionResult<IEnumerable<Article>>> GetByCategoryIdAsync(int id)
    {
        return await _cache.GetOrCreateAsync("articles:by_category/" + id,
            async () => await upwaysDbContext.Articles.Where(u => u.CategorieArticleId == id).ToListAsync());
    }

    public async Task<ActionResult<Article>> GetByStringAsync(string str)
    {
        throw new NotImplementedException();
    }

    public async Task<ActionResult<int>> GetCountAsync()
    {
        return await _cache.GetOrCreateAsync("articles:count", async () => await upwaysDbContext.Articles.CountAsync());
    }

    public async Task UpdateAsync(Article artToUpdate, Article art)
    {
        upwaysDbContext.Entry(artToUpdate).State = EntityState.Modified;
        artToUpdate.ArticleId = art.ArticleId;
        artToUpdate.CategorieArticleId = art.CategorieArticleId;
        await upwaysDbContext.SaveChangesAsync();
    }
}