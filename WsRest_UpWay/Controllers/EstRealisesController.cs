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
    public class EstRealisesController : ControllerBase
    {
       private readonly IDataEstRealise dataRepository;

        public EstRealisesController(IDataEstRealise dataRepo)
        {
            dataRepository = dataRepo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EstRealise>>> GetEstRealises()
        {
            return await dataRepository.GetAllAsync();
        }

        [HttpGet]
        [Route("[action]/{idvelo}/{idinspection}/{idreparation}")]
        [ActionName("GetByIds")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<EstRealise>> GetEstRealiseByIds(int idvelo, int idinspection, int idreparation)
        {
            var res = await dataRepository.GetByIdsAsync(idvelo, idinspection, idreparation);
            if (res.Value == null)
            {
                return NotFound();
            }
            
            return res;
        }


        [HttpGet]
        [Route("[action]/{id}/{type}")]
        [ActionName("GetByVeloId")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<EstRealise>>> GetEstRealiseByVelo(int id, string type)
        {
            var res = await dataRepository.GetByIdVeloAsync(id, type);
            if (res.Value == null)
            {
                return NotFound();
            }
            
            return res;
        }


        // PUT: api/Velos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Policy = Policies.Admin)]
        public async Task<IActionResult> PutEstRealise(int idvelo, int idinspection, int idreparation, EstRealise estRealise)
        {
            if (idvelo != estRealise.VeloId)
                return BadRequest();
            if (idinspection != estRealise.InspectionId)
                return BadRequest();
            if (idreparation != estRealise.ReparationId)
                return BadRequest();

            var realiseToUpdate = await dataRepository.GetByIdsAsync(idvelo, idinspection, idreparation);

            if (realiseToUpdate.Value == null)
                return NotFound();
            await dataRepository.UpdateAsync(realiseToUpdate.Value, estRealise);
            return NoContent();
        }

        // POST: api/Velos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Policy = Policies.Admin)]
        public async Task<ActionResult<Velo>> PostVelo(EstRealise realise)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await dataRepository.AddAsync(realise);

            return CreatedAtAction("GetByIds", new { idvelo = realise.VeloId, idinspection = realise.InspectionId, idreparation = realise.ReparationId }, realise);
        }

        // DELETE: api/Velos/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Policy = Policies.Admin)]
        public async Task<IActionResult> DeleteVelo(int id)
        {
            var velo = await dataRepository.GetByIdAsync(id);
            if (velo.Value == null)
                return NotFound();

            await dataRepository.DeleteAsync(velo.Value);
            return NoContent();
        }
    }
}
