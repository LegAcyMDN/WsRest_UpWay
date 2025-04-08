using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WsRest_UpWay.Models.Cache;
using WsRest_UpWay.Models.EntityFramework;
using WsRest_UpWay.Models.Repository;

namespace WsRest_UpWay.Models.DataManager;

public class VeloManager : IDataVelo
{
    public const int PAGE_SIZE = 20;
    private readonly ICache _cache;
    private readonly S215UpWayContext _upWayContext;

    public VeloManager(S215UpWayContext context, ICache cache)
    {
        _upWayContext = context;
        _cache = cache;
    }

    public async Task AddAsync(Velo entity)
    {
        await _upWayContext.Velos.AddAsync(entity);
        await _upWayContext.SaveChangesAsync();
    }

    public async Task<ActionResult<Velo>> GetByStringAsync(string nom)
    {
        var id = await _cache.GetOrCreateAsync("velos:" + nom.ToLower(), async () =>
        {
            var velo = await _upWayContext.Velos.FirstOrDefaultAsync(u =>
                u.NomVelo.ToLower().Equals(nom.ToLower()));

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

    public async Task<ActionResult<IEnumerable<Velo>>> GetByFiltresAsync(
        string? taille, int? categorie, int? cara, int? marque, int? annee,
        int? kilomMin, int? kilomMax, string? posmot, int? motmar,
        string? couplemot, string? capbat, decimal? poids,
        decimal? prixMin, decimal? prixMax, int page = 0)
    {
        return await _cache.GetOrCreateAsync(string.Format(
            "velos/filtered:{0}/{1}/{2}/{3}/{4}/{5}/{6}/{7}/{8}/{9}/{10}/{11}/{12}/{13}/{14}/{15}", taille ?? "null",
            categorie.ToString() ?? "null", cara.ToString() ?? "null", marque.ToString() ?? "null",
            annee.ToString() ?? "null", kilomMin.ToString() ?? "null", kilomMax.ToString() ?? "null", posmot ?? "null", motmar.ToString() ?? "null", couplemot ?? "null",
            capbat ?? "null", poids.ToString() ?? "null",
            prixMin.ToString() ?? "null", prixMax.ToString() ?? "null", page.ToString() ?? "null"), async () =>
        {
            IQueryable<Velo> velofilt = _upWayContext.Velos;
            if (taille != null) velofilt = velofilt.Where(p => p.TailleMin.ToUpper().Equals(taille.ToUpper()));
            if (categorie != null) velofilt = velofilt.Where(p => p.CategorieId.Equals(categorie));
            if (cara != null) velofilt = velofilt.Where(p => p.CaracteristiqueVeloId == cara);
            if (marque != null) velofilt = velofilt.Where(p => p.MarqueId == marque);
            if (annee != null) velofilt = velofilt.Where(p => p.AnneeVelo >= annee);
            if (prixMin != null) velofilt = velofilt.Where(p => p.PrixNeuf >= prixMin);
            if (prixMax != null) velofilt = velofilt.Where(p => p.PrixNeuf <= prixMax);
            if (kilomMin != null)
            {
                int nombreKmsInt;
                velofilt = velofilt.Where(p =>
                    int.TryParse(p.NombreKms.Substring(0, p.NombreKms.Length - 4), out nombreKmsInt) &&
                    nombreKmsInt <= kilomMin);
            }
            if (kilomMax != null)
            {
                int nombreKmsInt;
                velofilt = velofilt.Where(p =>
                    int.TryParse(p.NombreKms.Substring(0, p.NombreKms.Length - 4), out nombreKmsInt) &&
                    nombreKmsInt <= kilomMax);
            }
            if (posmot != null) velofilt = velofilt.Where(p => p.PositionMoteur.ToUpper().Equals(posmot.ToUpper()));
            if (motmar != null) velofilt = velofilt.Where(p => p.MoteurId.Equals(motmar));
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
            if (poids != null)
            {
                var catv = await _upWayContext.Caracteristiquevelos.FirstOrDefaultAsync(c => c.Poids == poids);
                velofilt = velofilt.Where(p => p.CaracteristiqueVeloId == catv.CaracteristiqueVeloId);
            }

            return await velofilt.Skip(page * PAGE_SIZE).Take(PAGE_SIZE).ToListAsync();
            ;
        });
    }

    public async Task<ActionResult<IEnumerable<Velo>>> GetAllAsync(int page)
    {
        return await _cache.GetOrCreateAsync("velos/all:" + page, async () =>
        {
            return await _upWayContext.Velos.Skip(page * PAGE_SIZE).Take(PAGE_SIZE).ToListAsync();
            ;
        });
    }

    public async Task<ActionResult<int>> GetCountAsync()
    {
        return await _cache.GetOrCreateAsync("velos:count", async () =>
        {
            return await _upWayContext.Accessoires.CountAsync();
            ;
        });
    }

    public async Task<ActionResult<Velo>> GetByIdAsync(int id)
    {
        return await _cache.GetOrCreateAsync("velos:" + id,
            async () => { return await _upWayContext.Velos.FindAsync(id); });
    }

    public async Task<ActionResult<IEnumerable<PhotoVelo>>> GetPhotosByIdAsync(int id)
    {
        return await _cache.GetOrCreateAsync("velos/photos:" + id, async () =>
        {
            return await _upWayContext.Photovelos.Where(u => u.VeloId == id).ToListAsync();
            ;
        });
    }

    public async Task<ActionResult<IEnumerable<MentionVelo>>> GetMentionByIdAsync(int id)
    {
        return await _cache.GetOrCreateAsync("velos/mentions:" + id, async () =>
        {
            return await _upWayContext.Mentionvelos.Where(u => u.VeloId == id).ToListAsync();
            ;
        });
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

    public async Task<ActionResult<IEnumerable<Caracteristique>>> GetCaracteristiquesVeloAsync(int id)
    {
        return await _cache.GetOrCreateAsync("velos/caracteristiques:" + id, async () =>
        {
            var velo = await _upWayContext.Velos
                .Include(v => v.ListeCaracteristiques)
                .FirstOrDefaultAsync(v => v.VeloId == id);

            if (velo == null) return new List<Caracteristique>();

            return velo.ListeCaracteristiques.ToList();
        });
    }
}