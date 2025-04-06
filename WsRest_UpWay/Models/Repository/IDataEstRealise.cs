using Microsoft.AspNetCore.Mvc;
using WsRest_UpWay.Models.EntityFramework;

namespace WsRest_UpWay.Models.Repository;

public interface IDataEstRealise : IDataRepository<EstRealise>
{
    Task<ActionResult<IEnumerable<EstRealise>>> GetByIdVeloAsync(int id);
    Task<ActionResult<EstRealise>> GetByIdsAsync(int idvelo, int idinspection, int idreparation);
}