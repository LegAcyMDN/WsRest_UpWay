using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WsRest_UpWay.Models.EntityFramework;
using WsRest_UpWay.Models.Repository;

namespace WsRest_UpWay.Models.DataManager
{
    public class DetailCommandeManager : IDataRepository<DetailCommande>
    {
        readonly S215UpWayContext? upwaysDbContext;

        public DetailCommandeManager() { }
        public DetailCommandeManager(S215UpWayContext context)
        {
            upwaysDbContext = context;
        }
        public async Task<ActionResult<IEnumerable<DetailCommande>>> GetAllAsync()
        {
            return await upwaysDbContext.Detailcommandes.ToListAsync();
        }
        public async Task<ActionResult<DetailCommande>> GetByIdAsync(int id)
        {
            return await upwaysDbContext.Detailcommandes.FirstOrDefaultAsync(u => u.CommandeId == id);
        }
        public async Task<ActionResult<DetailCommande>> GetByStringAsync(string nom)
        {
            return await upwaysDbContext.Detailcommandes.FirstOrDefaultAsync(u => u.ModeExpedition.ToUpper() == nom.ToUpper());
        }
        public async Task AddAsync(DetailCommande detcom)
        {
            await upwaysDbContext.Detailcommandes.AddAsync(detcom);
            await upwaysDbContext.SaveChangesAsync();
        }
        public async Task UpdateAsync(DetailCommande detcomToUpdate, DetailCommande detcom)
        {
            upwaysDbContext.Entry(detcomToUpdate).State = EntityState.Modified;
            detcomToUpdate.CommandeId = detcom.CommandeId;
            detcomToUpdate.RetraitMagasinId = detcom.RetraitMagasinId;
            detcomToUpdate.AdresseFactId = detcom.AdresseFactId;
            detcomToUpdate.EtatCommandeId = detcom.EtatCommandeId;
            detcomToUpdate.ClientId = detcom.ClientId;
            detcomToUpdate.PanierId = detcom.PanierId;
            detcomToUpdate.MoyenPaiement = detcom.MoyenPaiement;
            detcomToUpdate.ModeExpedition = detcom.ModeExpedition;
            detcomToUpdate.DateAchat = detcom.DateAchat;
            await upwaysDbContext.SaveChangesAsync();

        }
        public async Task DeleteAsync(DetailCommande detcom)
        {
            upwaysDbContext.Detailcommandes.Remove(detcom);
            await upwaysDbContext.SaveChangesAsync();
        }
    }
}
