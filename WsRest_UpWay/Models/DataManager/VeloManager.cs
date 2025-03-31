using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WsRest_UpWay.Models.EntityFramework;
using WsRest_UpWay.Models.Repository;

namespace WsRest_UpWay.Models.DataManager;

public class VeloManager : IDataVelo
{
    public const int PAGE_SIZE = 20;
    private readonly S215UpWayContext _upWayContext;

    public VeloManager()
    {
    }

    public VeloManager(S215UpWayContext context)
    {
        _upWayContext = context;
    }

    public async Task AddAsync(Velo entity)
    {
        await _upWayContext.Velos.AddAsync(entity);
        await _upWayContext.SaveChangesAsync();
    }

    public async Task<ActionResult<Velo>> GetByStringAsync(string nom)
    {
        return await _upWayContext.Velos.FirstOrDefaultAsync(p => p.NomVelo.ToUpper() == nom.ToUpper());
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
        IQueryable<Velo> velofilt = _upWayContext.Velos;
        if (taille != null) velofilt = velofilt.Where(p => p.TailleMin.ToUpper() == taille.ToUpper());
        if (categorie != null) velofilt = velofilt.Where(p => p.CategorieId == categorie);
        if (cara != null) velofilt = velofilt.Where(p => p.CaracteristiqueVeloId == cara);
        if (marque != null) velofilt = velofilt.Where(p => p.MarqueId == marque);
        if (annee != null) velofilt = velofilt.Where(p => p.AnneeVelo >= annee);
        if (kilom != null) velofilt = velofilt.Where(p => p.NombreKms.ToUpper() == kilom.ToUpper());
        if (posmot != null) velofilt = velofilt.Where(p => p.PositionMoteur.ToUpper() == posmot.ToUpper());
        if (motmar != null)
        {
            var mot = await _upWayContext.Moteurs.FirstOrDefaultAsync(m =>
                m.MoteurMarque.NomMarque.ToUpper() == motmar.ToUpper());
            velofilt = velofilt.Where(p => p.MoteurId == mot.MoteurId);
        }

        if (couplemot != null)
        {
            var mot = await _upWayContext.Moteurs.FirstOrDefaultAsync(cp =>
                cp.CoupleMoteur.ToUpper() == couplemot.ToUpper());
            velofilt = velofilt.Where(p => p.MoteurId == mot.MoteurId);
        }

        if (capbat != null)
        {
            var cat = await _upWayContext.Caracteristiques.FirstOrDefaultAsync(c =>
                c.LibelleCaracteristique.ToUpper() == capbat.ToUpper());
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

        return await velofilt.Skip(page * PAGE_SIZE).Take(PAGE_SIZE).ToListAsync();
    }

    public async Task<ActionResult<IEnumerable<Velo>>> GetAllAsync(int page)
    {
        return await _upWayContext.Velos.Skip(page * PAGE_SIZE).Take(PAGE_SIZE).ToListAsync();
    }

    public async Task<ActionResult<int>> GetCountAsync()
    {
        return await _upWayContext.Velos.CountAsync();
    }

    public async Task<ActionResult<Velo>> GetByIdAsync(int id)
    {
        return await _upWayContext.Velos.FirstOrDefaultAsync(p => p.VeloId == id);
    }

    public async Task<ActionResult<IEnumerable<PhotoVelo>>> GetPhotosByIdAsync(int id)
    {
        return await _upWayContext.Photovelos.Where(p => p.VeloId == id).ToListAsync();
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
}