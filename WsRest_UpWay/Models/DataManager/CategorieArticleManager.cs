﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WsRest_UpWay.Models.Cache;
using WsRest_UpWay.Models.EntityFramework;
using WsRest_UpWay.Models.Repository;

namespace WsRest_UpWay.Models.DataManager;

public class CategorieArticleManager : IDataRepository<CategorieArticle>
{
    public const int PAGE_SIZE = 20;
    private readonly ICache _cache;
    private readonly S215UpWayContext? s215UpWayContext;

    public CategorieArticleManager(S215UpWayContext context, ICache cache)
    {
        s215UpWayContext = context;
        _cache = cache;
    }

    public async Task AddAsync(CategorieArticle catArticle)
    {
        await s215UpWayContext.CategorieArticles.AddAsync(catArticle);
        await s215UpWayContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(CategorieArticle catArticle)
    {
        s215UpWayContext.CategorieArticles.Remove(catArticle);
        await s215UpWayContext.SaveChangesAsync();
    }

    public async Task<ActionResult<IEnumerable<CategorieArticle>>> GetAllAsync(int page)
    {
        return await s215UpWayContext.CategorieArticles.Skip(page * PAGE_SIZE).Take(PAGE_SIZE).ToListAsync();
    }

    public async Task<ActionResult<int>> GetCountAsync()
    {
        return await s215UpWayContext.CategorieArticles.CountAsync();
    }

    public async Task<ActionResult<CategorieArticle>> GetByIdAsync(int id)
    {
        return await s215UpWayContext.CategorieArticles.FirstOrDefaultAsync(u => u.CategorieArticleId == id);
    }

    public async Task<ActionResult<CategorieArticle>> GetByStringAsync(string str)
    {
        return await s215UpWayContext.CategorieArticles.FirstOrDefaultAsync(u =>
            u.TitreCategorieArticle.ToUpper() == str.ToUpper());
    }

    public async Task UpdateAsync(CategorieArticle catArtToUpdate, CategorieArticle catArticle)
    {
        s215UpWayContext.Entry(catArtToUpdate).State = EntityState.Modified;
        catArtToUpdate.CategorieArticleId = catArticle.CategorieArticleId;
        catArtToUpdate.TitreCategorieArticle = catArticle.TitreCategorieArticle;
        catArtToUpdate.ContenuCategorieArticle = catArticle.ContenuCategorieArticle;
        catArtToUpdate.ImageCategorie = catArticle.ImageCategorie;
        await s215UpWayContext.SaveChangesAsync();
    }
}