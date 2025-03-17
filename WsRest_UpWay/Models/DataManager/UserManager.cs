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

    public async Task<ActionResult<IEnumerable<CompteClient>>> GetAllAsync()
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

    public async Task AddAsync(CompteClient entity)
    {
        await _context.Compteclients.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(CompteClient entityToUpdate, CompteClient entity)
    {
        _context.Entry(entityToUpdate).State = EntityState.Modified;

        entityToUpdate.LoginClient = entity.LoginClient;
        entityToUpdate.MotDePasseClient = entity.MotDePasseClient;
        entityToUpdate.EmailClient = entity.EmailClient;
        entityToUpdate.RememberToken = entity.RememberToken;
        entityToUpdate.TwoFactorSecret = entity.TwoFactorSecret;
        entityToUpdate.TwoFactorRecoveryCodes = entity.TwoFactorRecoveryCodes;
        entityToUpdate.TwoFactorConfirmedAt = entity.TwoFactorConfirmedAt;
        entityToUpdate.Usertype = entity.Usertype;
        entityToUpdate.EmailVerifiedAt = entity.EmailVerifiedAt;
        entityToUpdate.IsFromGoogle = entity.IsFromGoogle;

        _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(CompteClient entity)
    {
        _context.Remove(entity);
        await _context.SaveChangesAsync();
    }
}