using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WsRest_UpWay.Models.EntityFramework;
using WsRest_UpWay.Models.Repository;

namespace WsRest_UpWay.Models.DataManager;

public class UserManager : IDataRepository<CompteClient>
{
    private readonly S215UpWayContext _context;

    public UserManager()
    {
    }

    public UserManager(S215UpWayContext context)
    {
        _context = context;
    }

    public async Task<ActionResult<IEnumerable<CompteClient>>> GetAllAsync(int page)
    {
        return await _context.Compteclients.ToListAsync();
    }

    public async Task<ActionResult<CompteClient>> GetByIdAsync(int id)
    {
        return await _context.Compteclients.FindAsync(id);
    }

    public async Task<ActionResult<CompteClient>> GetByStringAsync(string str)
    {
        return _context.Compteclients.FirstOrDefault(c => c.EmailClient.ToLower() == str.ToLower());
    }

    public async Task AddAsync(CompteClient coc)
    {
        await _context.Compteclients.AddAsync(coc);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(CompteClient cocToUpdate, CompteClient coc)
    {
        _context.Entry(cocToUpdate).State = EntityState.Modified;

        cocToUpdate.LoginClient = coc.LoginClient;
        cocToUpdate.MotDePasseClient = coc.MotDePasseClient;
        cocToUpdate.EmailClient = coc.EmailClient;
        cocToUpdate.RememberToken = coc.RememberToken;
        cocToUpdate.TwoFactorSecret = coc.TwoFactorSecret;
        cocToUpdate.TwoFactorRecoveryCodes = coc.TwoFactorRecoveryCodes;
        cocToUpdate.TwoFactorConfirmedAt = coc.TwoFactorConfirmedAt;
        cocToUpdate.Usertype = coc.Usertype;
        cocToUpdate.EmailVerifiedAt = coc.EmailVerifiedAt;
        cocToUpdate.IsFromGoogle = coc.IsFromGoogle;

        _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(CompteClient coc)
    {
        _context.Remove(coc);
        await _context.SaveChangesAsync();
    }
}