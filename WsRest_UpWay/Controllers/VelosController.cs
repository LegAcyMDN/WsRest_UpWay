using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WsRest_UpWay.Models;
using WsRest_UpWay.Models.EntityFramework;
using WsRest_UpWay.Models.Repository;

namespace WsRest_UpWay.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VelosController : ControllerBase
{
    private readonly IDataRepository<Velo> dataRepository;

    public VelosController(IDataRepository<Velo> dataRepo)
    {
        dataRepository = dataRepo;
    }

    // GET: api/Velos
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Velo>>> GetVelos()
    {
        return await dataRepository.GetAllAsync();
    }

    // GET: api/Velos/5
    [HttpGet]
    [Route("[action]/{id}")]
    [ActionName("GetById")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Velo>> GetVelo(int id)
    {
        var velo = await dataRepository.GetByIdAsync(id);
        if (velo == null)
            return NotFound();

        return velo;
    }

    [HttpGet]
    [Route("[action]/{nom}")]
    [ActionName("GetByName")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Velo>> GetInformationMode(string nom)
    {
        var velo = await dataRepository.GetByStringAsync(nom);
        if (velo == null)
            return NotFound();

        return velo;
    }

    // PUT: api/Velos/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize(Policy = Policies.Admin)]
    public async Task<IActionResult> PutVelo(int id, Velo velo)
    {
        if (id != velo.Idvelo)
            return BadRequest();

        var comToUpdate = await dataRepository.GetByIdAsync(id);

        if (comToUpdate == null)
            return NotFound();
        await dataRepository.UpdateAsync(comToUpdate.Value, velo);
        return NoContent();
    }

    // POST: api/Velos
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize(Policy = Policies.Admin)]
    public async Task<ActionResult<Velo>> PostVelo(Velo velo)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        await dataRepository.AddAsync(velo);

        return CreatedAtAction("GetById", new { id = velo.Idvelo }, velo);
    }

    // DELETE: api/Velos/5
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize(Policy = Policies.Admin)]
    public async Task<IActionResult> DeleteVelo(int id)
    {
        var velo = await dataRepository.GetByIdAsync(id);
        if (velo == null)
            return NotFound();

        await dataRepository.DeleteAsync(velo.Value);
        return NoContent();
    }
}
