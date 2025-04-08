using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WsRest_UpWay.Models.Cache;
using WsRest_UpWay.Models.EntityFramework;
using WsRest_UpWay.Models.Repository;

namespace WsRest_UpWay.Models.DataManager;

public class PhotoAccessoireManager : IDataRepository<PhotoAccessoire>
{
    public const int PAGE_SIZE = 20;
    private readonly ICache _cache;
    private readonly S215UpWayContext upwaysDbContext;

    public PhotoAccessoireManager(S215UpWayContext context, ICache cache)
    {
        upwaysDbContext = context;
        _cache = cache;
    }

    public async Task AddAsync(PhotoAccessoire phoAcs)
    {
        await upwaysDbContext.Photoaccessoires.AddAsync(phoAcs);
        await upwaysDbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(PhotoAccessoire phoAcs)
    {
        upwaysDbContext.Photoaccessoires.Remove(phoAcs);
        await upwaysDbContext.SaveChangesAsync();
    }

    public async Task<ActionResult<IEnumerable<PhotoAccessoire>>> GetAllAsync(int page = 0)
    {
        return await _cache.GetOrCreateAsync("photo_accessoire:all/" + page,
            async () => await upwaysDbContext.Photoaccessoires.Skip(page * PAGE_SIZE).Take(PAGE_SIZE).ToListAsync());
    }

    public async Task<ActionResult<PhotoAccessoire>> GetByIdAsync(int id)
    {
        return await _cache.GetOrCreateAsync("photo_accessoire:" + id,
            async () => await upwaysDbContext.Photoaccessoires.FirstOrDefaultAsync(p => p.PhotoAcessoireId == id));
    }

    public async Task<ActionResult<PhotoAccessoire>> GetByStringAsync(string str)
    {
        throw new NotImplementedException();
    }

    public async Task<ActionResult<int>> GetCountAsync()
    {
        return await _cache.GetOrCreateAsync("photo_accessoire:count",
            async () => await upwaysDbContext.Photoaccessoires.CountAsync());
    }

    public async Task UpdateAsync(PhotoAccessoire phoAcsToUpdate, PhotoAccessoire phoAcs)
    {
        upwaysDbContext.Entry(phoAcsToUpdate).State = EntityState.Modified;
        phoAcsToUpdate.PhotoAcessoireId = phoAcs.PhotoAcessoireId;
        phoAcsToUpdate.AccessoireId = phoAcs.AccessoireId;
        phoAcsToUpdate.UrlPhotoAccessoire = phoAcs.UrlPhotoAccessoire;
        await upwaysDbContext.SaveChangesAsync();
    }
}