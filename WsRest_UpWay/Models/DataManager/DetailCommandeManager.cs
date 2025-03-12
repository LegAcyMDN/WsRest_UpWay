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
            return await upwaysDbContext.Detailcommandes.FirstOrDefaultAsync(u => u.Idcommande == id);
        }
        public async Task<ActionResult<Detailcommande>> GetByStringAsync(string nom)
        {
            return await upwaysDbContext.Detailcommandes.FirstOrDefaultAsync(u => u.Modeexpedition.ToUpper() == nom.ToUpper());
        }
        public async Task AddAsync(Detailcommande entity)
        {
            upwaysDbContext.Detailcommandes.AddAsync(entity);
            upwaysDbContext.SaveChangesAsync();
        }
        public async Task UpdateAsync(Detailcommande detailcommande, Detailcommande entity)
        {
            upwaysDbContext.Entry(detailcommande).State = EntityState.Modified;
            detailcommande.Idcommande = entity.Idcommande;
            detailcommande.Idretraitmagasin = entity.Idretraitmagasin;
            detailcommande.Idadressefact = entity.Idadressefact;
            detailcommande.Idetatcommande = entity.Idetatcommande;
            detailcommande.Idclient = entity.Idclient;
            detailcommande.Idpanier = entity.Idpanier;
            detailcommande.Moyenpaiement = entity.Moyenpaiement;
            detailcommande.Modeexpedition = entity.Modeexpedition;
            detailcommande.Dateachat = entity.Dateachat;
            upwaysDbContext.SaveChangesAsync();

        }
        public async Task DeleteAsync(Detailcommande detailcommande)
        {
            upwaysDbContext.Detailcommandes.Remove(detailcommande);
            upwaysDbContext.SaveChangesAsync();
        }
    }
}
