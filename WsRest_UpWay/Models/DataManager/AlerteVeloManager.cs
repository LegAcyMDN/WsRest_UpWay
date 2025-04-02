using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WsRest_UpWay.Models.EntityFramework;
using WsRest_UpWay.Models.Repository;

namespace WsRest_UpWay.Models.DataManager
{
    public class AlerteVeloManager : IDataRepository<AlerteVelo>
    {
        public const int PAGE_SIZE = 20;
        private readonly S215UpWayContext upwaysDbContext;

        public AlerteVeloManager()
        {
        }

        public AlerteVeloManager(S215UpWayContext context)
        {
            upwaysDbContext = context;
        }

        public async Task AddAsync(AlerteVelo aleVel)
        {
            await upwaysDbContext.Alertevelos.AddAsync(aleVel);
            await upwaysDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(AlerteVelo aleVel)
        {
            upwaysDbContext.Alertevelos.Remove(aleVel);
            await upwaysDbContext.SaveChangesAsync();
        }

        public async Task<ActionResult<IEnumerable<AlerteVelo>>> GetAllAsync(int page = 0)
        {
            return await upwaysDbContext.Alertevelos.Skip(page * PAGE_SIZE).Take(PAGE_SIZE).ToListAsync();
        }

        public async Task<ActionResult<AlerteVelo>> GetByIdAsync(int id)
        {
            return await upwaysDbContext.Alertevelos.FirstOrDefaultAsync( a => a.AlerteId == id);
        }

        public async Task<ActionResult<AlerteVelo>> GetByStringAsync(string str)
        {
            throw new NotImplementedException();
        }

        public async Task<ActionResult<int>> GetCountAsync()
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(AlerteVelo aleVelToUpdate, AlerteVelo aleVel)
        {
            upwaysDbContext.Entry(aleVelToUpdate).State = EntityState.Modified;
            aleVelToUpdate.AlerteId = aleVel.AlerteId;
            aleVelToUpdate.ClientId = aleVel.ClientId;
            aleVelToUpdate.VeloId = aleVel.VeloId;
            aleVelToUpdate.ModificationAlerte = aleVel.ModificationAlerte;
            await upwaysDbContext.SaveChangesAsync();
        }
    }
}
