using Microsoft.AspNetCore.Mvc;
using WsRest_UpWay.Models.EntityFramework;

namespace WsRest_UpWay.Models.Repository
{
    public interface IDataPanier : IDataRepository<Panier>
    {
        public Task<ActionResult<Panier>> GetByUser(int user_id);
    }
}
