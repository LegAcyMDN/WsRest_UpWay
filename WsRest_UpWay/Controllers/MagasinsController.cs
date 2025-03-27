using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WsRest_UpWay.Models;
using WsRest_UpWay.Models.EntityFramework;
using WsRest_UpWay.Models.Repository;

namespace WsRest_UpWay.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MagasinsController : ControllerBase
{
    private readonly IDataRepository<Magasin> dataRepository;

    public MagasinsController(IDataRepository<Magasin> dataRepo)
    {
        dataRepository = dataRepo;
    }

    /// <summary>
    ///     Récupère tous les magasins.
    /// </summary>
    /// <returns>Http response</returns>
    /// <response code="200">Lorsque la liste des magasins est récupérée avec succès.</response>
    // GET: api/Magasin
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<Magasin>>> GetMagasins(int page = 0)
    {
        return await dataRepository.GetAllAsync(page);
    }

    /// <summary>
    ///     Récupère un magasin par son identifiant.
    /// </summary>
    /// <param name="id">L'identifiant du magasin.</param>
    /// <returns>Http response</returns>
    /// <response code="200">Lorsque le magasin est trouvé.</response>
    /// <response code="404">Lorsque l'identifiant du magasin n'est pas trouvé.</response>
    // GET: api/Magasin/5
    [HttpGet]
    [Route("[action]/{id}")]
    [ActionName("GetById")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Magasin>> GetMagasin(int id)
    {
        var magasin = await dataRepository.GetByIdAsync(id);

        if (magasin.Value == null) return NotFound();

        return magasin;
    }

    [HttpGet]
    [Route("[action]/{nom}")]
    [ActionName("GetByNom")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Magasin>> GetMagasinByNom(string nom)
    {
        var magasin = await dataRepository.GetByStringAsync(nom);
        if (magasin.Value == null)
            return NotFound();

        return magasin;
    }

    /// <summary>
    ///     Met à jour un magasin existant.
    /// </summary>
    /// <param name="id">L'identifiant du magasin à mettre à jour.</param>
    /// <param name="magasin">L'objet magasin avec les nouvelles valeurs.</param>
    /// <returns>Http response</returns>
    /// <response code="204">Lorsque la mise à jour est réussie.</response>
    /// <response code="404">Lorsque le magasin à mettre à jour n'est pas trouvé.</response>
    /// <response code="400">Lorsque l'identifiant du magasin ne correspond pas à l'objet fourni.</response>
    // PUT: api/Magasin/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize(Policy = Policies.Admin)]
    public async Task<IActionResult> PutMagasin(int id, Magasin magasin)
    {
        if (id != magasin.MagasinId) return BadRequest();

        var comtoUpdate = await dataRepository.GetByIdAsync(id);

        if (comtoUpdate.Value == null) return NotFound();

        await dataRepository.UpdateAsync(comtoUpdate.Value, magasin);
        return NoContent();
    }

    /// <summary>
    ///     Crée un nouveau magasin.
    /// </summary>
    /// <param name="magasin">L'objet magasin à créer.</param>
    /// <returns>Http response</returns>
    /// <response code="201">Lorsque le magasin est créé avec succès.</response>
    /// <response code="400">Lorsque le modèle du magasin est invalide.</response>
    // POST: api/Magasin
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Magasin>> PostMagasin(Magasin magasin)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        await dataRepository.AddAsync(magasin);

        return CreatedAtAction(nameof(GetMagasin), new { id = magasin.MagasinId }, magasin);
    }

    /// <summary>
    ///     Supprime un magasin par son identifiant.
    /// </summary>
    /// <param name="id">L'identifiant du magasin à supprimer.</param>
    /// <returns>Http response</returns>
    /// <response code="204">Lorsque la suppression est réussie.</response>
    /// <response code="404">Lorsque le magasin à supprimer n'est pas trouvé.</response>
    // DELETE: api/Magasin/5
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteMagasin(int id)
    {
        var magasin = await dataRepository.GetByIdAsync(id);
        if (magasin.Value == null) return NotFound();

        await dataRepository.DeleteAsync(magasin.Value);
        return NoContent();
    }
}