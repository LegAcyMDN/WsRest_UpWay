using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WsRest_UpWay.Models.EntityFramework;
using WsRest_UpWay.Models.Repository;

namespace WsRest_UpWay.Models.DataManager
{
    public class VeloManager : IDataVelo<Velo>
    {
        readonly S215UpWayContext _upWayContext;

        public VeloManager() { }

        public VeloManager(S215UpWayContext context)
        {
            _upWayContext = context;
        }

        public async Task AddAsync(Velo entity)
        {
            await _upWayContext.Velos.AddAsync(entity);
            await _upWayContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Velo vel)
        {
            _upWayContext.Velos.Remove(vel);
            await _upWayContext.SaveChangesAsync();
        }

        public async Task<ActionResult<IEnumerable<Velo>>> GetByFiltresAsync(string taille, int categorie, int cara, int marque, int annee, int kilomin, int kilomax, string posmot, string motmar, int couplemot, int capbat, string posbat, string batamo, string posbag, int poids)
        {
            IQueryable<Velo> velofilt = _upWayContext.Velos;
            if (taille != null) 
            {
                velofilt = velofilt.Where(p => p.TailleMin == taille);
            }
            if (categorie != null) 
            {
                velofilt = velofilt.Where(p => p.CategorieId == categorie);
            }
            return await velofilt.ToListAsync();
        }

        public async Task<ActionResult<IEnumerable<Velo>>> GetAllAsync()
        {
            return await _upWayContext.Velos.ToListAsync();
        }

        public async Task<ActionResult<Velo>> GetByIdAsync(int id)
        {
            return await _upWayContext.Velos.FirstOrDefaultAsync(p => p.VeloId == id);
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
}
