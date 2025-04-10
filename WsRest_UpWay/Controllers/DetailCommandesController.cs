﻿using Microsoft.AspNetCore.Mvc;
using WsRest_UpWay.Models.EntityFramework;
using WsRest_UpWay.Models.Repository;

namespace WsRest_UpWay.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DetailCommandesController : ControllerBase
{
    private readonly IDataRepository<DetailCommande> dataRepository;

    public DetailCommandesController(IDataRepository<DetailCommande> dataRepo)
    {
        dataRepository = dataRepo;
    }

    /// <summary>
    ///     Récupère tous les détails de commandes.
    /// </summary>
    /// <returns>Http response</returns>
    /// <response code="200">Lorsque la liste des détails de commandes est récupérée avec succès.</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<DetailCommande>>> GetDetailCommandes(int page = 0)
    {
        return await dataRepository.GetAllAsync(page);
    }

    /// <summary>
    ///     Récupère un détail de commande par son identifiant.
    /// </summary>
    /// <param name="id">L'identifiant du détail de commande.</param>
    /// <returns>Http response</returns>
    /// <response code="200">Lorsque le détail de commande est trouvé.</response>
    /// <response code="404">Lorsque l'identifiant du détail de commande n'est pas trouvé.</response>
    [HttpGet]
    [Route("[action]/{id}")]
    [ActionName("GetById")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<DetailCommande>> GetDetailCommande(int id)
    {
        var commande = await dataRepository.GetByIdAsync(id);

        if (commande.Value == null)
            return NotFound();

        return commande;
    }

    /// <summary>
    ///     Met à jour un détail de commande existant.
    /// </summary>
    /// <param name="id">L'identifiant du détail de commande à mettre à jour.</param>
    /// <param name="detailCommande">L'objet détail de commande avec les nouvelles valeurs.</param>
    /// <returns>Http response</returns>
    /// <response code="204">Lorsque la mise à jour est réussie.</response>
    /// <response code="404">Lorsque le détail de commande à mettre à jour n'est pas trouvé.</response>
    /// <response code="400">Lorsque l'identifiant du détail de commande ne correspond pas à l'objet fourni.</response>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> PutDetailCommande(int id, DetailCommande detailCommande)
    {
        if (id != detailCommande.CommandeId)
            return BadRequest();

        var comToUpdate = await dataRepository.GetByIdAsync(id);

        if (comToUpdate.Value == null)
            return NotFound();


        await dataRepository.UpdateAsync(comToUpdate.Value, detailCommande);
        return NoContent();
    }

    /// <summary>
    ///     Crée un nouveau détail de commande.
    /// </summary>
    /// <param name="detailCommande">L'objet détail de commande à créer.</param>
    /// <returns>Http response</returns>
    /// <response code="201">Lorsque le détail de commande est créé avec succès.</response>
    /// <response code="400">Lorsque le modèle du détail de commande est invalide.</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<DetailCommande>> PostDetailCommande(DetailCommande detailCommande)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        await dataRepository.AddAsync(detailCommande);

        return CreatedAtAction("GetById", new { id = detailCommande.CommandeId }, detailCommande);
    }

    /// <summary>
    ///     Supprime un détail de commande par son identifiant.
    /// </summary>
    /// <param name="id">L'identifiant du détail de commande à supprimer.</param>
    /// <returns>Http response</returns>
    /// <response code="204">Lorsque la suppression est réussie.</response>
    /// <response code="404">Lorsque le détail de commande à supprimer n'est pas trouvé.</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteDetailCommande(int id)
    {
        var commande = await dataRepository.GetByIdAsync(id);
        if (commande.Value == null)
            return NotFound();

        await dataRepository.DeleteAsync(commande.Value);
        return NoContent();
    }
}