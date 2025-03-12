using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WsRest_UpWay.Models.EntityFramework;
using WsRest_UpWay.Models.Repository;

namespace WsRest_UpWay.Models.DataManager
{
    public class VeloManager : IDataRepository<Velo>
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

        public async Task<ActionResult<IEnumerable<Velo>>> GetAllAsync()
        {
            return await _upWayContext.Velos.ToListAsync();
        }

        public async Task<ActionResult<Velo>> GetByIdAsync(int id)
        {
            return await _upWayContext.Velos.FirstOrDefaultAsync(p => p.Idvelo == id);
        }

        public async Task<ActionResult<Velo>> GetByStringAsync(string str)
        {
            return await _upWayContext.Velos.FirstOrDefaultAsync(p => p.Nomvelo.ToUpper() == str.ToLower());
        }

        public async Task UpdateAsync(Velo vel, Velo entity)
        {
            _upWayContext.Entry(vel).State = EntityState.Modified;
            vel.Idvelo = entity.Idvelo;
            vel.Idmarque = entity.Idmarque;
            vel.Idcategorie = entity.Idcategorie;
            vel.Idmoteur = entity.Idmoteur;
            vel.Idcaracteristiquevelo = entity.Idcaracteristiquevelo;
            vel.Nomvelo = entity.Nomvelo;
            vel.Anneevelo = entity.Anneevelo;
            vel.Taillemin = entity.Taillemax;
            vel.Taillemax = entity.Taillemax;
            vel.Nombrekms = entity.Nombrekms;
            vel.Prixremise = entity.Prixremise;
            vel.Prixneuf = entity.Prixneuf;
            vel.Pourcentagereduction = entity.Pourcentagereduction;
            vel.Descriptifvelo = entity.Descriptifvelo;
            vel.Quantitevelo = entity.Quantitevelo;
            vel.Positionmoteur = entity.Positionmoteur;
            vel.Capacitebatterie = entity.Capacitebatterie;
            await _upWayContext.SaveChangesAsync();
        }
    }
}
