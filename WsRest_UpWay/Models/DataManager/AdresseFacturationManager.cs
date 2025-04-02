using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WsRest_UpWay.Models.EntityFramework;
using WsRest_UpWay.Models.Repository;

namespace WsRest_UpWay.Models.DataManager
{
    public class AdresseFacturationManager : IDataRepository<AdresseFacturation>
    {
        public const int PAGE_SIZE = 20;
        private readonly S215UpWayContext upwaysDbContext;

        public AdresseFacturationManager()
        {
        }

        public AdresseFacturationManager(S215UpWayContext context)
        {
            upwaysDbContext = context;
        }

        public async Task AddAsync(AdresseFacturation adrFac)
        {
            await upwaysDbContext.Adressefacturations.AddAsync(adrFac);
            await upwaysDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(AdresseFacturation adrFac)
        {
            upwaysDbContext.Adressefacturations.Remove(adrFac);
            await upwaysDbContext.SaveChangesAsync();
        }

        public async Task<ActionResult<IEnumerable<AdresseFacturation>>> GetAllAsync(int page = 0)
        {
            return await upwaysDbContext.Adressefacturations.Skip(page * PAGE_SIZE).Take(PAGE_SIZE).ToListAsync();
        }

        public async Task<ActionResult<AdresseFacturation>> GetByIdAsync(int id)
        {
            return await upwaysDbContext.Adressefacturations.FirstOrDefaultAsync(a => a.AdresseFactId == id);
        }

        public async Task<ActionResult<AdresseFacturation>> GetByStringAsync(string str)
        {
            throw new NotImplementedException();
        }

        public Task<ActionResult<int>> GetCountAsync()
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(AdresseFacturation adrFacToUpdate, AdresseFacturation adrFac)
        {
            upwaysDbContext.Entry(adrFacToUpdate).State = EntityState.Modified;
            adrFacToUpdate.AdresseFactId = adrFac.AdresseFactId;
            adrFacToUpdate.ClientId = adrFac.ClientId;
            adrFacToUpdate.AdresseExpId = adrFac.AdresseExpId;
            adrFacToUpdate.PaysFacturation = adrFac.PaysFacturation;
            adrFacToUpdate.BatimentFacturationOpt = adrFac.BatimentFacturationOpt;
            adrFacToUpdate.RueFacturation = adrFac.RueFacturation;
            adrFacToUpdate.CPFacturation = adrFac.CPFacturation;
            adrFacToUpdate.RegionFacturation = adrFac.RegionFacturation;
            adrFacToUpdate.VilleFacturation = adrFac.VilleFacturation;
            adrFacToUpdate.TelephoneFacturation = adrFac.TelephoneFacturation;
            await upwaysDbContext.SaveChangesAsync();
        }
    }
}
