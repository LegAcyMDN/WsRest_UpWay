using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WsRest_UpWay.Models.EntityFramework;

namespace WsRest_UpWay.Models.Repository
{
    public interface IDataAccessoire : IDataRepository<Accessoire>
    {
        Task<ActionResult<IEnumerable<Accessoire>>> GetByCategory(string categoryName);
        Task<ActionResult<IEnumerable<Accessoire>>> GetByCategoryPrix(string categoryName, int min, int max);
        Task<ActionResult<IEnumerable<Accessoire>>> GetByPrix(int min, int max);
    }
}