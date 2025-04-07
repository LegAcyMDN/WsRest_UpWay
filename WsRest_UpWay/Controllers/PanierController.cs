using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WsRest_UpWay.Helpers;
using WsRest_UpWay.Models;
using WsRest_UpWay.Models.EntityFramework;
using WsRest_UpWay.Models.Repository;

namespace WsRest_UpWay.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PanierController : ControllerBase
{
    private readonly IDataPanier _dataRepository;

    public PanierController(IDataPanier dataRepository)
    {
        _dataRepository = dataRepository;
    }

    // GET: api/Panier
    [HttpGet]
    [Authorize(Policy = Policies.Admin)]
    public async Task<ActionResult<IEnumerable<Panier>>> GetPaniers()
    {
        return await _dataRepository.GetAllAsync();
    }

    // GET: api/Panier/5
    [HttpGet("[action]/{id}")]
    [ActionName("GetById")]
    [Authorize]
    public async Task<ActionResult<Panier>> GetById(int id)
    {
        var panier = await _dataRepository.GetByIdAsync(id);
        if (panier.Value == null) return NotFound();

        if (panier.Value.ClientId != User.GetId()) return Unauthorized();

        return panier;
    }

    // GET: api/Panier/5
    [HttpGet("[action]")]
    [ActionName("GetMine")]
    [Authorize]
    public async Task<ActionResult<Panier>> GetMyPanier()
    {
        var panier = await _dataRepository.GetByUser(User.GetId());
        if (panier.Value == null) return NotFound();

        return panier;
    }

    // PUT: api/Panier/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    [Authorize]
    public async Task<IActionResult> PutPanier(int id, Panier panier)
    {
        if (id != panier.PanierId) return BadRequest();

        var panier1 = await _dataRepository.GetByIdAsync(id);
        if (panier1.Value == null) return NotFound();
        if (panier1.Value.ClientId != User.GetId()) return Unauthorized();

        await _dataRepository.UpdateAsync(panier1.Value, panier);

        return NoContent();
    }

    // POST: api/Panier
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Panier>> PostPanier(Panier panier)
    {
        _dataRepository.AddAsync(panier);

        return CreatedAtAction("GetById", new { id = panier.PanierId }, panier);
    }

    // DELETE: api/Panier/5
    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> DeletePanier(int id)
    {
        var panier = await _dataRepository.GetByIdAsync(id);
        if (panier.Value == null) return NotFound();
        if (panier.Value.ClientId != User.GetId()) return Unauthorized();

        await _dataRepository.DeleteAsync(panier.Value);

        return NoContent();
    }
}