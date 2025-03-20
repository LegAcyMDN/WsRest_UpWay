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

    /// <summary>
    /// Récupère tous les articles.
    /// </summary>
    /// <returns>Http response</returns>
    /// <response code="200">Lorsque la liste des articles est récupérée avec succès.</response>
    // GET: api/Velos
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<Article>>> GetArticles()
    {
        return await dataRepository.GetAllAsync();
    }

    /// <summary>
    /// Récupère un article par son identifiant.
    /// </summary>
    /// <param name="id">L'identifiant de l'article.</param>
    /// <returns>Http response</returns>
    /// <response code="200">Lorsque l'article est trouvé.</response>
    /// <response code="404">Lorsque l'identifiant de l'article n'est pas trouvé.</response>
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

    /// <summary>
    /// Récupère un article par son nom.
    /// </summary>
    /// <param name="nom">Le nom de l'article.</param>
    /// <returns>Http response</returns>
    /// <response code="200">Lorsque l'article est trouvé.</response>
    /// <response code="404">Lorsque l'article avec le nom spécifié n'est pas trouvé.</response>
    [HttpGet]
    [Route("[action]/{nom}")]
    [ActionName("GetByName")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Article>> GetArticlebyTitreArticle(string nom)
    {
        var article = await dataRepository.GetByStringAsync(nom);
        if (article == null)
            return NotFound();

        return article;
    }

    /// <summary>
    /// Met à jour un article existant.
    /// </summary>
    /// <param name="id">L'identifiant de l'article à mettre à jour.</param>
    /// <param name="article">L'objet article avec les nouvelles valeurs.</param>
    /// <returns>Http response</returns>
    /// <response code="204">Lorsque la mise à jour est réussie.</response>
    /// <response code="404">Lorsque l'article à mettre à jour n'est pas trouvé.</response>
    /// <response code="400">Lorsque l'identifiant de l'article ne correspond pas à l'objet fourni.</response>
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

    /// <summary>
    /// Crée un nouvel article.
    /// </summary>
    /// <param name="article">L'objet article à créer.</param>
    /// <returns>Http response</returns>
    /// <response code="201">Lorsque l'article est créé avec succès.</response>
    /// <response code="400">Lorsque le modèle de l'article est invalide.</response>
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

    /// <summary>
    /// Supprime un article par son identifiant.
    /// </summary>
    /// <param name="id">L'identifiant de l'article à supprimer.</param>
    /// <returns>Http response</returns>
    /// <response code="204">Lorsque la suppression est réussie.</response>
    /// <response code="404">Lorsque l'article à supprimer n'est pas trouvé.</response>
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
