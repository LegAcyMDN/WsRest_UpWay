using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WsRest_UpWay.Models.EntityFramework;
using WsRest_UpWay.Models.Repository;

namespace WsRest_UpWay.Models.DataManager
{
    public class PhotoVeloManager : IDataRepository<PhotoVelo>
    {
        public const int PAGE_SIZE = 20;
        private readonly S215UpWayContext upwaysDbContext;

        public PhotoVeloManager()
        {
        }

        public PhotoVeloManager(S215UpWayContext context)
        {
            upwaysDbContext = context;
        }

        public async Task AddAsync(PhotoVelo phoVel)
        {
            await upwaysDbContext.Photovelos.AddAsync(phoVel);
            await upwaysDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(PhotoVelo phoVel)
        {
            upwaysDbContext.Photovelos.Remove(phoVel);
            await upwaysDbContext.SaveChangesAsync();
        }

        public async Task<ActionResult<IEnumerable<PhotoVelo>>> GetAllAsync(int page = 0)
        {
            return await upwaysDbContext.Photovelos.Skip(page * PAGE_SIZE).Take(PAGE_SIZE).ToListAsync();
        }

        public async Task<ActionResult<PhotoVelo>> GetByIdAsync(int id)
        {
            return await upwaysDbContext.Photovelos.FirstOrDefaultAsync(p => p.PhotoVeloId == id);
        }

        public async Task<ActionResult<PhotoVelo>> GetByStringAsync(string str)
        {
            throw new NotImplementedException();
        }

        public async Task<ActionResult<int>> GetCountAsync()
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(PhotoVelo phoVelToUpdate, PhotoVelo phoVel)
        {
            upwaysDbContext.Entry(phoVelToUpdate).State = EntityState.Modified;
            phoVelToUpdate.PhotoVeloId = phoVel.PhotoVeloId;
            phoVelToUpdate.VeloId = phoVel.VeloId;
            phoVelToUpdate.UrlPhotoVelo = phoVel.UrlPhotoVelo;
            phoVelToUpdate.PhotoBytea = phoVel.PhotoBytea;
            await upwaysDbContext.SaveChangesAsync();
        }
    }
}
