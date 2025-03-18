using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WsRest_UpWay.Models;
using WsRest_UpWay.Models.EntityFramework;
using WsRest_UpWay.Models.Repository;

namespace WsRest_UpWay.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MarquesController : ControllerBase
{
    private readonly IDataRepository<Marque> dataRepository;

    public MarquesController(IDataRepository<Marque> dataRepo)
    {
        dataRepository = dataRepo;
    }

    /// <summary>
    /// Récupère toutes les marques.
    /// </summary>
    /// <returns>Http response</returns>
    /// <response code="200">Lorsque la liste des marques est récupérée avec succès.</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<Marque>>> GetMarques()
    {
        return await dataRepository.GetAllAsync();
    }

    /// <summary>
    /// Récupère une marque par son identifiant.
    /// </summary>
    /// <param name="id">L'identifiant de la marque.</param>
    /// <returns>Http response</returns>
    /// <response code="200">Lorsque la marque est trouvée.</response>
    /// <response code="404">Lorsque l'identifiant de la marque n'est pas trouvé.</response>
    [HttpGet]
    [Route("[action]/{id}")]
    [ActionName("GetById")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Marque>> GetMarque(int id)
    {
        var marque = await dataRepository.GetByIdAsync(id);

        if (marque == null)
            return NotFound();

        return marque;
    }

    /// <summary>
    /// Récupère une marque par son nom.
    /// </summary>
    /// <param name="nom">Le nom de la marque.</param>
    /// <returns>Http response</returns>
    /// <response code="200">Lorsque la marque est trouvée.</response>
    /// <response code="404">Lorsque la marque avec le nom spécifié n'est pas trouvée.</response>
    [HttpGet]
    [Route("[action]/{nom}")]
    [ActionName("GetByNom")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Marque>> GetMarqueByNom(string nom)
    {
        var marque = await dataRepository.GetByStringAsync(nom);
        if (marque == null)
            return NotFound();

        return marque;
    }

    /// <summary>
    /// Met à jour une marque existante.
    /// </summary>
    /// <param name="id">L'identifiant de la marque à mettre à jour.</param>
    /// <param name="marque">L'objet marque avec les nouvelles valeurs.</param>
    /// <returns>Http response</returns>
    /// <response code="204">Lorsque la mise à jour est réussie.</response>
    /// <response code="404">Lorsque la marque à mettre à jour n'est pas trouvée.</response>
    /// <response code="400">Lorsque l'identifiant de la marque ne correspond pas à l'objet fourni.</response>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize(Policy = Policies.Admin)]
    public async Task<IActionResult> PutMarque(int id, Marque marque)
    {
        if (id != marque.MarqueId)
            return BadRequest();

        var accToUpdate = await dataRepository.GetByIdAsync(id);

        if (accToUpdate == null)
            return NotFound();
        await dataRepository.UpdateAsync(accToUpdate.Value, marque);
        return NoContent();
    }

    /// <summary>
    /// Crée une nouvelle marque.
    /// </summary>
    /// <param name="marque">L'objet marque à créer.</param>
    /// <returns>Http response</returns>
    /// <response code="201">Lorsque la marque est créée avec succès.</response>
    /// <response code="400">Lorsque le modèle de la marque est invalide.</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize(Policy = Policies.Admin)]
    public async Task<ActionResult<Marque>> PostMarque(Marque marque)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        await dataRepository.AddAsync(marque);

        return CreatedAtAction("GetById", new { id = marque.MarqueId }, marque);
    }

    /// <summary>
    /// Supprime une marque par son identifiant.
    /// </summary>
    /// <param name="id">L'identifiant de la marque à supprimer.</param>
    /// <returns>Http response</returns>
    /// <response code="204">Lorsque la suppression est réussie.</response>
    /// <response code="404">Lorsque la marque à supprimer n'est pas trouvée.</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize(Policy = Policies.Admin)]
    public async Task<IActionResult> DeleteMarque(int id)
    {
        var accessoire = await dataRepository.GetByIdAsync(id);
        if (accessoire == null)
            return NotFound();

        await dataRepository.DeleteAsync(accessoire.Value);
        return NoContent();
    }
}