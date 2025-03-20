using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WsRest_UpWay.Models.EntityFramework;

namespace WsRest_UpWay.Models.Repository
{
    public interface IDataAccessoire : IDataRepository<Accessoire>
    {
        Task<ActionResult<IEnumerable<Accessoire>>> GetByCategoryAsync(string categoryName);
        Task<ActionResult<IEnumerable<Accessoire>>> GetByCategoryPrixAsync(string categoryName, int min, int max);
        Task<ActionResult<IEnumerable<Accessoire>>> GetByPrixAsync(int min, int max);
    }
}