using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WsRest_UpWay.Models;
using WsRest_UpWay.Models.EntityFramework;
using WsRest_UpWay.Models.Repository;

namespace WsRest_UpWay.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ArticleController : ControllerBase
{
    private readonly IDataRepository<Article> dataRepository;

    public ArticleController(IDataRepository<Article> dataRepo)
    {
        dataRepository = dataRepo;
    }

    // GET: api/Velos
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Article>>> GetArticles()
    {
        return await dataRepository.GetAllAsync();
    }

    // GET: api/Articles/5
    [HttpGet]
    [Route("[action]/{id}")]
    [ActionName("GetById")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Article>> GetArticle(int id)
    {
        var article = await dataRepository.GetByIdAsync(id);
        if (article == null)
            return NotFound();

        return article;
    }

    [HttpGet]
    [Route("[action]/{nom}")]
    [ActionName("GetByName")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Article>> GetInformationMode(string nom)
    {
        var article = await dataRepository.GetByStringAsync(nom);
        if (article == null)
            return NotFound();

        return article;
    }

    // PUT: api/Velos/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize(Policy = Policies.Admin)]
    public async Task<IActionResult> PutArticle(int id, Article article)
    {
        if (id != article.ArticleId)
            return BadRequest();

        var comToUpdate = await dataRepository.GetByIdAsync(id);

        if (comToUpdate == null)
            return NotFound();
        await dataRepository.UpdateAsync(comToUpdate.Value, article);
        return NoContent();
    }

    // POST: api/Velos
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize(Policy = Policies.Admin)]
    public async Task<ActionResult<Article>> PostArticle(Article article)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        await dataRepository.AddAsync(article);

        return CreatedAtAction("GetById", new { id = article.ArticleId }, article);
    }

    // DELETE: api/Velos/5
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize(Policy = Policies.Admin)]
    public async Task<IActionResult> DeleteArticle(int id)
    {
        var article = await dataRepository.GetByIdAsync(id);
        if (article == null)
            return NotFound();

        await dataRepository.DeleteAsync(article.Value);
        return NoContent();
    }
}
