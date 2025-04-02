using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WsRest_UpWay.Helpers;
using WsRest_UpWay.Models.EntityFramework;
using WsRest_UpWay.Models.Repository;

namespace WsRest_UpWay.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MarqageVeloController : ControllerBase
{
    private readonly IDataRepository<MarquageVelo> _dataRepository;
    private readonly IDataRepository<Panier> _panierRepository;

    public MarqageVeloController(IDataRepository<MarquageVelo> dataRepository, IDataRepository<Panier> panierRepository)
    {
        _dataRepository = dataRepository;
        _panierRepository = panierRepository;
    }

    // GET: api/Panier
    [HttpGet]
    public async Task<ActionResult<IEnumerable<MarquageVelo>>> Gets()
    {
        return await _dataRepository.GetAllAsync();
    }

    // GET: api/Panier/5
    [HttpGet("{id}")]
    [Authorize]
    public async Task<ActionResult<MarquageVelo>> Get(int id)
    {
        var lignepanier = await _dataRepository.GetByIdAsync(id);
        if (lignepanier.Value == null) return NotFound();

        var panier = await _panierRepository.GetByIdAsync(lignepanier.Value.PanierId);
        if (panier.Value == null) return NotFound();
        if (panier.Value.ClientId != User.GetId()) return Unauthorized();

        return lignepanier;
    }

    // PUT: api/Panier/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    [Authorize]
    public async Task<IActionResult> Put(int id, MarquageVelo body)
    {
        if (id != body.PanierId) return BadRequest();

        var lignepanier = await _dataRepository.GetByIdAsync(id);
        if (lignepanier.Value == null) return NotFound();

        var panier = await _panierRepository.GetByIdAsync(lignepanier.Value.PanierId);
        if (panier.Value == null) return NotFound();
        if (panier.Value.ClientId != User.GetId()) return Unauthorized();

        await _dataRepository.UpdateAsync(lignepanier.Value, body);

        return NoContent();
    }

    // DELETE: api/Panier/5
    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> Delete(int id)
    {
        var lignepanier = await _dataRepository.GetByIdAsync(id);
        if (lignepanier.Value == null) return NotFound();

        var panier = await _panierRepository.GetByIdAsync(lignepanier.Value.PanierId);
        if (panier.Value == null) return NotFound();
        if (panier.Value.ClientId != User.GetId()) return Unauthorized();

        await _dataRepository.DeleteAsync(lignepanier.Value);

        return NoContent();
    }
}