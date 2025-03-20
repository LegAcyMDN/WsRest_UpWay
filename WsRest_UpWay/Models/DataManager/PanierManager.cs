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

        public async Task AddAsync(Panier pan)
        {
            await upwaysDbContext.Paniers.AddAsync(pan);
            await upwaysDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Panier pan)
        {
            upwaysDbContext.Paniers.Remove(pan);
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
            return await upwaysDbContext.Paniers.FirstOrDefaultAsync(u => u.Cookie.ToUpper() == str.ToUpper());
        }

        public async Task UpdateAsync(Panier panToUpdate, Panier pan)
        {
            upwaysDbContext.Entry(panToUpdate).State = EntityState.Modified;
            panToUpdate.PanierId = pan.PanierId;
            panToUpdate.ClientId = pan.ClientId;
            panToUpdate.CommandeId = pan.CommandeId;
            panToUpdate.Cookie = pan.Cookie;
            panToUpdate.PrixPanier = pan.PrixPanier;
            await upwaysDbContext.SaveChangesAsync();
        }
    }
}
