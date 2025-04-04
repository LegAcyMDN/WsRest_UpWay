using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using WsRest_UpWay.Models.EntityFramework;
using WsRest_UpWay.Models.Repository;

namespace WsRest_UpWay.Models.DataManager;

public class VeloManager : IDataVelo
{
    public const int PAGE_SIZE = 20;
    private readonly IMemoryCache _cache;
    private readonly IConfiguration _configuration;
    private readonly S215UpWayContext _upWayContext;

    public VeloManager()
    {
    }

    public VeloManager(S215UpWayContext context, IMemoryCache cache, IConfiguration configuration)
    {
        _upWayContext = context;
        _cache = cache;
        _configuration = configuration;
    }

    public async Task AddAsync(Velo entity)
    {
        await _upWayContext.Velos.AddAsync(entity);
        await _upWayContext.SaveChangesAsync();
    }

    public async Task<ActionResult<Velo>> GetByStringAsync(string nom)
    {
        var id = await _cache.GetOrCreateAsync("velos:" + nom.ToLower(), async entry =>
        {
            var velo = await _upWayContext.Velos.FirstOrDefaultAsync(u =>
                u.NomVelo.ToLower().Equals(nom.ToLower()));

            entry.SetSlidingExpiration(TimeUtils.PrettyParse(_configuration["CACHE_SLIDING_EXPIRATION"]))
                .SetAbsoluteExpiration(TimeUtils.PrettyParse(_configuration["CACHE_ABSOLUTE_EXPIRATION"]))
                .SetSize(4);

            return velo == null ? -1 : velo.VeloId;
        });

        if (id == -1) return null;
        return await GetByIdAsync(id);
    }

    public async Task DeleteAsync(Velo vel)
    {
        _upWayContext.Velos.Remove(vel);
        await _upWayContext.SaveChangesAsync();
    }

    public async Task<ActionResult<IEnumerable<Velo>>> GetByFiltresAsync(string? taille, int? categorie, int? cara,
        int? marque,
        int? annee, string? kilom, string? posmot, string? motmar, string? couplemot, string? capbat, string? posbat,
        string? batamo, string? posbag, decimal? poids, int page = 0)
    {
        var velos = await _cache.GetOrCreateAsync(string.Format(
            "velos/filtered:{0}/{1}/{2}/{3}/{4}/{5}/{6}/{7}/{8}/{9}/{10}/{11}/{12}/{13}/{14}", taille ?? "null",
            categorie.ToString() ?? "null", cara.ToString() ?? "null", marque.ToString() ?? "null",
            annee.ToString() ?? "null", kilom ?? "null", posmot ?? "null", motmar ?? "null", couplemot ?? "null",
            capbat ?? "null", posbat ?? "null", batamo ?? "null", posbag ?? "null", poids.ToString() ?? "null",
            page.ToString() ?? "null"), async entry =>
        {
            IQueryable<Velo> velofilt = _upWayContext.Velos;
            if (taille != null) velofilt = velofilt.Where(p => p.TailleMin.ToUpper().Equals(taille.ToUpper()));
            if (categorie != null) velofilt = velofilt.Where(p => p.CategorieId.Equals(categorie));
            if (cara != null) velofilt = velofilt.Where(p => p.CaracteristiqueVeloId == cara);
            if (marque != null) velofilt = velofilt.Where(p => p.MarqueId == marque);
            if (annee != null) velofilt = velofilt.Where(p => p.AnneeVelo >= annee);
            if (kilom != null) velofilt = velofilt.Where(p => p.NombreKms.ToUpper().Equals(kilom.ToUpper()));
            if (posmot != null) velofilt = velofilt.Where(p => p.PositionMoteur.ToUpper().Equals(posmot.ToUpper()));
            if (motmar != null)
            {
                var mot = await _upWayContext.Moteurs.FirstOrDefaultAsync(m =>
                    m.MoteurMarque.NomMarque.ToUpper().Equals(motmar.ToUpper()));
                velofilt = velofilt.Where(p => p.MoteurId == mot.MoteurId);
            }

            if (couplemot != null)
            {
                var mot = await _upWayContext.Moteurs.FirstOrDefaultAsync(cp =>
                    cp.CoupleMoteur.ToUpper().Equals(couplemot.ToUpper()));
                velofilt = velofilt.Where(p => p.MoteurId == mot.MoteurId);
            }

            if (capbat != null)
            {
                var cat = await _upWayContext.Caracteristiques.FirstOrDefaultAsync(c =>
                    c.LibelleCaracteristique.ToUpper().Equals(capbat.ToUpper()));
                velofilt = velofilt.Where(p => p.CapaciteBatterie == cat.LibelleCaracteristique);
            }

            if (posbat != null)
            {
            }

            if (poids != null)
            {
                var catv = await _upWayContext.Caracteristiquevelos.FirstOrDefaultAsync(c => c.Poids == poids);
                velofilt = velofilt.Where(p => p.CaracteristiqueVeloId == catv.CaracteristiqueVeloId);
            }

            var velos = await velofilt.Skip(page * PAGE_SIZE).Take(PAGE_SIZE).ToListAsync();

            entry.SetSlidingExpiration(TimeUtils.PrettyParse(_configuration["CACHE_SLIDING_EXPIRATION"]))
                .SetAbsoluteExpiration(TimeUtils.PrettyParse(_configuration["CACHE_ABSOLUTE_EXPIRATION"]))
                .SetSize(velos.Sum(GetAproximateSize));

            return velos;
        });

        return velos;
    }

