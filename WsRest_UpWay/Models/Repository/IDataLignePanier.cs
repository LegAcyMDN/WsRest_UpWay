using Microsoft.AspNetCore.Mvc;
using WsRest_UpWay.Models.EntityFramework;

namespace WsRest_UpWay.Models.Repository;

public interface IDataLignePanier : IDataRepository<LignePanier>
{
    Task<ActionResult<LignePanier>> GetByIdsAsync(int panierId, int veloId);
}