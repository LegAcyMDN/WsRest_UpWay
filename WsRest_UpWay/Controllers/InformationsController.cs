using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WsRest_UpWay.Models;
using WsRest_UpWay.Models.EntityFramework;
using WsRest_UpWay.Models.Repository;

namespace WsRest_UpWay.Controllers;

[Route("api/[controller]")]
[ApiController]
public class InformationsController : ControllerBase
{
    private readonly IDataRepository<Information> _context;

    public InformationsController(IDataRepository<Information> context)
    {
        _context = context;
    }

    // GET: api/Information
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<Information>>> GetInformations()
    {
        return await _context.GetAllAsync();
    }

    // GET: api/Information/5
    [HttpGet]
    [Route("[action]/{id}")]
    [ActionName("GetById")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Information>> GetInformation(int id)
    {
        var information = await _context.GetByIdAsync(id);

        if (information == null)
            return NotFound();

        return information;
    }

    [HttpGet]
    [Route("[action]/{mode}")]
    [ActionName("GetByMode")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Information>> GetInformationMode(string mode)
    {
        var information = await _context.GetByStringAsync(mode);
        if (information == null)
            return NotFound();

        return information;
    }

    // PUT: api/Information/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize(Policy = Policies.Admin)]
    public async Task<IActionResult> PutInformation(int id, Information information)
    {
        if (id != information.InformationId)
            return BadRequest();

        var comToUpdate = await _context.GetByIdAsync(id);

        if (comToUpdate == null)
            return NotFound();
        await _context.UpdateAsync(comToUpdate.Value, information);
        return NoContent();
    }

    // POST: api/Information
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize(Policy = Policies.Admin)]
    public async Task<ActionResult<Information>> PostInformation(Information information)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        await _context.AddAsync(information);

        return CreatedAtAction("GetById", new { id = information.InformationId }, information);
    }

    // DELETE: api/Information/5
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize(Policy = Policies.Admin)]
    public async Task<IActionResult> DeleteInformation(int id)
    {
        var information = await _context.GetByIdAsync(id);
        if (information == null)
            return NotFound();

        await _context.DeleteAsync(information.Value);
        return NoContent();
    }
    /*
    private bool InformationExists(int id)
    {
        return _context.Informations.Any(e => e.Idinformations == id);
    }
    */
}