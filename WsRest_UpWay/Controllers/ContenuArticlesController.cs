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
    public class ContenuArticlesController : ControllerBase
    {
        private readonly IDataContenuArticles _dataRepository;

        public ContenuArticlesController(IDataContenuArticles dataRepository)
        {
            _dataRepository = dataRepository;
        }

        // GET: api/ContenuArticles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContenuArticle>>> GetContenuArticles(int page = 0)
        {
            return await _dataRepository.GetAllAsync(page);
        }

        // GET: api/ContenuArticles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ContenuArticle>> GetContenuArticle(int id)
        {
            var contenuArticle = await _dataRepository.GetByIdAsync(id);

            if (contenuArticle.Value == null)
            {
                return NotFound();
            }

            return contenuArticle;
        }
        [HttpGet]
        [Route("[action]/{id}")]
        [ActionName("GetByArticleId")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<ContenuArticle>>> GetByArticleId(int id)
        {
            var contenuArticle = await _dataRepository.GetByArticleIdAsync(id);

            if (contenuArticle.Value == null || !contenuArticle.Value.Any())
                return NotFound();

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
            var conToUpdate = await _dataRepository.GetByIdAsync(id);

            if (conToUpdate.Value == null)
                return NotFound();
            _dataRepository.UpdateAsync(conToUpdate.Value, contenuArticle);
            return NoContent();
        }

        // POST: api/ContenuArticles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ContenuArticle>> PostContenuArticle(ContenuArticle contenuArticle)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _dataRepository.AddAsync(contenuArticle);

            return CreatedAtAction("GetContenuArticle", new { id = contenuArticle.ContenueId }, contenuArticle);
        }

        // DELETE: api/ContenuArticles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContenuArticle(int id)
        {
            var contenuArticle = await _dataRepository.GetByIdAsync(id);
            if (contenuArticle.Value == null)
                return NotFound();

            await _dataRepository.DeleteAsync(contenuArticle.Value);
            return NoContent();
        }
    }
}
