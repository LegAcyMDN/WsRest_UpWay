using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Printing;
using WsRest_UpWay.Models.EntityFramework;
using WsRest_UpWay.Models.Repository;

namespace WsRest_UpWay.Models.DataManager
{
    public class PhotoAccessoireManager : IDataRepository<PhotoAccessoire>
    {
        public const int PAGE_SIZE = 20;
        private readonly S215UpWayContext upwaysDbContext;

        public PhotoAccessoireManager()
        {
        }

        public PhotoAccessoireManager(S215UpWayContext context)
        {
            upwaysDbContext = context;
        }

        public async Task AddAsync(PhotoAccessoire phoAcs)
        {
            await upwaysDbContext.Photoaccessoires.AddAsync(phoAcs);
            await upwaysDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(PhotoAccessoire phoAcs)
        {
            upwaysDbContext.Photoaccessoires.Remove(phoAcs);
            await upwaysDbContext.SaveChangesAsync();
        }

        public async Task<ActionResult<IEnumerable<PhotoAccessoire>>> GetAllAsync(int page = 0)
        {
            return await upwaysDbContext.Photoaccessoires.Skip(page * PAGE_SIZE).Take(PAGE_SIZE).ToListAsync();
        }

        public async Task<ActionResult<PhotoAccessoire>> GetByIdAsync(int id)
        {
            return await upwaysDbContext.Photoaccessoires.FirstOrDefaultAsync(p => p.PhotoAcessoireId == id);
        }

        public async Task<ActionResult<PhotoAccessoire>> GetByStringAsync(string str)
        {
            throw new NotImplementedException();
        }

        public async Task<ActionResult<int>> GetCountAsync()
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(PhotoAccessoire phoAcsToUpdate, PhotoAccessoire phoAcs)
        {
            upwaysDbContext.Entry(phoAcsToUpdate).State = EntityState.Modified;
            phoAcsToUpdate.PhotoAcessoireId = phoAcs.PhotoAcessoireId;
            phoAcsToUpdate.AccessoireId = phoAcs.AccessoireId;
            phoAcsToUpdate.UrlPhotoAccessoire = phoAcs.UrlPhotoAccessoire;
            await upwaysDbContext.SaveChangesAsync();
        }
    }
}
