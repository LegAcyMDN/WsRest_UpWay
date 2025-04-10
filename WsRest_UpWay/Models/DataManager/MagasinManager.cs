﻿using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WsRest_UpWay.Models.Cache;
using WsRest_UpWay.Models.EntityFramework;
using WsRest_UpWay.Models.Repository;

namespace WsRest_UpWay.Models.DataManager;

public class MagasinManager : IDataRepository<Magasin>
{
    public const int PAGE_SIZE = 20;
    private readonly ICache _cache;
    private readonly S215UpWayContext? upwaysDbContext;

    public MagasinManager(S215UpWayContext context, ICache cache)
    {
        upwaysDbContext = context;
        _cache = cache;
    }

    public async Task<ActionResult<IEnumerable<Magasin>>> GetAllAsync(int page)
    {
        return await _cache.GetOrCreateAsync("stores:all/" + page,
            async () => await upwaysDbContext.Magasins.Skip(page * PAGE_SIZE).Take(PAGE_SIZE).ToListAsync());
    }

    public async Task<ActionResult<int>> GetCountAsync()
    {
        return await _cache.GetOrCreateAsync("stores:count", async () => await upwaysDbContext.Magasins.CountAsync());
    }

    public async Task<ActionResult<Magasin>> GetByIdAsync(int id)
    {
        return await _cache.GetOrCreateAsync("stores:" + id,
            async () => await upwaysDbContext.Magasins.FirstOrDefaultAsync(u => u.MagasinId == id));
    }

    public async Task<ActionResult<Magasin>> GetByStringAsync(string nom)
    {
        return await _cache.GetOrCreateAsync("stores:" + HtmlEncoder.Create().Encode(nom), async () =>
            await upwaysDbContext.Magasins.FirstOrDefaultAsync(u =>
                u.NomMagasin.ToLower().Equals(nom.ToLower()))
        );
    }

    public async Task AddAsync(Magasin mag)
    {
        await upwaysDbContext.Magasins.AddAsync(mag);
        await upwaysDbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Magasin magToUpdate, Magasin mag)
    {
        upwaysDbContext.Entry(magToUpdate).State = EntityState.Modified;
        magToUpdate.MagasinId = mag.MagasinId;
        magToUpdate.NomMagasin = mag.NomMagasin;
        magToUpdate.HoraireMagasin = mag.HoraireMagasin;
        magToUpdate.RueMagasin = mag.RueMagasin;
        magToUpdate.CPMagasin = mag.CPMagasin;
        magToUpdate.VilleMagasin = mag.VilleMagasin;
        await upwaysDbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Magasin mag)
    {
        upwaysDbContext.Magasins.Remove(mag);
        await upwaysDbContext.SaveChangesAsync();
    }
}