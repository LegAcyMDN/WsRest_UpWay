using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WsRest_UpWay.Models;
using WsRest_UpWay.Models.EntityFramework;

namespace WsRest_UpWay.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MagasinsController : ControllerBase
{
    private readonly S215UpWayContext _context;

    public MagasinsController(S215UpWayContext context)
    {
        _context = context;
    }

    // GET: api/Magasin
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Magasin>>> GetMagasin()
    {
        return await _context.Magasins.ToListAsync();
    }

    // GET: api/Magasin/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Magasin>> GetMagasin(int id)
    {
        var magasin = await _context.Magasins.FindAsync(id);

        if (magasin == null) return NotFound();

        return magasin;
    }

    // PUT: api/Magasin/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    [Authorize(Policy = Policies.Admin)]
    public async Task<IActionResult> PutMagasin(int id, Magasin magasin)
    {
        if (id != magasin.Idmagasin) return BadRequest();

        _context.Entry(magasin).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!MagasinExists(id)) return NotFound();

            throw;
        }

        return NoContent();
    }

    // POST: api/Magasin
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    [Authorize(Policy = Policies.Admin)]
    public async Task<ActionResult<Magasin>> PostMagasin(Magasin magasin)
    {
        _context.Magasins.Add(magasin);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetAccessoire", new { id = magasin.Idmagasin }, magasin);
    }

    // DELETE: api/Magasin/5
    [HttpDelete("{id}")]
    [Authorize(Policy = Policies.Admin)]
    public async Task<IActionResult> DeleteMagasin(int id)
    {
        var magasin = await _context.Magasins.FindAsync(id);
        if (magasin == null) return NotFound();

        _context.Magasins.Remove(magasin);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool MagasinExists(int id)
    {
        return _context.Magasins.Any(e => e.Idmagasin == id);
    }
}