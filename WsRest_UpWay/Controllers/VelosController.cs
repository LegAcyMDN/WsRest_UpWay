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

    /// <summary>
    /// Récupère tous les vélos.
    /// </summary>
    /// <returns>Une liste de vélos.</returns>
    // GET: api/Velos
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Velo>>> GetVelos()
    {
        return await dataRepository.GetAllAsync();
    }

    /// <summary>
    /// Récupère un vélo par son identifiant.
    /// </summary>
    /// <param name="id">L'identifiant du vélo à récupérer.</param>
    /// <returns>Le vélo correspondant à l'identifiant.</returns>
    /// <response code="200">Lorsque le vélo est trouvé.</response>
    /// <response code="404">Lorsque le vélo n'est pas trouvé.</response>
    // GET: api/Velos/5
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
    /// Récupère des vélos en fonction de plusieurs filtres.
    /// </summary>
    /// <param name="taille">La taille du vélo.</param>
    /// <param name="categorie">La catégorie du vélo.</param>
    /// <param name="cara">Caractéristiques supplémentaires.</param>
    /// <param name="marque">La marque du vélo.</param>
    /// <param name="annee">L'année de fabrication.</param>
    /// <param name="kilom">Le kilométrage.</param>
    /// <param name="posmot">Position du mot.</param>
    /// <param name="motmar">Mot de la marque.</param>
    /// <param name="couplemot">Couple de mots.</param>
    /// <param name="capbat">Capacité de la batterie.</param>
    /// <param name="posbat">Position de la batterie.</param>
    /// <param name="batamo">Batterie amovible.</param>
    /// <param name="posbag">Position du bagage.</param>
    /// <param name="poids">Le poids du vélo.</param>
    /// <returns>Une liste de vélos correspondant aux filtres.</returns>
    /// <response code="200">Lorsque les vélos sont trouvés.</response>
    /// <response code="404">Lorsque aucun vélo n'est trouvé.</response>
    [HttpGet]
    [Route("[action]/{taille}")]
    [ActionName("GetByFilters")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<Velo>>> GetByFiltres(string taille, int categorie, int cara, int marque, int annee, string kilom, string posmot, string motmar, string couplemot, string capbat, string posbat, string batamo, string posbag, decimal poids)
    {
        var velo = await dataRepository.GetByFiltresAsync(taille,categorie, cara, marque, annee ,kilom, posmot,  motmar,  couplemot,  capbat, posbat, batamo, posbag, poids);
        if (velo == null)
            return NotFound();

        return velo;
    }

    /// <summary>
    /// Met à jour un vélo existant.
    /// </summary>
    /// <param name="id">L'identifiant du vélo à mettre à jour.</param>
    /// <param name="velo">L'objet vélo avec les nouvelles valeurs.</param>
    /// <returns>Http response.</returns>
    /// <response code="204">Lorsque la mise à jour est réussie.</response>
    /// <response code="404">Lorsque le vélo à mettre à jour n'est pas trouvé.</response>
    /// <response code="400">Lorsque l'identifiant du vélo ne correspond pas à l'objet fourni.</response>
    // PUT: api/Velos/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
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

    /// <summary>
    /// Crée un nouveau vélo.
    /// </summary>
    /// <param name="velo">L'objet vélo à créer.</param>
    /// <returns>Http response avec le vélo créé.</returns>
    /// <response code="201">Lorsque le vélo est créé avec succès.</response>
    /// <response code="400">Lorsque les données fournies ne sont pas valides.</response>
    // POST: api/Velos
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
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

    /// <summary>
    /// Supprime un vélo existant.
    /// </summary>
    /// <param name="id">L'identifiant du vélo à supprimer.</param>
    /// <returns>Http response.</returns>
    /// <response code="204">Lorsque la suppression est réussie.</response>
    /// <response code="404">Lorsque le vélo à supprimer n'est pas trouvé.</response>
    // DELETE: api/Velos/5
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
