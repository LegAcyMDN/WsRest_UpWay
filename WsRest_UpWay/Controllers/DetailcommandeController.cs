﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WsRest_UpWay.Models.EntityFramework;
using WsRest_UpWay.Models.Repository;

namespace WsRest_UpWay.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetailcommandeController : ControllerBase
    {
        private readonly IDataRepository<Detailcommande> dataRepository;

        public DetailcommandeController(IDataRepository<Detailcommande> dataRepo)
        {
            dataRepository = dataRepo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Detailcommande>>> GetDetailCommandes()
        {
            return await dataRepository.GetAllAsync();
        }

        [HttpGet]
        [Route("[action]/{id}")]
        [ActionName("GetById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Detailcommande>> GetDetailCommande(int id)
        {
            var commande = await dataRepository.GetByIdAsync(id);

            if (commande == null)
                return NotFound();

            return commande;
        }

        [HttpGet]
        [Route("[action]/{mode}")]
        [ActionName("GetByMode")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Detailcommande>> GetDetailCommandeByModeLivraison(string mode)
        {
            var commande = await dataRepository.GetByStringAsync(mode);
            if (commande == null)
                return NotFound();

            return commande;
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutDetailCommande(int id, Detailcommande detailCommande)
        {
            if (id != detailCommande.Idcommande)
                return BadRequest();

            var comToUpdate = await dataRepository.GetByIdAsync(id);

            if (comToUpdate == null)
                return NotFound();
            else
            {
                await dataRepository.UpdateAsync(comToUpdate.Value, detailCommande);
                return NoContent();
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Detailcommande>> PostDetailCommande(Detailcommande detailCommande)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await dataRepository.AddAsync(detailCommande);

            return CreatedAtAction("GetById", new { id = detailCommande.Idcommande }, detailCommande);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteDetailCommande(int id)
        {
            var commande = await dataRepository.GetByIdAsync(id);
            if (commande == null)
                return NotFound();

            await dataRepository.DeleteAsync(commande.Value);
            return NoContent();
        }
    }
}
