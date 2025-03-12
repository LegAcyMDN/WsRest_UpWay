using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WsRest_UpWay.Models.EntityFramework;
using WsRest_UpWay.Models.Repository;

namespace WsRest_UpWay.Models.DataManager
{
    public class CompteClientManager : IDataRepository<Compteclient>
    {
        readonly S215UpWayContext? s215DbContext;
        public CompteClientManager() { }
        public CompteClientManager(S215UpWayContext context)
        {
            s215DbContext = context;
        }
        public async Task<ActionResult<IEnumerable<Compteclient>>> GetAllAsync()
        {
            return await s215DbContext.Compteclients.ToListAsync();
        }
        public async Task<ActionResult<Compteclient>> GetByIdAsync(int id)
        {
            return await s215DbContext.Compteclients.FirstOrDefaultAsync(u => u.Idclient == id);
        }
        public async Task<ActionResult<Compteclient>> GetByStringAsync(string mail)
        {
            return await s215DbContext.Compteclients.FirstOrDefaultAsync(u => u.Emailclient.ToUpper() == mail.ToUpper());
        }
        public async Task AddAsync(Compteclient entity)
        {
            s215DbContext.Compteclients.Add(entity);
            s215DbContext.SaveChanges();
        }
        public async Task UpdateAsync(Compteclient Compteclient, Compteclient entity)
        {
            s215DbContext.Entry(Compteclient).State = EntityState.Modified;
            Compteclient.Idclient = entity.Idclient;
            Compteclient.Loginclient = entity.Loginclient;
            Compteclient.Motdepasseclient = entity.Motdepasseclient;
            Compteclient.Emailclient = entity.Emailclient;
            Compteclient.Prenomclient = entity.Prenomclient;
            Compteclient.Nomclient = entity.Nomclient;
            Compteclient.Datecreation = entity.Datecreation;
            Compteclient.RememberToken = entity.RememberToken;
            Compteclient.TwoFactorRecoveryCodes = entity.TwoFactorRecoveryCodes;
            Compteclient.TwoFactorConfirmedAt = entity.TwoFactorConfirmedAt;
            Compteclient.Usertype = entity.Usertype;
            Compteclient.EmailVerifiedAt = entity.EmailVerifiedAt;
            Compteclient.IsFromGoogle = entity.IsFromGoogle;
            s215DbContext.SaveChanges();
        }
        public async Task DeleteAsync(Compteclient Compteclient)
        {
            s215DbContext.Compteclients.Remove(Compteclient);
            s215DbContext.SaveChanges();
        }
    }
}
