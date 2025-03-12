using Microsoft.AspNetCore.Mvc;
using WsRest_UpWay.Models.EntityFramework;
using WsRest_UpWay.Models.Repository;

namespace WsRest_UpWay.Models.DataManager
{
    public class VeloManager : IDataRepository<Velo>
    {
        public Task AddAsync(Velo entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Velo entity)
        {
            throw new NotImplementedException();
        }

        public Task<ActionResult<IEnumerable<Velo>>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ActionResult<Velo>> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ActionResult<Velo>> GetByStringAsync(string str)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Velo entityToUpdate, Velo entity)
        {
            throw new NotImplementedException();
        }
    }
}
