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
        public async Task AddAsync(DetailCommande entity)
        {
            await upwaysDbContext.Detailcommandes.AddAsync(entity);
            await upwaysDbContext.SaveChangesAsync();
        }
        public async Task UpdateAsync(DetailCommande detailcommande, DetailCommande entity)
        {
            upwaysDbContext.Entry(detailcommande).State = EntityState.Modified;
            detailcommande.CommandeId = entity.CommandeId;
            detailcommande.RetraitMagasinId = entity.RetraitMagasinId;
            detailcommande.AdresseFactId = entity.AdresseFactId;
            detailcommande.EtatCommandeId = entity.EtatCommandeId;
            detailcommande.ClientId = entity.ClientId;
            detailcommande.PanierId = entity.PanierId;
            detailcommande.MoyenPaiement = entity.MoyenPaiement;
            detailcommande.ModeExpedition = entity.ModeExpedition;
            detailcommande.DateAchat = entity.DateAchat;
            await upwaysDbContext.SaveChangesAsync();

        }
        public async Task DeleteAsync(DetailCommande detailcommande)
        {
            upwaysDbContext.Detailcommandes.Remove(detailcommande);
            await upwaysDbContext.SaveChangesAsync();
        }
    }
}