    public async Task<ActionResult<IEnumerable<Velo>>> GetAllAsync(int page)
    {
        var velos = await _cache.GetOrCreateAsync("velos/all:" + page, async entry =>
        {
            var velos = await _upWayContext.Velos.Skip(page * PAGE_SIZE).Take(PAGE_SIZE).ToListAsync();

            entry.SetSlidingExpiration(TimeUtils.PrettyParse(_configuration["CACHE_SLIDING_EXPIRATION"]))
                .SetAbsoluteExpiration(TimeUtils.PrettyParse(_configuration["CACHE_ABSOLUTE_EXPIRATION"]))
                .SetSize(velos.Sum(GetAproximateSize));

            return velos;
        });

        return velos;
    }

    public async Task<ActionResult<int>> GetCountAsync()
    {
        var count = await _cache.GetOrCreateAsync("velos:count", async entry =>
        {
            var count = await _upWayContext.Accessoires.CountAsync();

            entry.SetSlidingExpiration(TimeUtils.PrettyParse(_configuration["CACHE_SLIDING_EXPIRATION"]))
                .SetAbsoluteExpiration(TimeUtils.PrettyParse(_configuration["CACHE_ABSOLUTE_EXPIRATION"]))
                .SetSize(4);

            return count;
        });

        return count;
    }

    public async Task<ActionResult<Velo>> GetByIdAsync(int id)
    {
        var velo = await _cache.GetOrCreateAsync("velos:" + id, async entry =>
        {
            var velo = await _upWayContext.Velos.FindAsync(id);

            entry.SetSlidingExpiration(TimeUtils.PrettyParse(_configuration["CACHE_SLIDING_EXPIRATION"]))
                .SetAbsoluteExpiration(TimeUtils.PrettyParse(_configuration["CACHE_ABSOLUTE_EXPIRATION"]))
                .SetSize(4);

            return velo;
        });

        return velo;
    }

    public async Task<ActionResult<IEnumerable<PhotoVelo>>> GetPhotosByIdAsync(int id)
    {
        var photos = await _cache.GetOrCreateAsync("velos/photos:" + id, async entry =>
        {
            var photos = await _upWayContext.Photovelos.Where(u => u.VeloId == id).ToListAsync();

            entry.SetSlidingExpiration(TimeUtils.PrettyParse(_configuration["CACHE_SLIDING_EXPIRATION"]))
                .SetAbsoluteExpiration(TimeUtils.PrettyParse(_configuration["CACHE_ABSOLUTE_EXPIRATION"]))
                .SetSize(photos.Sum(p => PhotoVelo.APROXIMATE_SIZE + (p.UrlPhotoVelo ?? "").Length));

            return photos;
        });

        return photos;
    }

    public async Task<ActionResult<IEnumerable<MentionVelo>>> GetMentionByIdAsync(int id)
    {
        var mentions = await _cache.GetOrCreateAsync("velos/mentions:" + id, async entry =>
        {
            var mentions = await _upWayContext.Mentionvelos.Where(u => u.VeloId == id).ToListAsync();

            entry.SetSlidingExpiration(TimeUtils.PrettyParse(_configuration["CACHE_SLIDING_EXPIRATION"]))
                .SetAbsoluteExpiration(TimeUtils.PrettyParse(_configuration["CACHE_ABSOLUTE_EXPIRATION"]))
                .SetSize(mentions.Sum(p =>
                    PhotoVelo.APROXIMATE_SIZE + (p.LibelleMention ?? "").Length + (p.ValeurMention ?? "").Length));

            return mentions;
        });

        return mentions;
    }

    public async Task UpdateAsync(Velo vel, Velo entity)
    {
        _upWayContext.Entry(vel).State = EntityState.Modified;
        vel.VeloId = entity.VeloId;
        vel.MarqueId = entity.MarqueId;
        vel.CategorieId = entity.CategorieId;
        vel.MoteurId = entity.MoteurId;
        vel.CaracteristiqueVeloId = entity.CaracteristiqueVeloId;
        vel.NomVelo = entity.NomVelo;
        vel.AnneeVelo = entity.AnneeVelo;
        vel.TailleMin = entity.TailleMax;
        vel.TailleMax = entity.TailleMax;
        vel.NombreKms = entity.NombreKms;
        vel.PrixRemise = entity.PrixRemise;
        vel.PrixNeuf = entity.PrixNeuf;
        vel.PourcentageReduction = entity.PourcentageReduction;
        vel.DescriptifVelo = entity.DescriptifVelo;
        vel.QuantiteVelo = entity.QuantiteVelo;
        vel.PositionMoteur = entity.PositionMoteur;
        vel.CapaciteBatterie = entity.CapaciteBatterie;
        await _upWayContext.SaveChangesAsync();
    }

    private long GetAproximateSize(Velo velo)
    {
        var size = Velo.APROXIMATE_SIZE;
        if (velo.NomVelo != null) size += velo.NomVelo.Length;
        if (velo.TailleMin != null) size += velo.TailleMin.Length;
        if (velo.TailleMax != null) size += velo.TailleMax.Length;
        if (velo.NombreKms != null) size += velo.NombreKms.Length;
        if (velo.DescriptifVelo != null) size += velo.DescriptifVelo.Length;
        if (velo.PositionMoteur != null) size += velo.PositionMoteur.Length;
        if (velo.CapaciteBatterie != null) size += velo.CapaciteBatterie.Length;

        return size;
    }
}