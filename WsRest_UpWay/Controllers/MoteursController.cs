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
    public class MoteursController : ControllerBase
    {
        private readonly S215UpWayContext _context;

        public MoteursController(S215UpWayContext context)
        {
            _context = context;
        }

        // GET: api/Moteurs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Moteur>>> GetMoteurs()
        {
            return await _context.Moteurs.ToListAsync();
        }

        // GET: api/Moteurs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Moteur>> GetMoteur(int id)
        {
            var moteur = await _context.Moteurs.FindAsync(id);

            if (moteur == null)
            {
                return NotFound();
            }

            return moteur;
        }

        // PUT: api/Moteurs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMoteur(int id, Moteur moteur)
        {
            if (id != moteur.MoteurId)
            {
                return BadRequest();
            }

            _context.Entry(moteur).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MoteurExists(id))
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

        // POST: api/Moteurs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Moteur>> PostMoteur(Moteur moteur)
        {
            _context.Moteurs.Add(moteur);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMoteur", new { id = moteur.MoteurId }, moteur);
        }

        // DELETE: api/Moteurs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMoteur(int id)
        {
            var moteur = await _context.Moteurs.FindAsync(id);
            if (moteur == null)
            {
                return NotFound();
            }

            _context.Moteurs.Remove(moteur);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MoteurExists(int id)
        {
            return _context.Moteurs.Any(e => e.MoteurId == id);
        }
    }
}
