using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WsRest_UpWay.Helpers;
using WsRest_UpWay.Models.EntityFramework;
using WsRest_UpWay.Models.Repository;

namespace WsRest_UpWay.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LignePanierController : ControllerBase
{
    private readonly IDataLignePanier _dataRepository;
    private readonly IDataRepository<Panier> _panierRepository;
    private readonly IDataRepository<MarquageVelo> _marquageVeloRepository;
    private static Random random = new Random();


    public LignePanierController(IDataLignePanier dataRepository, IDataRepository<Panier> panierRepository, IDataRepository<MarquageVelo> marquageVeloRepository)
    {
        _dataRepository = dataRepository;
        _panierRepository = panierRepository;
        _marquageVeloRepository = marquageVeloRepository;
    }

    // GET: api/Panier
    [HttpGet]
    public async Task<ActionResult<IEnumerable<LignePanier>>> GetPaniers()
    {
        return await _dataRepository.GetAllAsync();
    }

    // GET: api/Panier/5
    [HttpGet("{panierId}/{veloId}")]
    [Authorize]
    public async Task<ActionResult<LignePanier>> GetLignePanier(int panierId, int veloId)
    {
        var lignepanier = await _dataRepository.GetByIdsAsync(panierId, veloId);
        if (lignepanier.Value == null) return NotFound();

        var panier = await _panierRepository.GetByIdAsync(lignepanier.Value.PanierId);
        if (panier.Value == null) return NotFound();
        if (panier.Value.ClientId != User.GetId()) return Unauthorized();

        return lignepanier;
    }

    // PUT: api/Panier/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut]
    [Authorize]
    public async Task<IActionResult> PutPanier(LignePanier body)
    {
        var lignepanier = await _dataRepository.GetByIdsAsync(body.PanierId, body.VeloId);
        if (lignepanier.Value == null) return NotFound();

        var panier = await _panierRepository.GetByIdAsync(lignepanier.Value.PanierId);
        if (panier.Value == null) return NotFound();
        if (panier.Value.ClientId != User.GetId()) return Unauthorized();

        await _dataRepository.UpdateAsync(lignepanier.Value, body);

        return NoContent();
    }

    // POST: api/Panier
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    [Authorize]
    public async Task<ActionResult<LignePanier>> PostPanier(LignePanier lignepanier)
    {
        var panier = await _panierRepository.GetByIdAsync(lignepanier.PanierId);
        if (panier.Value == null) return NotFound();
        if (panier.Value.ClientId != User.GetId()) return Unauthorized();
        
        await _dataRepository.AddAsync(lignepanier);

        MarquageVelo marquageVelo = new MarquageVelo()
        {
            VeloId = lignepanier.VeloId,
            CodeMarquage = RandomString(10),
            PrixMarquage = (decimal?)990.0,
            PanierId = lignepanier.PanierId
        };
        
        await _marquageVeloRepository.AddAsync(marquageVelo);

        return CreatedAtAction("GetLignePanier", new { panierId = lignepanier.PanierId, veloId = lignepanier.VeloId }, lignepanier);
    }

    // DELETE: api/Panier/5
    [HttpDelete("{panierId}/{veloId}")]
    [Authorize]
    public async Task<IActionResult> DeletePanier(int panierId, int veloId)
    {
        var lignepanier = await _dataRepository.GetByIdsAsync(panierId, veloId);
        if (lignepanier.Value == null) return NotFound();

        var panier = await _panierRepository.GetByIdAsync(lignepanier.Value.PanierId);
        if (panier.Value == null) return NotFound();
        if (panier.Value.ClientId != User.GetId()) return Unauthorized();

        await _dataRepository.DeleteAsync(lignepanier.Value);

        return NoContent();
    }
    

    private static string RandomString(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }
}