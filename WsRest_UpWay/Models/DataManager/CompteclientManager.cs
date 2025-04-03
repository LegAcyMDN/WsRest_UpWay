using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WsRest_UpWay.Models.EntityFramework;
using WsRest_UpWay.Models.Repository;

namespace WsRest_UpWay.Models.DataManager
{
    public class CompteclientManager : IDataRepository<CompteClient>
    {
        public const int PAGE_SIZE = 20;
        private readonly S215UpWayContext? s215UpWayContext;

        public CompteclientManager()
        {
        }

        public CompteclientManager(S215UpWayContext context)
        {
            s215UpWayContext = context;
        }

        public async Task AddAsync(CompteClient coc)
        {
            await s215UpWayContext.Compteclients.AddAsync(coc);
            await s215UpWayContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(CompteClient coc)
        {
            s215UpWayContext.Compteclients.Remove(coc);
            await s215UpWayContext.SaveChangesAsync();
        }

        public async Task<ActionResult<IEnumerable<CompteClient>>> GetAllAsync(int page)
        {
            return await s215UpWayContext.Compteclients.Skip(page * PAGE_SIZE).Take(PAGE_SIZE).ToListAsync();
        }

        public async Task<ActionResult<int>> GetCountAsync()
        {
            return await s215UpWayContext.Compteclients.CountAsync();
        }

        public async Task<ActionResult<CompteClient>> GetByIdAsync(int id)
        {
            return await s215UpWayContext.Compteclients.FirstOrDefaultAsync(u => u.ClientId == id);
        }

        public async Task<ActionResult<CompteClient>> GetByStringAsync(string lib)
        {
            return await s215UpWayContext.Compteclients.FirstOrDefaultAsync(u =>
                u.EmailClient.ToUpper() == lib.ToUpper());
        }

        public async Task UpdateAsync(CompteClient comToUpdate, CompteClient com)
        {
            s215UpWayContext.Entry(comToUpdate).State = EntityState.Modified;
            comToUpdate.ClientId = com.ClientId;
            comToUpdate.LoginClient = com.LoginClient;
            comToUpdate.MotDePasseClient = com.MotDePasseClient;
            comToUpdate.EmailClient = com.EmailClient;
            comToUpdate.PrenomClient = com.PrenomClient;
            comToUpdate.NomClient = com.NomClient;
            comToUpdate.DateCreation = com.DateCreation;
            comToUpdate.RememberToken = com.RememberToken;
            comToUpdate.TwoFactorSecret = com.TwoFactorSecret;
            comToUpdate.TwoFactorRecoveryCodes = com.TwoFactorRecoveryCodes;
            comToUpdate.TwoFactorConfirmedAt = com.TwoFactorConfirmedAt;
            comToUpdate.Usertype = com.Usertype;
            comToUpdate.EmailVerifiedAt = com.EmailVerifiedAt;
            comToUpdate.IsFromGoogle = com.IsFromGoogle;
            await s215UpWayContext.SaveChangesAsync();
        }
    }
}
