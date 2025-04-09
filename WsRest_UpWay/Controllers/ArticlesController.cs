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
    public class ArticlesController : ControllerBase
    {
        private readonly IDataArticles _dataRepository;

        public ArticlesController(IDataArticles dataRepository)
        {
            _dataRepository = dataRepository;
        }

        // GET: api/Articles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Article>>> GetArticles(int page = 0)
        {
            return await _dataRepository.GetAllAsync(page);
        }

        // GET: api/Articles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Article>> GetArticle(int id)
        {
            var article = await _dataRepository.GetByIdAsync(id);

            if (article == null)
            {
                return NotFound();
            }

            return article;
        }
        [HttpGet]
        [Route("[action]/{id}")]
        [ActionName("GetByCategoryId")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<Article>>> GetByCategoryId(int id)
        {
            var article = await _dataRepository.GetByCategoryIdAsync(id);

            if (article.Value == null)
                return NotFound();

            return article;
        }


        // PUT: api/Articles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutArticle(int id, Article article)
        {
            if (id != article.ArticleId)
            {
                return BadRequest();
            }
            var artToUpdate = await _dataRepository.GetByIdAsync(id);

            if (artToUpdate.Value == null)
                return NotFound();
            await _dataRepository.UpdateAsync(artToUpdate.Value, article);
            return NoContent();
        }

        // POST: api/Articles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Article>> PostArticle(Article article)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _dataRepository.AddAsync(article);

            return CreatedAtAction("Getarticle", new { id = article.ArticleId }, article);
        }

        // DELETE: api/Articles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArticle(int id)
        {
            var article = await _dataRepository.GetByIdAsync(id);
            if (article.Value == null)
            {
                return NotFound();
            }

            await _dataRepository.DeleteAsync(article.Value);
            return NoContent();
        }
    }
}
