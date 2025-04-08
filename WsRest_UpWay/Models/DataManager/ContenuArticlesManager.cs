using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WsRest_UpWay.Models.Cache;
using WsRest_UpWay.Models.EntityFramework;
using WsRest_UpWay.Models.Repository;

namespace WsRest_UpWay.Models.DataManager;

public class ContenuArticlesManager : IDataContenuArticles
{
    public const int PAGE_SIZE = 20;
    private readonly ICache _cache;
    private readonly S215UpWayContext? s215UpWayContext;

    public ContenuArticlesManager(S215UpWayContext context, ICache cache)
    {
        s215UpWayContext = context;
        _cache = cache;
    }

    public async Task AddAsync(ContenuArticle coc)
    {
        await s215UpWayContext.ContenuArticles.AddAsync(coc);
        await s215UpWayContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(ContenuArticle coc)
    {
        s215UpWayContext.ContenuArticles.Remove(coc);
        await s215UpWayContext.SaveChangesAsync();
    }

    public async Task<ActionResult<IEnumerable<ContenuArticle>>> GetAllAsync(int page)
    {
        return await _cache.GetOrCreateAsync("articles_content:all/" + page,
            async () => await s215UpWayContext.ContenuArticles.Skip(page * PAGE_SIZE).Take(PAGE_SIZE).ToListAsync());
    }

    public async Task<ActionResult<int>> GetCountAsync()
    {
        return await _cache.GetOrCreateAsync("articles_content:count",
            async () => await s215UpWayContext.ContenuArticles.CountAsync());
    }

    public async Task<ActionResult<ContenuArticle>> GetByIdAsync(int id)
    {
        return await _cache.GetOrCreateAsync("articles_content:" + id,
            async () => await s215UpWayContext.ContenuArticles.FirstOrDefaultAsync(u => u.ContenueId == id));
    }

    public async Task<ActionResult<IEnumerable<ContenuArticle>>> GetByArticleIdAsync(int id)
    {
        return await _cache.GetOrCreateAsync("articles_content:by_article/" + id,
            async () => await s215UpWayContext.ContenuArticles.Where(u => u.ArticleId == id).ToListAsync());
    }

    public async Task<ActionResult<ContenuArticle>> GetByStringAsync(string lib)
    {
        return await _cache.GetOrCreateAsync("articles_content:" + HtmlEncoder.Create().Encode(lib), async () =>
            await s215UpWayContext.ContenuArticles.FirstOrDefaultAsync(u =>
                u.TypeContenu.ToUpper().Equals(lib.ToUpper())));
    }

    public async Task UpdateAsync(ContenuArticle coaToUpdate, ContenuArticle coa)
    {
        s215UpWayContext.Entry(coaToUpdate).State = EntityState.Modified;
        coaToUpdate.ContenueId = coa.ContenueId;
        coaToUpdate.ArticleId = coa.ArticleId;
        coa.PrioriteContenu = coa.PrioriteContenu;
        coa.TypeContenu = coa.TypeContenu;
        coa.Contenu = coa.Contenu;
    }
}