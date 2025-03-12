using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WsRest_UpWay.Models.EntityFramework;
using WsRest_UpWay.Models.Repository;

namespace WsRest_UpWay.Models.DataManager
{
    public class DetailCommandeManager : IDataRepository<Detailcommande>
    {
        readonly S215UpWayContext? upwaysDbContext;

        public DetailCommandeManager() { }
        public DetailCommandeManager(S215UpWayContext context)
        {
            upwaysDbContext = context;
        }
        public async Task<ActionResult<IEnumerable<Detailcommande>>> GetAllAsync()
        {
            return await upwaysDbContext.Detailcommandes.ToListAsync();
        }
        public async Task<ActionResult<Detailcommande>> GetByIdAsync(int id)
        {
            return await upwaysDbContext.Detailcommandes.FirstOrDefaultAsync(u => u.CommandeId == id);
        }
        public async Task<ActionResult<Detailcommande>> GetByStringAsync(string nom)
        {
            return await upwaysDbContext.Detailcommandes.FirstOrDefaultAsync(u => u.ModeExpedition.ToUpper() == nom.ToUpper());
        }
        public async Task AddAsync(Detailcommande entity)
        {
            upwaysDbContext.Detailcommandes.AddAsync(entity);
            upwaysDbContext.SaveChangesAsync();
        }
        public async Task UpdateAsync(Detailcommande detailcommande, Detailcommande entity)
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
            upwaysDbContext.SaveChangesAsync();

        }
        public async Task DeleteAsync(Detailcommande detailcommande)
        {
            upwaysDbContext.Detailcommandes.Remove(detailcommande);
            upwaysDbContext.SaveChangesAsync();
        }
    }
}
