using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Printing;
using WsRest_UpWay.Models.EntityFramework;
using WsRest_UpWay.Models.Repository;

namespace WsRest_UpWay.Models.DataManager
{
    public class AdresseExpeditionManager : IDataRepository<AdresseExpedition>
    {
        public const int PAGE_SIZE = 20;
        private readonly S215UpWayContext upwaysDbContext;

        public AdresseExpeditionManager() { }

        public AdresseExpeditionManager(S215UpWayContext context)
        {
            upwaysDbContext = context;
        }

        public async Task AddAsync(AdresseExpedition adr_exp)
        {
            await upwaysDbContext.Adresseexpeditions.AddAsync(adr_exp);
            await upwaysDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(AdresseExpedition adr_exp)
        {
            upwaysDbContext.Adresseexpeditions.Remove(adr_exp);
            await upwaysDbContext.SaveChangesAsync();
        }

        public async Task<ActionResult<IEnumerable<AdresseExpedition>>> GetAllAsync(int page = 0)
        {
            return await upwaysDbContext.Adresseexpeditions.Skip(page * PAGE_SIZE).Take(PAGE_SIZE).ToListAsync();
        }

        public async Task<ActionResult<AdresseExpedition>> GetByIdAsync(int id)
        {
            return await upwaysDbContext.Adresseexpeditions.FirstOrDefaultAsync(a => a.AdresseExpeId == id);
        }

        public async Task<ActionResult<AdresseExpedition>> GetByStringAsync(string str)
        {
            throw new NotImplementedException();
        }

        public async Task<ActionResult<int>> GetCountAsync()
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(AdresseExpedition adrExpToUpdate, AdresseExpedition adr_exp)
        {
            upwaysDbContext.Entry(adrExpToUpdate).State = EntityState.Modified;
            adrExpToUpdate.AdresseExpeId = adr_exp.AdresseExpeId;
            adrExpToUpdate.ClientId = adr_exp.ClientId;
            adrExpToUpdate.AdresseFactId = adr_exp.AdresseFactId;
            adrExpToUpdate.PaysExpedition = adr_exp.PaysExpedition;
            adrExpToUpdate.BatimentExpeditionOpt = adr_exp.BatimentExpeditionOpt;
            adrExpToUpdate.RueExpedition = adr_exp.RueExpedition;
            adrExpToUpdate.CPExpedition = adr_exp.CPExpedition;
            adrExpToUpdate.RegionExpedition = adr_exp.RegionExpedition;
            adrExpToUpdate.VilleExpedition = adr_exp.VilleExpedition;
            adrExpToUpdate.TelephoneExpedition = adr_exp.TelephoneExpedition;
            adrExpToUpdate.DonneesSauvegardees = adr_exp.DonneesSauvegardees;
            await upwaysDbContext.SaveChangesAsync();
        }
    }
}
