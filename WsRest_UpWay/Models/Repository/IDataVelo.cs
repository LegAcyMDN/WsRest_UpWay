using Microsoft.AspNetCore.Mvc;
using WsRest_UpWay.Models.EntityFramework;

namespace WsRest_UpWay.Models.Repository
{
    public interface IDataVelo<TEntity>
    {
        Task<ActionResult<IEnumerable<TEntity>>> GetAllAsync();
        Task<ActionResult<TEntity>> GetByIdAsync(int id);
        Task AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entityToUpdate, TEntity entity);
        Task DeleteAsync(TEntity entity);

        Task<ActionResult<IEnumerable<TEntity>>> GetByFiltresAsync(string taille, int categorie, int cara, int marque, int annee, int kilomin, int kilomax, string posmot, string motmar, int couplemot, int capbat, string posbat, string batamo, string posbag, int poids);
    }
}
