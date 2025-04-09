using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WsRest_UpWay.Models.EntityFramework;
using WsRest_UpWay.Models.Repository;

namespace WsRest_UpWay.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompteClientsController : ControllerBase
    {
        private readonly IDataRepository<CompteClient> _context;

        public CompteClientsController(IDataRepository<CompteClient> context)
        {
            _context = context;
        }

        // GET: api/CompteClients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CompteClient>>> GetCompteclients()
        {
            return await _context.GetAllAsync();
        }

        // GET: api/CompteClients/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CompteClient>> GetCompteClient(int id)
        {
            var compteClient = await _context.GetByIdAsync(id);

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

            var compToUpdate = await _context.GetByIdAsync(id);

            if (compToUpdate.Value == null)
                return NotFound();


            await _context.UpdateAsync(compToUpdate.Value, compteClient);
            return NoContent();
        }

        // POST: api/CompteClients
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CompteClient>> PostCompteClient(CompteClient compteClient)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _context.AddAsync(compteClient);

            return CreatedAtAction("GetById", new { id = compteClient.ClientId }, compteClient);
        }

        // DELETE: api/CompteClients/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompteClient(int id)
        {
            var compteClient = await _context.GetByIdAsync(id);
            if (compteClient == null)
            {
                return NotFound();
            }

            _context.DeleteAsync(compteClient.Value);

            return NoContent();
        }
    }
}
