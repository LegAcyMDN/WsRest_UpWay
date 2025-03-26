using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WsRest_UpWay.Models;
using WsRest_UpWay.Models.EntityFramework;
using WsRest_UpWay.Models.Repository;

namespace WsRest_UpWay.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{
    private readonly IDataRepository<Categorie> dataRepository;

    public CategoriesController(IDataRepository<Categorie> dataRepo)
    {
        dataRepository = dataRepo;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<Categorie>>> GetCategoriesArticles()
    {
        return await dataRepository.GetAllAsync();
    }

    [HttpGet]
    [Route("[action]/{id}")]
    [ActionName("GetById")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Categorie>> GetCategorieById(int id)
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
    public async Task<ActionResult<Categorie>> GetCategorieByTitre(string titre)
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
    public async Task<IActionResult> PutCategorie(int id, Categorie cat)
    {
        if (id != cat.CategorieId)
            return BadRequest();

        var comToUpdate = await dataRepository.GetByIdAsync(id);

        if (comToUpdate == null)
            return NotFound();
        await dataRepository.UpdateAsync(comToUpdate.Value, cat);
        return NoContent();
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize(Policy = Policies.Admin)]
    public async Task<ActionResult<Categorie>> PostCategorie(Categorie cat)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        await dataRepository.AddAsync(cat);

        return CreatedAtAction("GetById", new { id = cat.CategorieId }, cat);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize(Policy = Policies.Admin)]
    public async Task<IActionResult> DeleteCategorie(int id)
    {
        var catArticle = await dataRepository.GetByIdAsync(id);
        if (catArticle == null)
            return NotFound();

        await dataRepository.DeleteAsync(catArticle.Value);
        return NoContent();
    }
}