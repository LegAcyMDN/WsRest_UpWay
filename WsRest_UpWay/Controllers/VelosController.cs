using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WsRest_UpWay.Models;
using WsRest_UpWay.Models.EntityFramework;
using WsRest_UpWay.Models.Repository;

namespace WsRest_UpWay.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VelosController : ControllerBase
{
    private readonly IDataVelo dataRepository;

    public VelosController(IDataVelo dataRepo)
    {
        dataRepository = dataRepo;
    }

    // GET: api/Velos
    /// <summary>
    /// Récupère tous les Velos.
    /// </summary>
    /// <returns>Http response</returns>
    /// <response code="200">Lorsque la liste des Velos est récupérée avec succès.</response>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Velo>>> GetVelos()
    {
        return await dataRepository.GetAllAsync();
    }

    // GET: api/Velos/5
    /// <summary>
    /// Récupère un Velo par son identifiant.
    /// </summary>
    /// <param name="id">L'identifiant du Velo.</param>
    /// <returns>Http response</returns>
    /// <response code="200">Lorsque le Velo est trouvé.</response>
    /// <response code="404">Lorsque l'identifiant du Velo n'est pas trouvé.</response>
    [HttpGet]
    [Route("[action]/{id}")]
    [ActionName("GetById")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Velo>> GetVelo(int id)
    {
        var velo = await dataRepository.GetByIdAsync(id);
        if (velo == null)
            return NotFound();

        return velo;
    }

    /// <summary>
    /// Récupère un Velo via plien de filtre.
    /// </summary>
    /// <param name="taille">La taille du Velo.</param>
    /// <returns>Http response</returns>
    /// <response code="200">Lorsque le Velo est trouvé.</response>
    /// <response code="404">Lorsque le Velo avec les filtre spécifié n'est pas trouvé.</response>
    [HttpGet]
    [Route("[action]/{filtre}")]
    [ActionName("GetByFilters")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<Velo>>> GetVeloByFiltres(string taille, int categorie, int cara, int marque, int annee, string kilom, string posmot, string motmar, string couplemot, string capbat, string posbat, string batamo, string posbag, decimal poids)
    {
        var velo = await dataRepository.GetByFiltresAsync(taille,categorie, cara, marque, annee ,kilom, posmot,  motmar,  couplemot,  capbat, posbat, batamo, posbag, poids);
        if (velo == null)
            return NotFound();

        return velo;
    }

    // PUT: api/Velos/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    /// <summary>
    /// Met à jour un velo existant.
    /// </summary>
    /// <param name="id">L'identifiant du velo à mettre à jour.</param>
    /// <param name="accessoire">L'objet velo avec les nouvelles valeurs.</param>
    /// <returns>Http response</returns>
    /// <response code="204">Lorsque la mise à jour est réussie.</response>
    /// <response code="404">Lorsque le velo à mettre à jour n'est pas trouvé.</response>
    /// <response code="400">Lorsque l'identifiant du velo ne correspond pas à l'objet fourni.</response>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize(Policy = Policies.Admin)]
    public async Task<IActionResult> PutVelo(int id, Velo velo)
    {
        if (id != velo.VeloId)
            return BadRequest();

        var velToUpdate = await dataRepository.GetByIdAsync(id);

        if (velToUpdate == null)
            return NotFound();
        await dataRepository.UpdateAsync(velToUpdate.Value, velo);
        return NoContent();
    }

    // POST: api/Velos
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    /// <summary>
    /// Crée un nouvel velo.
    /// </summary>
    /// <param name="velo">L'objet velo à créer.</param>
    /// <returns>Http response</returns>
    /// <response code="201">Lorsque le velo est créé avec succès.</response>
    /// <response code="400">Lorsque le modèle du velo est invalide.</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize(Policy = Policies.Admin)]
    public async Task<ActionResult<Velo>> PostVelo(Velo velo)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        await dataRepository.AddAsync(velo);

        return CreatedAtAction("GetById", new { id = velo.VeloId }, velo);
    }

    // DELETE: api/Velos/5
    /// <summary>
    /// Supprime un velo par son identifiant.
    /// </summary>
    /// <param name="id">L'identifiant du velo à supprimer.</param>
    /// <returns>Http response</returns>
    /// <response code="204">Lorsque la suppression est réussie.</response>
    /// <response code="404">Lorsque le velo à supprimer n'est pas trouvé.</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize(Policy = Policies.Admin)]
    public async Task<IActionResult> DeleteVelo(int id)
    {
        var velo = await dataRepository.GetByIdAsync(id);
        if (velo == null)
            return NotFound();

        await dataRepository.DeleteAsync(velo.Value);
        return NoContent();
    }
}
