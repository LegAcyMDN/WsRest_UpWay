using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WsRest_UpWay.Models.EntityFramework;

namespace WsRest_UpWay.Models.DataManager
{
    public class InformationManager
    {
        readonly S215UpWayContext? upwaysDbContext;

        public InformationManager() { }

        public InformationManager(S215UpWayContext context)
        {
            upwaysDbContext = context;
        }

        public async Task<ActionResult<IEnumerable<Information>>> GetAllAsync()
        {
            return await upwaysDbContext.Informations.ToListAsync();
        }

        public async Task<ActionResult<Information>> GetByIdAsync(int id)
        {
            return await upwaysDbContext.Informations.FirstOrDefaultAsync(u => u.InformationId == id);
        }

        public async Task<ActionResult<Information>> GetByStringAsync(string mode)
        {
            return await upwaysDbContext.Informations.FirstOrDefaultAsync(u => u.ModeLivraison.ToLower() == mode.ToUpper());
        }

        public async Task AddAsync(Information entity)
        {
            await upwaysDbContext.Informations.AddAsync(entity);
            await upwaysDbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Information inf, Information entity)
        {
            upwaysDbContext.Entry(inf).State = EntityState.Modified;
            inf.InformationId = entity.InformationId;
            inf.ReductionId = entity.ReductionId;
            inf.RetraitMagasinId = entity.RetraitMagasinId;
            inf.AdresseExpeId = entity.AdresseExpeId;
            inf.PanierId = entity.PanierId;
            inf.ContactInformations = entity.ContactInformations;
            inf.OffreEmail = entity.OffreEmail;
            inf.ModeLivraison = entity.ModeLivraison;
            inf.InformationPays = entity.InformationPays;
            inf.InformationRue = entity.InformationRue;
            await upwaysDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Information inf)
        {
            upwaysDbContext.Informations.Remove(inf);
            await upwaysDbContext.SaveChangesAsync();
        }
    }
}
