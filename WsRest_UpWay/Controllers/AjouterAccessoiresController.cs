using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WsRest_UpWay.Helpers;
using WsRest_UpWay.Models.EntityFramework;
using WsRest_UpWay.Models.Repository;

namespace WsRest_UpWay.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AjouterAccessoiresController : ControllerBase
{
    private readonly IDataRepository<AjouterAccessoire> _dataRepository;
    private readonly IDataPanier _panierRepository;

    public AjouterAccessoiresController(IDataRepository<AjouterAccessoire> dataRepository, IDataPanier panierRepository)
    {
        _dataRepository = dataRepository;
        _panierRepository = panierRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AjouterAccessoire>>> Gets()
    {
        return await _dataRepository.GetAllAsync();
    }

    [HttpGet("{id}")]
    [Authorize]
    public async Task<ActionResult<AjouterAccessoire>> Get(int id)
    {
        var accessoire = await _dataRepository.GetByIdAsync(id);
        if (accessoire.Value == null) return NotFound();

        var panier = await _panierRepository.GetByIdAsync(accessoire.Value.PanierId);
        if (panier.Value == null) return NotFound();
        if (panier.Value.ClientId != User.GetId()) return Unauthorized();

        return accessoire;
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<IActionResult> Put(int id, AjouterAccessoire body)
    {
        if (id != body.PanierId) return BadRequest();

        var accessoire = await _dataRepository.GetByIdAsync(id);
        if (accessoire.Value == null) return NotFound();

        var panier = await _panierRepository.GetByIdAsync(accessoire.Value.PanierId);
        if (panier.Value == null) return NotFound();
        if (panier.Value.ClientId != User.GetId()) return Unauthorized();

        await _dataRepository.UpdateAsync(accessoire.Value, body);

        return NoContent();
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<AjouterAccessoire>> PostAjouterAccessoire(AjouterAccessoire ajoutAccessoire)
    {
        var panier = await _panierRepository.GetByIdAsync(ajoutAccessoire.PanierId);
        if (panier.Value == null) return NotFound();
        if (panier.Value.ClientId != User.GetId()) return Unauthorized();

        await _dataRepository.AddAsync(ajoutAccessoire);

        return CreatedAtAction("GetAjouterAccessoire", new { id = ajoutAccessoire.AccessoireId }, panier);
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> DeleteAjouterAccessoire(int id)
    {
        var accessoire = await _dataRepository.GetByIdAsync(id);
        if (accessoire.Value == null) return NotFound();

        var panier = await _panierRepository.GetByIdAsync(accessoire.Value.PanierId);
        if (panier.Value == null) return NotFound();
        if (panier.Value.ClientId != User.GetId()) return Unauthorized();

        await _dataRepository.DeleteAsync(accessoire.Value);

        return NoContent();
    }
}
