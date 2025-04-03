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
    public class CompteClientsController : ControllerBase
    {
        private readonly S215UpWayContext _context;

        public CompteClientsController(S215UpWayContext context)
        {
            _context = context;
        }

        // GET: api/CompteClients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CompteClient>>> GetCompteclients()
        {
            return await _context.Compteclients.ToListAsync();
        }

        // GET: api/CompteClients/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CompteClient>> GetCompteClient(int id)
        {
            var compteClient = await _context.Compteclients.FindAsync(id);

            if (compteClient == null)
            {
                return NotFound();
            }

            return compteClient;
        }

        // PUT: api/CompteClients/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCompteClient(int id, CompteClient compteClient)
        {
            if (id != compteClient.ClientId)
            {
                return BadRequest();
            }

            _context.Entry(compteClient).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CompteClientExists(id))
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

        // POST: api/CompteClients
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CompteClient>> PostCompteClient(CompteClient compteClient)
        {
            _context.Compteclients.Add(compteClient);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCompteClient", new { id = compteClient.ClientId }, compteClient);
        }

        // DELETE: api/CompteClients/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompteClient(int id)
        {
            var compteClient = await _context.Compteclients.FindAsync(id);
            if (compteClient == null)
            {
                return NotFound();
            }

            _context.Compteclients.Remove(compteClient);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CompteClientExists(int id)
        {
            return _context.Compteclients.Any(e => e.ClientId == id);
        }
    }
}
