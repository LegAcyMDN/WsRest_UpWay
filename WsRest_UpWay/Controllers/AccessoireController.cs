using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WsRest_UpWay.Models.EntityFramework;

namespace WsRest_UpWay.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccessoireController : ControllerBase
    {
        private readonly S215UpWayContext _context;

        public AccessoireController(S215UpWayContext context)
        {
            _context = context;
        }

        // GET: api/Accessoire
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Accessoire>>> GetAccessoire()
        {
            return await _context.Accessoires.ToListAsync();
        }

        // GET: api/Accessoire/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Accessoire>> GetAccessoire(int id)
        {
            var accessoire = await _context.Accessoires.FindAsync(id);

            if (accessoire == null)
            {
                return NotFound();
            }

            return accessoire;
        }

        // PUT: api/Accessoire/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAccessoire(int id, Accessoire accessoire)
        {
            if (id != accessoire.Idaccessoire)
            {
                return BadRequest();
            }

            _context.Entry(accessoire).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccessoireExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Accessoire
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Accessoire>> PostAccessoire(Accessoire accessoire)
        {
            _context.Accessoires.Add(accessoire);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAccessoire", new { id = accessoire.Idaccessoire }, accessoire);
        }

        // DELETE: api/Accessoire/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccessoire(int id)
        {
            var accessoire = await _context.Accessoires.FindAsync(id);
            if (accessoire == null)
            {
                return NotFound();
            }

            _context.Accessoires.Remove(accessoire);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AccessoireExists(int id)
        {
            return _context.Accessoires.Any(e => e.Idaccessoire == id);
        }
    }
}
