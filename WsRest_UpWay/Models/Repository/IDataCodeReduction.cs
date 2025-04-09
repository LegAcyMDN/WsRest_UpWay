using Microsoft.AspNetCore.Mvc;
using WsRest_UpWay.Models.EntityFramework;

namespace WsRest_UpWay.Models.Repository
{
    public interface IDataCodeReduction<TEntity>
    {
        Task<ActionResult<IEnumerable<TEntity>>> GetAllAsync(int page = 0);



        Task<ActionResult<int>> GetCountAsync();


        Task<ActionResult<CodeReduction>> GetByStringAsync(string reductionCode);


        Task AddAsync(TEntity entity);


        Task UpdateAsync(TEntity entityToUpdate, TEntity entity);


        Task DeleteAsync(TEntity entity);
    }
}
