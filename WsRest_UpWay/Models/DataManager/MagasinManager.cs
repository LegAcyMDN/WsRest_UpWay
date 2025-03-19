using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WsRest_UpWay.Models.EntityFramework;

namespace WsRest_UpWay.Models.DataManager
{
    public class MagasinManager
    {
        readonly S215UpWayContext? upwaysDbContext;

        public MagasinManager() { }

        public MagasinManager(S215UpWayContext context)
        {
            upwaysDbContext = context;
        }

        public async Task<ActionResult<IEnumerable<Magasin>>> GetAllAsync()
        {
            return await upwaysDbContext.Magasins.ToListAsync();
        }

        public async Task<ActionResult<Magasin>> GetByIdAsync(int id)
        {
            return await upwaysDbContext.Magasins.FirstOrDefaultAsync(u => u.MagasinId == id);
        }

        public async Task<ActionResult<Magasin>> GetByStringAsync(string nom)
        {
            return await upwaysDbContext.Magasins.FirstOrDefaultAsync(u => u.NomMagasin.ToLower() == nom.ToUpper());
        }

        public async Task AddAsync(Magasin mag)
        {
            await upwaysDbContext.Magasins.AddAsync(mag);
            await upwaysDbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Magasin magToUpdate, Magasin mag)
        {
            upwaysDbContext.Entry(magToUpdate).State = EntityState.Modified;
            magToUpdate.MagasinId = mag.MagasinId;
            magToUpdate.NomMagasin = mag.NomMagasin;
            magToUpdate.HoraireMagasin = mag.HoraireMagasin;
            magToUpdate.RueMagasin = mag.RueMagasin;
            magToUpdate.CPMagasin = mag.CPMagasin;
            magToUpdate.VilleMagasin = mag.VilleMagasin;
            await upwaysDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Magasin mag)
        {
            upwaysDbContext.Magasins.Remove(mag);
            await upwaysDbContext.SaveChangesAsync();
        }
    }
}
