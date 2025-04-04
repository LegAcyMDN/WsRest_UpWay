using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WsRest_UpWay.Models.EntityFramework;
using WsRest_UpWay.Models.Repository;

namespace WsRest_UpWay.Models.DataManager
{

    public class MoteurManager : IDataRepository<Moteur>
    {
        public const int PAGE_SIZE = 20;
        private readonly S215UpWayContext upwaysDbContext;

        public MoteurManager()
        {
        }

        public MoteurManager(S215UpWayContext context)
        {
            upwaysDbContext = context;
        }

        public async Task AddAsync(Moteur moteur)
        {
            await upwaysDbContext.Moteurs.AddAsync(moteur);
            await upwaysDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Moteur moteur)
        {
            upwaysDbContext.Moteurs.Remove(moteur);
            await upwaysDbContext.SaveChangesAsync();
        }

        public async Task<ActionResult<IEnumerable<Moteur>>> GetAllAsync(int page = 0)
        {
            return await upwaysDbContext.Moteurs.Skip(page * PAGE_SIZE).Take(PAGE_SIZE).ToListAsync();
        }

        public async Task<ActionResult<Moteur>> GetByIdAsync(int id)
        {
            return await upwaysDbContext.Moteurs.FirstOrDefaultAsync(a => a.MoteurId == id);
        }

        public async Task<ActionResult<Moteur>> GetByStringAsync(string str)
        {
            throw new NotImplementedException();
        }

        public async Task<ActionResult<int>> GetCountAsync()
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(Moteur moteurToUpdate, Moteur moteur)
        {
            upwaysDbContext.Entry(moteurToUpdate).State = EntityState.Modified;
            moteurToUpdate.MoteurId = moteur.MoteurId;
            moteurToUpdate.MarqueId = moteur.MarqueId;
            moteurToUpdate.ModeleMoteur = moteur.ModeleMoteur;
            moteurToUpdate.CoupleMoteur = moteur.CoupleMoteur;
            moteurToUpdate.VitesseMaximal = moteur.VitesseMaximal;
            upwaysDbContext.SaveChangesAsync();
        }
    }
}
