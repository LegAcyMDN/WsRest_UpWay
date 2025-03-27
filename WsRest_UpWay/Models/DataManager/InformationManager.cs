using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WsRest_UpWay.Models.EntityFramework;
using WsRest_UpWay.Models.Repository;

namespace WsRest_UpWay.Models.DataManager;

public class InformationManager : IDataRepository<Information>
{
    public const int PAGE_SIZE = 20;
    private readonly S215UpWayContext? upwaysDbContext;

    public InformationManager()
    {
    }

    public InformationManager(S215UpWayContext context)
    {
        upwaysDbContext = context;
    }

    public async Task<ActionResult<IEnumerable<Information>>> GetAllAsync(int page)
    {
        return await upwaysDbContext.Informations.Skip(page * PAGE_SIZE).Take(PAGE_SIZE).ToListAsync();
    }

    public async Task<ActionResult<Information>> GetByIdAsync(int id)
    {
        return await upwaysDbContext.Informations.FirstOrDefaultAsync(u => u.InformationId == id);
    }

    public async Task<ActionResult<Information>> GetByStringAsync(string mode)
    {
        throw new NotImplementedException();
    }

    public async Task AddAsync(Information inf)
    {
        await upwaysDbContext.Informations.AddAsync(inf);
        await upwaysDbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Information infToUpdate, Information inf)
    {
        upwaysDbContext.Entry(infToUpdate).State = EntityState.Modified;
        infToUpdate.InformationId = inf.InformationId;
        infToUpdate.ReductionId = inf.ReductionId;
        infToUpdate.RetraitMagasinId = inf.RetraitMagasinId;
        infToUpdate.AdresseExpeId = inf.AdresseExpeId;
        infToUpdate.PanierId = inf.PanierId;
        infToUpdate.ContactInformations = inf.ContactInformations;
        infToUpdate.OffreEmail = inf.OffreEmail;
        infToUpdate.ModeLivraison = inf.ModeLivraison;
        infToUpdate.InformationPays = inf.InformationPays;
        infToUpdate.InformationRue = inf.InformationRue;
        await upwaysDbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Information inf)
    {
        upwaysDbContext.Informations.Remove(inf);
        await upwaysDbContext.SaveChangesAsync();
    }
}