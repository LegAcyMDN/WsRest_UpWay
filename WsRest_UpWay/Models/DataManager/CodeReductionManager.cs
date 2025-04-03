using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WsRest_UpWay.Models.EntityFramework;
using WsRest_UpWay.Models.Repository;

namespace WsRest_UpWay.Models.DataManager
{
    public class CodeReductionManager : IDataCodeReduction<CodeReduction>
    {
        public const int PAGE_SIZE = 20;
        private readonly S215UpWayContext? s215UpWayContext;

        public CodeReductionManager()
        {
        }

        public CodeReductionManager(S215UpWayContext context)
        {
            s215UpWayContext = context;
        }

        public async Task AddAsync(CodeReduction cor)
        {
            await s215UpWayContext.Codereductions.AddAsync(cor);
            await s215UpWayContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(CodeReduction cor)
        {
            s215UpWayContext.Codereductions.Remove(cor);
            await s215UpWayContext.SaveChangesAsync();
        }

        public async Task<ActionResult<IEnumerable<CodeReduction>>> GetAllAsync(int page)
        {
            return await s215UpWayContext.Codereductions.Skip(page * PAGE_SIZE).Take(PAGE_SIZE).ToListAsync();
        }

        public async Task<ActionResult<int>> GetCountAsync()
        {
            return await s215UpWayContext.Codereductions.CountAsync();
        }

        public async Task<ActionResult<CodeReduction>> GetByStringAsync(string id)
        {
            return await s215UpWayContext.Codereductions.FirstOrDefaultAsync(u =>
            u.ReductionId.ToUpper() == id.ToUpper());
        }
        public async Task UpdateAsync(CodeReduction catToUpdate, CodeReduction cat)
        {
            s215UpWayContext.Entry(catToUpdate).State = EntityState.Modified;
            catToUpdate.ReductionId = cat.ReductionId;
            catToUpdate.ActifReduction = cat.ActifReduction;
            catToUpdate.Reduction = cat.Reduction;
            await s215UpWayContext.SaveChangesAsync();
        }
    }
}
