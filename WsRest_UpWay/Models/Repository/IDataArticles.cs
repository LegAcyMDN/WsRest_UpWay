using Microsoft.AspNetCore.Mvc;
using WsRest_UpWay.Models.EntityFramework;

namespace WsRest_UpWay.Models.Repository
{
    public interface IDataArticles : IDataRepository<Article>
    {
        Task<ActionResult<IEnumerable<Article>>> GetByCategoryIdAsync(int id);
    }
}
