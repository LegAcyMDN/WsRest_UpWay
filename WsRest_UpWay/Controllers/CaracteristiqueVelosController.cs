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
    public class CaracteristiqueVelosController : ControllerBase
    {
        private readonly S215UpWayContext _context;

        public CaracteristiqueVelosController(S215UpWayContext context)
        {
            _context = context;
        }

        // GET: api/CaracteristiqueVeloes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CaracteristiqueVelo>>> GetCaracteristiquevelos()
        {
            return await _context.Caracteristiquevelos.ToListAsync();
        }

        // GET: api/CaracteristiqueVeloes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CaracteristiqueVelo>> GetCaracteristiqueVelo(int id)
        {
            var caracteristiqueVelo = await _context.Caracteristiquevelos.FindAsync(id);

            if (caracteristiqueVelo == null)
            {
                return NotFound();
            }

            return caracteristiqueVelo;
        }

        // PUT: api/CaracteristiqueVeloes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCaracteristiqueVelo(int id, CaracteristiqueVelo caracteristiqueVelo)
        {
            if (id != caracteristiqueVelo.CaracteristiqueVeloId)
            {
                return BadRequest();
            }

            _context.Entry(caracteristiqueVelo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CaracteristiqueVeloExists(id))
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

        // POST: api/CaracteristiqueVeloes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CaracteristiqueVelo>> PostCaracteristiqueVelo(CaracteristiqueVelo caracteristiqueVelo)
        {
            _context.Caracteristiquevelos.Add(caracteristiqueVelo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCaracteristiqueVelo", new { id = caracteristiqueVelo.CaracteristiqueVeloId }, caracteristiqueVelo);
        }

        // DELETE: api/CaracteristiqueVeloes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCaracteristiqueVelo(int id)
        {
            var caracteristiqueVelo = await _context.Caracteristiquevelos.FindAsync(id);
            if (caracteristiqueVelo == null)
            {
                return NotFound();
            }

            _context.Caracteristiquevelos.Remove(caracteristiqueVelo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CaracteristiqueVeloExists(int id)
        {
            return _context.Caracteristiquevelos.Any(e => e.CaracteristiqueVeloId == id);
        }
    }
}
