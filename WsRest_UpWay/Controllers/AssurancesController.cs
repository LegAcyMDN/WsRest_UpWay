using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WsRest_UpWay.Models;
using WsRest_UpWay.Models.EntityFramework;
using WsRest_UpWay.Models.Repository;

namespace WsRest_UpWay.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AssurancesController : ControllerBase
{
    private readonly IDataRepository<Assurance> _dataRepository;

    public AssurancesController(IDataRepository<Assurance> dataRepository)
    {
        _dataRepository = dataRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Assurance>>> Gets()
    {
        return await _dataRepository.GetAllAsync();
    }

    [HttpGet]
    [Route("[action]/{id}")]
    [ActionName("GetById")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Assurance>> GetAssuranceById(int id)
    {
        var assurance = await _dataRepository.GetByIdAsync(id);
        if (assurance.Value == null)
            return NotFound();

        return assurance;
    }

    [HttpGet]
    [Route("[action]/{nom}")]
    [ActionName("GetByName")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Assurance>> GetAssuranceByTitre(string titre)
    {
        var assurance = await _dataRepository.GetByStringAsync(titre);
        if (assurance.Value == null)
            return NotFound();

        return assurance;
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize(Policy = Policies.Admin)]
    public async Task<IActionResult> PutAssurance(int id, Assurance assurance)
    {
        if (id != assurance.AssuranceId)
            return BadRequest();

        var assToUpdate = await _dataRepository.GetByIdAsync(id);

        if (assToUpdate.Value == null)
            return NotFound();
        await _dataRepository.UpdateAsync(assToUpdate.Value, assurance);
        return NoContent();
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize(Policy = Policies.Admin)]
    public async Task<ActionResult<Assurance>> PostAssurance(Assurance assurance)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        await _dataRepository.AddAsync(assurance);

        return CreatedAtAction("GetById", new { id = assurance.AssuranceId }, assurance);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize(Policy = Policies.Admin)]
    public async Task<IActionResult> DeleteAssurance(int id)
    {
        var assurance = await _dataRepository.GetByIdAsync(id);
        if (assurance.Value == null)
            return NotFound();

        await _dataRepository.DeleteAsync(assurance.Value);
        return NoContent();
    }
}
