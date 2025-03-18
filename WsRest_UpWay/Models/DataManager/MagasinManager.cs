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
            return await upwaysDbContext.Magasins.FirstOrDefaultAsync(u => u.NomMagasin.ToUpper() == nom.ToUpper());
        }

        public async Task AddAsync(Magasin entity)
        {
            await upwaysDbContext.Magasins.AddAsync(entity);
            await upwaysDbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Magasin magasin, Magasin entity)
        {
            upwaysDbContext.Entry(magasin).State = EntityState.Modified;
            magasin.MagasinId = entity.MagasinId;
            magasin.NomMagasin = entity.NomMagasin;
            magasin.HoraireMagasin = entity.HoraireMagasin;
            magasin.RueMagasin = entity.RueMagasin;
            magasin.CPMagasin = entity.CPMagasin;
            magasin.VilleMagasin = entity.VilleMagasin;
            await upwaysDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Magasin magasin)
        {
            upwaysDbContext.Magasins.Remove(magasin);
            await upwaysDbContext.SaveChangesAsync();
        }
    }
}
