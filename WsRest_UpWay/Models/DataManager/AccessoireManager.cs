using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WsRest_UpWay.Models.EntityFramework;
using WsRest_UpWay.Models.Repository;
namespace WsRest_UpWay.Models.DataManager
{
    public class AccessoireManager : IDatarepository<Accessoire>
    {
        readonly S215UpWayContext? upwaysDbContext;
        
        public AccessoireManager() { }
        public AccessoireManager(S215UpWayContext context)
        {
            upwaysDbContext = context;
        }
        public async Task<ActionResult<IEnumerable<Accessoire>>> GetAllAsync()
        {
            return await upwaysDbContext.Accessoires.ToListAsync();
        }
        public async Task<ActionResult<Accessoire>> GetByIdAsync(int id)
        {
            return await upwaysDbContext.Accessoires.FirstOrDefaultAsync(u => u.Idaccessoire == id);
        }
        public async Task<ActionResult<Accessoire>> GetByStringAsync(string nom)
        {
            return await upwaysDbContext.Accessoires.FirstOrDefaultAsync(u => u.Nomaccessoire.ToUpper() == nom.ToUpper());
        }
        public async Task AddAsync(Accessoire entity)
        {
            upwaysDbContext.Accessoires.AddAsync(entity);
            upwaysDbContext.SaveChangesAsync();
        }
        public async Task UpdateAsync(Accessoire accessoire, Accessoire entity)
        {
            upwaysDbContext.Entry(accessoire).State = EntityState.Modified;
            accessoire.Idaccessoire = entity.Idaccessoire;
            accessoire.Idmarque = entity.Idmarque;
            accessoire.Idcategorie = entity.Idcategorie;
            accessoire.Nomaccessoire = entity.Nomaccessoire;
            accessoire.Prixaccessoire = entity.Prixaccessoire;
            accessoire.Descriptionaccessoire = entity.Descriptionaccessoire;
            upwaysDbContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(Accessoire accessoire)
        {
            upwaysDbContext.Accessoires.Remove(accessoire);
            upwaysDbContext.SaveChangesAsync();
        }
    }
}
