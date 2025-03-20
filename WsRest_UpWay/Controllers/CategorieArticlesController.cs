using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WsRest_UpWay.Models;
using WsRest_UpWay.Models.EntityFramework;
using WsRest_UpWay.Models.Repository;

namespace WsRest_UpWay.Controllers
{
    public class CategorieArticlesController : ControllerBase
    {
        private readonly IDataRepository<CategorieArticle> dataRepository;

        public CategorieArticlesController(IDataRepository<CategorieArticle> dataRepo)
        {
            dataRepository = dataRepo;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<CategorieArticle>>> GetCategoriesArticles()
        {
            return await dataRepository.GetAllAsync();
        }

        [HttpGet]
        [Route("[action]/{id}")]
        [ActionName("GetById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CategorieArticle>> GetCategorieArticle(int id)
        {
            var catArticle = await dataRepository.GetByIdAsync(id);
            if (catArticle == null)
                return NotFound();

            return catArticle;
        }

        [HttpGet]
        [Route("[action]/{nom}")]
        [ActionName("GetByName")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CategorieArticle>> GetCategorieArticleByTitre(string titre)
        {
            var catArticle = await dataRepository.GetByStringAsync(titre);
            if (catArticle == null)
                return NotFound();

            return catArticle;
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Policy = Policies.Admin)]
        public async Task<IActionResult> PutCategorieArticle(int id, CategorieArticle catArticle)
        {
            if (id != catArticle.CategorieArticleId)
                return BadRequest();

            var comToUpdate = await dataRepository.GetByIdAsync(id);

            if (comToUpdate == null)
                return NotFound();
            await dataRepository.UpdateAsync(comToUpdate.Value, catArticle);
            return NoContent();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Policy = Policies.Admin)]
        public async Task<ActionResult<Article>> PostCategorieArticle(CategorieArticle catArticle)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await dataRepository.AddAsync(catArticle);

            return CreatedAtAction("GetById", new { id = catArticle.CategorieArticleId }, catArticle);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Policy = Policies.Admin)]
        public async Task<IActionResult> DeleteArticle(int id)
        {
            var catArticle = await dataRepository.GetByIdAsync(id);
            if (catArticle == null)
                return NotFound();

            await dataRepository.DeleteAsync(catArticle.Value);
            return NoContent();
        }
    }
}
