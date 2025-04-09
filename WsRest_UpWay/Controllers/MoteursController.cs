using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WsRest_UpWay.Models;
using WsRest_UpWay.Models.EntityFramework;
using WsRest_UpWay.Models.Repository;

namespace WsRest_UpWay.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoteursController : ControllerBase
    {
        private readonly IDataRepository<Moteur> dataRepository;

        public MoteursController(IDataRepository<Moteur> dataRepo)
        {
            dataRepository = dataRepo;
        }

        // GET: api/Moteurs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Moteur>>> GetMoteurs()
        {
            return await dataRepository.GetAllAsync();
        }

        // GET: api/Moteurs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Moteur>> GetMoteur(int id)
        {
            var moteur = await dataRepository.GetByIdAsync(id);

            if (moteur.Value == null)
            {
                return NotFound();
            }

            return moteur;
        }

        // PUT: api/Moteurs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Policy = Policies.Admin)]
        public async Task<IActionResult> PutMoteur(int id, Moteur catArticle)
        {
            if (id != catArticle.MoteurId)
                return BadRequest();

            var comToUpdate = await dataRepository.GetByIdAsync(id);

            if (comToUpdate.Value == null)
                return NotFound();
            await dataRepository.UpdateAsync(comToUpdate.Value, catArticle);
            return NoContent();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Policy = Policies.Admin)]
        public async Task<ActionResult<CategorieArticle>> PostMoteur(Moteur catArticle)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await dataRepository.AddAsync(catArticle);

            return CreatedAtAction("GetById", new { id = catArticle.MoteurId }, catArticle);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Policy = Policies.Admin)]
        public async Task<IActionResult> DeleteMoteur(int id)
        {
            var catArticle = await dataRepository.GetByIdAsync(id);
            if (catArticle.Value == null)
                return NotFound();

            await dataRepository.DeleteAsync(catArticle.Value);
            return NoContent();
        }
    }
}
