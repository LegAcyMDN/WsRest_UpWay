using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WsRest_UpWay.Models.EntityFramework;
using WsRest_UpWay.Models.Repository;

namespace WsRest_UpWay.Models.DataManager
{
    public class CompteClientManager : IDataRepository<CompteClient>
    {
        readonly S215UpWayContext? s215DbContext;
        public CompteClientManager() { }
        public CompteClientManager(S215UpWayContext context)
        {
            s215DbContext = context;
        }
        public async Task<ActionResult<IEnumerable<CompteClient>>> GetAllAsync()
        {
            return await s215DbContext.Compteclients.ToListAsync();
        }
        public async Task<ActionResult<CompteClient>> GetByIdAsync(int id)
        {
            return await s215DbContext.Compteclients.FirstOrDefaultAsync(u => u.ClientId == id);
        }
        public async Task<ActionResult<CompteClient>> GetByStringAsync(string mail)
        {
            return await s215DbContext.Compteclients.FirstOrDefaultAsync(u => u.EmailClient.ToUpper() == mail.ToUpper());
        }
        public async Task AddAsync(CompteClient entity)
        {
            s215DbContext.Compteclients.Add(entity);
            s215DbContext.SaveChanges();
        }
        public async Task UpdateAsync(CompteClient Compteclient, CompteClient entity)
        {
            s215DbContext.Entry(Compteclient).State = EntityState.Modified;
            Compteclient.ClientId = entity.ClientId;
            Compteclient.LoginClient = entity.LoginClient;
            Compteclient.MotDePasseClient = entity.MotDePasseClient;
            Compteclient.EmailClient = entity.EmailClient;
            Compteclient.PrenomClient = entity.PrenomClient;
            Compteclient.NomClient = entity.NomClient;
            Compteclient.DateCreation = entity.DateCreation;
            Compteclient.RememberToken = entity.RememberToken;
            Compteclient.TwoFactorRecoveryCodes = entity.TwoFactorRecoveryCodes;
            Compteclient.TwoFactorConfirmedAt = entity.TwoFactorConfirmedAt;
            Compteclient.Usertype = entity.Usertype;
            Compteclient.EmailVerifiedAt = entity.EmailVerifiedAt;
            Compteclient.IsFromGoogle = entity.IsFromGoogle;
            s215DbContext.SaveChanges();
        }
        public async Task DeleteAsync(CompteClient Compteclient)
        {
            s215DbContext.Compteclients.Remove(Compteclient);
            s215DbContext.SaveChanges();
        }
    }
}
