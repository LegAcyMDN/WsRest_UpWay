using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WsRest_UpWay.Models;
using WsRest_UpWay.Models.EntityFramework;
using WsRest_UpWay.Models.Repository;

namespace WsRest_UpWay.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccessoiresController : ControllerBase
{
    private readonly IDataRepository<Accessoire> dataRepository;

    public AccessoiresController(IDataRepository<Accessoire> dataRepo)
    {
        dataRepository = dataRepo;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Accessoire>>> GetAccessoires()
    {
        return await dataRepository.GetAllAsync();
    }

    [HttpGet]
    [Route("[action]/{id}")]
    [ActionName("GetById")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Accessoire>> GetAccessoire(int id)
    {
        var accessoire = await dataRepository.GetByIdAsync(id);

        if (accessoire == null)
            return NotFound();

        return accessoire;
    }

    [HttpGet]
    [Route("[action]/{nom}")]
    [ActionName("GetByNom")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Accessoire>> GetAccessoireByNom(string nom)
    {
        var accessoire = await dataRepository.GetByStringAsync(nom);
        if (accessoire == null)
            return NotFound();

        return accessoire;
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize(Policy = Policies.Admin)]
    public async Task<IActionResult> PutAccessoire(int id, Accessoire accessoire)
    {
        if (id != accessoire.Idaccessoire)
            return BadRequest();

        var accToUpdate = await dataRepository.GetByIdAsync(id);

        if (accToUpdate == null)
            return NotFound();
        await dataRepository.UpdateAsync(accToUpdate.Value, accessoire);
        return NoContent();
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize(Policy = Policies.Admin)]
    public async Task<ActionResult<Accessoire>> PostAccessoire(Accessoire accessoire)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        await dataRepository.AddAsync(accessoire);

        return CreatedAtAction("GetById", new { id = accessoire.Idaccessoire }, accessoire);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize(Policy = Policies.Admin)]
    public async Task<IActionResult> DeleteAccessoire(int id)
    {
        var accessoire = await dataRepository.GetByIdAsync(id);
        if (accessoire == null)
            return NotFound();

        await dataRepository.DeleteAsync(accessoire.Value);
        return NoContent();
    }
}