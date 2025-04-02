using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WsRest_UpWay.Models.EntityFramework;
using WsRest_UpWay.Models.Repository;

namespace WsRest_UpWay.Models.DataManager
{
    public class AssuranceManager : IDataRepository<Assurance>
    {
        public const int PAGE_SIZE = 20;
        private readonly S215UpWayContext upwaysDbContext;

        public AssuranceManager()
        {
        }

        public AssuranceManager(S215UpWayContext context)
        {
            upwaysDbContext = context;
        }

        public async Task AddAsync(Assurance ass)
        {
            await upwaysDbContext.Assurances.AddAsync(ass);
            await upwaysDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Assurance ass)
        {
            upwaysDbContext.Assurances.Remove(ass);
            await upwaysDbContext.SaveChangesAsync();
        }

        public async Task<ActionResult<IEnumerable<Assurance>>> GetAllAsync(int page = 0)
        {
            return await upwaysDbContext.Assurances.Skip(page * PAGE_SIZE).Take(PAGE_SIZE).ToListAsync();
        }

        public async Task<ActionResult<Assurance>> GetByIdAsync(int id)
        {
            return await upwaysDbContext.Assurances.FirstOrDefaultAsync();
        }

        public async Task<ActionResult<Assurance>> GetByStringAsync(string str)
        {
            return await upwaysDbContext.Assurances.FirstOrDefaultAsync(u => u.TitreAssurance.ToUpper() == str.ToUpper());
        }

        public async Task<ActionResult<int>> GetCountAsync()
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(Assurance assToUpdate, Assurance ass)
        {
            upwaysDbContext.Entry(assToUpdate).State = EntityState.Modified;
            assToUpdate.AssuranceId = ass.AssuranceId;
            assToUpdate.TitreAssurance = ass.TitreAssurance;
            assToUpdate.DescriptionAssurance = ass.DescriptionAssurance;
            assToUpdate.PrixAssurance = ass.PrixAssurance;
            await upwaysDbContext.SaveChangesAsync();
        }
    }
}
