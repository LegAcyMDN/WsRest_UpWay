using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Printing;
using WsRest_UpWay.Models.EntityFramework;
using WsRest_UpWay.Models.Repository;

namespace WsRest_UpWay.Models.DataManager
{
    public class EstRealiseManager : IDataEstRealise
    {
        private readonly S215UpWayContext upwaysDbContext;

        public EstRealiseManager()
        {
        }

        public EstRealiseManager(S215UpWayContext context)
        {
            upwaysDbContext = context;
        }

        public async Task AddAsync(EstRealise estRealise)
        {
            await upwaysDbContext.Estrealises.AddAsync(estRealise);
            await upwaysDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(EstRealise estRealise)
        {
            upwaysDbContext.Estrealises.Remove(estRealise);
            await upwaysDbContext.SaveChangesAsync();
        }

        public async Task<ActionResult<IEnumerable<EstRealise>>> GetAllAsync(int page = 0)
        {
            return await upwaysDbContext.Estrealises.ToListAsync();
        }

        public async Task<ActionResult<EstRealise>> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ActionResult<EstRealise>> GetByIdsAsync(int idvelo, int idinspection, int idreparation)
        {
            return await upwaysDbContext.Estrealises.FindAsync(idvelo ,idinspection, idreparation);
        }

        public async Task<ActionResult<IEnumerable<EstRealise>>> GetByIdVeloAsync(int id, string type)
        {
            return await upwaysDbContext.Estrealises.Where(u => u.VeloId == id && u.EstRealiseRapportInspection.TypeInspection == type).Include(v => v.EstRealiseRapportInspection).Include(v => v.EstRealiseReparationVelo).ToListAsync();
        }

        public async Task<ActionResult<EstRealise>> GetByStringAsync(string str)
        {
            throw new NotImplementedException();
        }

        public async Task<ActionResult<int>> GetCountAsync()
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(EstRealise estrealiseToUpdate, EstRealise estRealise)
        {
            upwaysDbContext.Entry(estrealiseToUpdate).State = EntityState.Modified;
            estrealiseToUpdate.VeloId = estRealise.VeloId;
            estrealiseToUpdate.InspectionId = estRealise.InspectionId;
            estrealiseToUpdate.ReparationId = estRealise.ReparationId;
            estrealiseToUpdate.DateInspection = estRealise.DateInspection;
            estrealiseToUpdate.CommentaireInspection = estRealise.CommentaireInspection;
            estrealiseToUpdate.HistoriqueInspection = estRealise.HistoriqueInspection;
            upwaysDbContext.SaveChangesAsync();
        }
    }
}
