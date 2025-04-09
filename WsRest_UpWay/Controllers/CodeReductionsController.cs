using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WsRest_UpWay.Models.Repository;
using WsRest_UpWay.Models.EntityFramework;

namespace WsRest_UpWay.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CodeReductionsController : ControllerBase
    {
        private readonly  IDataCodeReduction<CodeReduction>_context;

        public CodeReductionsController(IDataCodeReduction<CodeReduction> context)
        {
            _context = context;
        }

        // GET: api/CodeReductions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CodeReduction>>> GetCodereductions()
        {
            return await _context.GetAllAsync();
        }

        // GET: api/CodeReductions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CodeReduction>> GetCodeReduction(string id)
        {
            var codeReduction = await _context.GetByStringAsync(id);

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

            var codToUpdate = await _context.GetByStringAsync(id);

            if (codToUpdate.Value == null)
                return NotFound();
            _context.UpdateAsync(codToUpdate.Value, codeReduction);
            return NoContent();
        }

        // POST: api/CodeReductions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CodeReduction>> PostCodeReduction(CodeReduction codeReduction)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.AddAsync(codeReduction);

            return CreatedAtAction("GetCodeReduction", new { id = codeReduction.ReductionId }, codeReduction);
        }

        // DELETE: api/CodeReductions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCodeReduction(string id)
        {
            var codeReduction = await _context.GetByStringAsync(id);
            if (codeReduction == null)
            {
                return NotFound();
            }

            _context.DeleteAsync(codeReduction.Value);
            return NoContent();
        }
    }
}
