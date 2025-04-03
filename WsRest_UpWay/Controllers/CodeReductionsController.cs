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
    public class CodeReductionsController : ControllerBase
    {
        private readonly S215UpWayContext _context;

        public CodeReductionsController(S215UpWayContext context)
        {
            _context = context;
        }

        // GET: api/CodeReductions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CodeReduction>>> GetCodereductions()
        {
            return await _context.Codereductions.ToListAsync();
        }

        // GET: api/CodeReductions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CodeReduction>> GetCodeReduction(string id)
        {
            var codeReduction = await _context.Codereductions.FindAsync(id);

            if (codeReduction == null)
            {
                return NotFound();
            }

            return codeReduction;
        }

        // PUT: api/CodeReductions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCodeReduction(string id, CodeReduction codeReduction)
        {
            if (id != codeReduction.ReductionId)
            {
                return BadRequest();
            }

            _context.Entry(codeReduction).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CodeReductionExists(id))
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

        // POST: api/CodeReductions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CodeReduction>> PostCodeReduction(CodeReduction codeReduction)
        {
            _context.Codereductions.Add(codeReduction);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CodeReductionExists(codeReduction.ReductionId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCodeReduction", new { id = codeReduction.ReductionId }, codeReduction);
        }

        // DELETE: api/CodeReductions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCodeReduction(string id)
        {
            var codeReduction = await _context.Codereductions.FindAsync(id);
            if (codeReduction == null)
            {
                return NotFound();
            }

            _context.Codereductions.Remove(codeReduction);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CodeReductionExists(string id)
        {
            return _context.Codereductions.Any(e => e.ReductionId == id);
        }
    }
}
