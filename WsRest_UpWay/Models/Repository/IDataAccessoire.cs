using Microsoft.AspNetCore.Mvc;
using WsRest_UpWay.Models.EntityFramework;

namespace WsRest_UpWay.Models.Repository
{
    public interface IDataAccessoire : IDataRepository<Accessoire>
    {
        Task<ActionResult<IEnumerable<Accessoire>>> GetByCategory(string categoryName);
    }
}