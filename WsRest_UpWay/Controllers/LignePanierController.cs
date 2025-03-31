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
    private readonly IDataRepository<LignePanier> _dataRepository;
    private readonly IDataRepository<Panier> _panierRepository;

    public LignePanierController(IDataRepository<LignePanier> dataRepository, IDataRepository<Panier> panierRepository)
    {
        _dataRepository = dataRepository;
        _panierRepository = panierRepository;
    }

    // GET: api/Panier
    [HttpGet]
    public async Task<ActionResult<IEnumerable<LignePanier>>> GetPaniers()
    {
        return await _dataRepository.GetAllAsync();
    }

    // GET: api/Panier/5
    [HttpGet("{id}")]
    [Authorize]
    public async Task<ActionResult<LignePanier>> GetPanier(int id)
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
    public async Task<IActionResult> PutPanier(int id, LignePanier body)
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

    // POST: api/Panier
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    [Authorize]
    public async Task<ActionResult<LignePanier>> PostPanier(LignePanier panier)
    {
        _dataRepository.AddAsync(panier);

        return CreatedAtAction("GetPanier", new { id = panier.PanierId }, panier);
    }

    // DELETE: api/Panier/5
    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> DeletePanier(int id)
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