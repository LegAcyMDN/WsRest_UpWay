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
    public class ContenuArticlesController : ControllerBase
    {
        private readonly S215UpWayContext _context;

        public ContenuArticlesController(S215UpWayContext context)
        {
            _context = context;
        }

        // GET: api/ContenuArticles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContenuArticle>>> GetContenuArticles()
        {
            return await _context.ContenuArticles.ToListAsync();
        }

        // GET: api/ContenuArticles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ContenuArticle>> GetContenuArticle(int id)
        {
            var contenuArticle = await _context.ContenuArticles.FindAsync(id);

            if (contenuArticle == null)
            {
                return NotFound();
            }

            return contenuArticle;
        }

        // PUT: api/ContenuArticles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContenuArticle(int id, ContenuArticle contenuArticle)
        {
            if (id != contenuArticle.ContenueId)
            {
                return BadRequest();
            }

            _context.Entry(contenuArticle).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContenuArticleExists(id))
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

        // POST: api/ContenuArticles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ContenuArticle>> PostContenuArticle(ContenuArticle contenuArticle)
        {
            _context.ContenuArticles.Add(contenuArticle);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetContenuArticle", new { id = contenuArticle.ContenueId }, contenuArticle);
        }

        // DELETE: api/ContenuArticles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContenuArticle(int id)
        {
            var contenuArticle = await _context.ContenuArticles.FindAsync(id);
            if (contenuArticle == null)
            {
                return NotFound();
            }

            _context.ContenuArticles.Remove(contenuArticle);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ContenuArticleExists(int id)
        {
            return _context.ContenuArticles.Any(e => e.ContenueId == id);
        }
    }
}
