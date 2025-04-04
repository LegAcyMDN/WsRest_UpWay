using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WsRest_UpWay.Models;
using WsRest_UpWay.Models.EntityFramework;
using WsRest_UpWay.Models.Repository;

namespace WsRest_UpWay.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccessoiresController : ControllerBase
{
    private readonly IDataAccessoire dataRepository;

    public AccessoiresController(IDataAccessoire dataRepo)
    {
        dataRepository = dataRepo;
    }

    /// <summary>
    ///     Récupère tous les accessoires.
    /// </summary>
    /// <returns>Http response</returns>
    /// <response code="200">Lorsque la liste des accessoires est récupérée avec succès.</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<Accessoire>>> GetAccessoires(int page = 0)
    {
        return await dataRepository.GetAllAsync(page);
    }

    [HttpGet("count")]
    public async Task<ActionResult<int>> GetCount()
    {
        return await dataRepository.GetCountAsync();
    }

    /// <summary>
    ///     Récupère un accessoire par son identifiant.
    /// </summary>
    /// <param name="id">L'identifiant de l'accessoire.</param>
    /// <returns>Http response</returns>
    /// <response code="200">Lorsque l'accessoire est trouvé.</response>
    /// <response code="404">Lorsque l'identifiant de l'accessoire n'est pas trouvé.</response>
    [HttpGet]
    [Route("[action]/{id}")]
    [ActionName("GetById")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Accessoire>> GetAccessoire(int id)
    {
        var accessoire = await dataRepository.GetByIdAsync(id);

        if (accessoire.Value == null)
            return NotFound();

        return accessoire;
    }

    /// <summary>
    ///     Récupère un accessoire par son identifiant.
    /// </summary>
    /// <param name="id">L'identifiant de l'accessoire.</param>
    /// <returns>Http response</returns>
    /// <response code="200">Lorsque l'accessoire est trouvé.</response>
    /// <response code="404">Lorsque l'identifiant de l'accessoire n'est pas trouvé.</response>
    [HttpGet]
    [Route("[action]/{id}")]
    [ActionName("GetPhotosById")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<PhotoAccessoire>>> GetPhotosById(int id)
    {
        var accessoire = await dataRepository.GetPhotosByIdAsync(id);

        if (accessoire.Value == null)
            return NotFound();

        return accessoire;
    }


    // Batch version of GetPhotosById
    [HttpPost]
    [Route("[action]")]
    [ActionName("GetPhotosByIds")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Dictionary<int, IEnumerable<PhotoAccessoire>>>> GetPhotosById(IEnumerable<int> ids)
    {
        var photos = new Dictionary<int, IEnumerable<PhotoAccessoire>>();

        foreach (var id in ids)
        {
            var photosaccessoire = await dataRepository.GetPhotosByIdAsync(id);
            if (photosaccessoire.Value != null) photos.Add(id, photosaccessoire.Value);
        }

        return photos;
    }

    /// <summary>
    ///     Récupère un accessoire par son prix et sa category.
    /// </summary>
    /// <param name="category">La category de l'accessoire.</param>
    /// <param name="min">Le prix min de l'accessoire.</param>
    /// <param name="max">Le prix max de l'accessoire.</param>
    /// <returns>Http response</returns>
    /// <response code="200">Lorsque l'accessoire est trouvé.</response>
    /// <response code="404">Lorsque l'accessoire avec le prix et la category spécifié n'est pas trouvé.</response>
    [HttpGet]
    [Route("[action]")]
    [ActionName("GetByCategoryPrix")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<Accessoire>>> GetAccessoireByCategoryPrix(int? categoryid, int min,
        int max, int page = 0)
    {
        var accessoire = await dataRepository.GetByCategoryPrixAsync(categoryid, min, max, page);
        if (accessoire.Value == null)
            return NotFound();

        return accessoire;
    }

    /// <summary>
    ///     Met à jour un accessoire existant.
    /// </summary>
    /// <param name="id">L'identifiant de l'accessoire à mettre à jour.</param>
    /// <param name="accessoire">L'objet accessoire avec les nouvelles valeurs.</param>
    /// <returns>Http response</returns>
    /// <response code="204">Lorsque la mise à jour est réussie.</response>
    /// <response code="404">Lorsque l'accessoire à mettre à jour n'est pas trouvé.</response>
    /// <response code="400">Lorsque l'identifiant de l'accessoire ne correspond pas à l'objet fourni.</response>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize(Policy = Policies.Admin)]
    public async Task<IActionResult> PutAccessoire(int id, Accessoire accessoire)
    {
        if (id != accessoire.AccessoireId)
            return BadRequest();

        var accToUpdate = await dataRepository.GetByIdAsync(id);

        if (accToUpdate.Value == null)
            return NotFound();
        await dataRepository.UpdateAsync(accToUpdate.Value, accessoire);
        return NoContent();
    }

    /// <summary>
    ///     Crée un nouvel accessoire.
    /// </summary>
    /// <param name="accessoire">L'objet accessoire à créer.</param>
    /// <returns>Http response</returns>
    /// <response code="201">Lorsque l'accessoire est créé avec succès.</response>
    /// <response code="400">Lorsque le modèle de l'accessoire est invalide.</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize(Policy = Policies.Admin)]
    public async Task<ActionResult<Accessoire>> PostAccessoire(Accessoire accessoire)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        await dataRepository.AddAsync(accessoire);

        return CreatedAtAction("GetById", new { id = accessoire.AccessoireId }, accessoire);
    }

    // DELETE: api/Accessoires/5
    /// <summary>
    ///     Supprime un accessoire par son identifiant.
    /// </summary>
    /// <param name="id">L'identifiant de l'accessoire à supprimer.</param>
    /// <returns>Http response</returns>
    /// <response code="204">Lorsque la suppression est réussie.</response>
    /// <response code="404">Lorsque l'accessoire à supprimer n'est pas trouvé.</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize(Policy = Policies.Admin)]
    public async Task<IActionResult> DeleteAccessoire(int id)
    {
        var accessoire = await dataRepository.GetByIdAsync(id);
        if (accessoire.Value == null)
            return NotFound();

        await dataRepository.DeleteAsync(accessoire.Value);
        return NoContent();
    }
}