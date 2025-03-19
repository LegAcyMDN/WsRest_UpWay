using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WsRest_UpWay.Models.EntityFramework;
using WsRest_UpWay.Models.Repository;

namespace WsRest_UpWay.Models.DataManager
{
    public class PanierManager : IDataRepository<Panier>
    {
        readonly S215UpWayContext upwaysDbContext;

        public PanierManager() { }

        public PanierManager(S215UpWayContext context)
        {
            upwaysDbContext = context;
        }

        public async Task AddAsync(Panier entity)
        {
            await upwaysDbContext.Paniers.AddAsync(entity);
            await upwaysDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Panier entity)
        {
            upwaysDbContext.Paniers.Remove(entity);
            await upwaysDbContext.SaveChangesAsync();
        }

        public async Task<ActionResult<IEnumerable<Panier>>> GetAllAsync()
        {
            return await upwaysDbContext.Paniers.ToListAsync();
        }

        public async Task<ActionResult<Panier>> GetByIdAsync(int id)
        {
            return await upwaysDbContext.Paniers.FirstOrDefaultAsync(u => u.PanierId == id);
        }

        public async Task<ActionResult<Panier>> GetByStringAsync(string str)
        {
            return await upwaysDbContext.Paniers.FirstOrDefaultAsync(u => u.Cookie == str);
        }

        public async Task UpdateAsync(Panier entityToUpdate, Panier entity)
        {
            upwaysDbContext.Entry(entityToUpdate).State = EntityState.Modified;
            entityToUpdate.PanierId = entity.PanierId;
            entityToUpdate.ClientId = entity.ClientId;
            entityToUpdate.CommandeId = entity.CommandeId;
            entityToUpdate.Cookie = entity.Cookie;
            entityToUpdate.PrixPanier = entity.PrixPanier;
            await upwaysDbContext.SaveChangesAsync();
        }
    }
}
