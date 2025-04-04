using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using WsRest_UpWay.Models.EntityFramework;
using WsRest_UpWay.Models.Repository;

namespace WsRest_UpWay.Models.DataManager;

public class AccessoireManager : IDataAccessoire
{
    public const int PAGE_SIZE = 20;
    private readonly IMemoryCache cache;
    private readonly IConfiguration configuration;

    private readonly S215UpWayContext? upwaysDbContext;

    public AccessoireManager()
    {
    }

    public AccessoireManager(S215UpWayContext? upwaysDbContext, IMemoryCache cache, IConfiguration configuration)
    {
        this.upwaysDbContext = upwaysDbContext;
        this.cache = cache;
        this.configuration = configuration;
    }

    public async Task<ActionResult<int>> GetCountAsync()
    {
        var count = await cache.GetOrCreateAsync("accessoires:count", async entry =>
        {
            var count = await upwaysDbContext.Accessoires.CountAsync();

            entry.SetSlidingExpiration(TimeUtils.PrettyParse(configuration["CACHE_SLIDING_EXPIRATION"]))
                .SetAbsoluteExpiration(TimeUtils.PrettyParse(configuration["CACHE_ABSOLUTE_EXPIRATION"]))
                .SetSize(4);

            return count;
        });

        return count;
    }

    public async Task<ActionResult<Accessoire>> GetByIdAsync(int id)
    {
        var accessoire = await cache.GetOrCreateAsync("accessoires:" + id, async entry =>
        {
            var accessoire = await upwaysDbContext.Accessoires.FirstOrDefaultAsync(u => u.AccessoireId == id);

            entry.SetSlidingExpiration(TimeUtils.PrettyParse(configuration["CACHE_SLIDING_EXPIRATION"]))
                .SetAbsoluteExpiration(TimeUtils.PrettyParse(configuration["CACHE_ABSOLUTE_EXPIRATION"]))
                .SetSize(Accessoire.APROXIMATE_SIZE + (accessoire.NomAccessoire ?? "").Length +
                         (accessoire.DescriptionAccessoire ?? "").Length);

            return accessoire;
        });

        return accessoire;
    }

    public async Task<ActionResult<IEnumerable<PhotoAccessoire>>> GetPhotosByIdAsync(int id)
    {
        var photos = await cache.GetOrCreateAsync("accessoires/photos:" + id, async entry =>
        {
            var photos = await upwaysDbContext.Photoaccessoires.Where(u => u.AccessoireId == id).ToListAsync();

            entry.SetSlidingExpiration(TimeUtils.PrettyParse(configuration["CACHE_SLIDING_EXPIRATION"]))
                .SetAbsoluteExpiration(TimeUtils.PrettyParse(configuration["CACHE_ABSOLUTE_EXPIRATION"]))
                .SetSize(photos.Sum(p => PhotoAccessoire.APROXIMATE_SIZE + (p.UrlPhotoAccessoire ?? "").Length));

            return photos;
        });

        return photos;
    }

    public async Task<ActionResult<Accessoire>> GetByStringAsync(string nom)
    {
        var id = await cache.GetOrCreateAsync("accessoires:" + nom.ToLower(), async entry =>
        {
            var accessoire = await upwaysDbContext.Accessoires.FirstOrDefaultAsync(u =>
                u.NomAccessoire.ToLower().Equals(nom.ToLower()));

            entry.SetSlidingExpiration(TimeUtils.PrettyParse(configuration["CACHE_SLIDING_EXPIRATION"]))
                .SetAbsoluteExpiration(TimeUtils.PrettyParse(configuration["CACHE_ABSOLUTE_EXPIRATION"]))
                .SetSize(4);

            return accessoire == null ? -1 : accessoire.AccessoireId;
        });

        if (id == -1) return null;
        return await GetByIdAsync(id);
    }

    public async Task<ActionResult<IEnumerable<Accessoire>>> GetByCategoryPrixAsync(int? categoryId, int min, int max,
        int page = 0)
    {
        var accessoires = await cache.GetOrCreateAsync(
            "accessoires/filtered:" + categoryId + "/" + min + "/" + max + "/" + page, async entry =>
            {
                IQueryable<Accessoire> accessoirefilt = upwaysDbContext.Accessoires;
                if (categoryId != null) accessoirefilt = accessoirefilt.Where(p => p.CategorieId == categoryId);

                var accessoires = accessoirefilt.Where(a => a.PrixAccessoire < max && a.PrixAccessoire > min)
                    .Skip(page * PAGE_SIZE)
                    .Take(PAGE_SIZE).ToList();

                entry.SetSlidingExpiration(TimeUtils.PrettyParse(configuration["CACHE_SLIDING_EXPIRATION"]))
                    .SetAbsoluteExpiration(TimeUtils.PrettyParse(configuration["CACHE_ABSOLUTE_EXPIRATION"]))
                    .SetSize(accessoires.Sum(p =>
                        Accessoire.APROXIMATE_SIZE + (p.NomAccessoire ?? "").Length +
                        (p.DescriptionAccessoire ?? "").Length));

                return accessoires;
            });

        return accessoires;
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
        var accessoires = await cache.GetOrCreateAsync("accessoires/all:" + page, async entry =>
        {
            var accessoires = await upwaysDbContext.Accessoires.Skip(page * PAGE_SIZE).Take(PAGE_SIZE).ToListAsync();

            entry.SetSlidingExpiration(TimeUtils.PrettyParse(configuration["CACHE_SLIDING_EXPIRATION"]))
                .SetAbsoluteExpiration(TimeUtils.PrettyParse(configuration["CACHE_ABSOLUTE_EXPIRATION"]))
                .SetSize(accessoires.Sum(p =>
                    Accessoire.APROXIMATE_SIZE + (p.NomAccessoire ?? "").Length +
                    (p.DescriptionAccessoire ?? "").Length));

            return accessoires;
        });

        return accessoires;
    }
}