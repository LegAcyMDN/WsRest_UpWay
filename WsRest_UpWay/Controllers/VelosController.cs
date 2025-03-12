using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WsRest_UpWay.Models.EntityFramework;
using WsRest_UpWay.Models.Repository;

namespace WsRest_UpWay.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VelosController : ControllerBase
    {
        private readonly IDataRepository<Velo> dataRepository;

        public VelosController(IDataRepository<Velo> dataRepo)
        {
            dataRepository = dataRepo;
        }

        // GET: api/Velos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Velo>>> GetVelos()
        {
            return await dataRepository.ToListAsync();
        }

        // GET: api/Velos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Velo>> GetVelo(int id)
        {
            var velo = await dataRepository.FindAsync(id);

            if (velo == null)
            {
                return NotFound();
            }

            return velo;
        }

        // PUT: api/Velos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVelo(int id, Velo velo)
        {
            if (id != velo.Idvelo)
                return BadRequest();

            var comToUpdate = await dataRepository.GetByIdAsync(id);

            if (comToUpdate == null)
                return NotFound();
            else
            {
                await dataRepository.UpdateAsync(comToUpdate.Value, velo);
                return NoContent();
            }
        }

        // POST: api/Velos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Velo>> PostVelo(Velo velo)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await dataRepository.AddAsync(velo);

            return CreatedAtAction("GetById", new { id = velo.Idvelo }, velo);
        }

        // DELETE: api/Velos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVelo(int id)
        {
            var velo = await dataRepository.GetByIdAsync(id);
            if (velo == null)
                return NotFound();

            await dataRepository.DeleteAsync(velo.Value);
            return NoContent();
        }
    }
}
