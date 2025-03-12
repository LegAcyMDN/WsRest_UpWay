using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WsRest_UpWay.Models;
using WsRest_UpWay.Models.EntityFramework;
using WsRest_UpWay.Models.Repository;

namespace WsRest_UpWay.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MagasinsController : ControllerBase
    {
        private readonly IDataRepository<Magasin> dataRepository;

        public MagasinsController(IDataRepository<Magasin> dataRepo)
        {
            dataRepository = dataRepo;
        }

        // GET: api/Magasin
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Magasin>>> GetMagasins()
        {
            return await dataRepository.GetAllAsync();
        }

        // GET: api/Magasin/5
        [HttpGet]
        [Route("[action]/{id}")]
        [ActionName("GetById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Magasin>> GetMagasin(int id)
        {
            var magasin = await dataRepository.GetByIdAsync(id);

        if (magasin == null) return NotFound();

        return magasin;
    }

    // PUT: api/Magasin/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    [Authorize(Policy = Policies.Admin)]
    public async Task<IActionResult> PutMagasin(int id, Magasin magasin)
    {
        if (id != magasin.MagasinId) return BadRequest();

           var comtoUpdate = await dataRepository.GetByIdAsync(id);

            if(comtoUpdate == null)
            {
                return NotFound();
            }
            else 
            {
                await dataRepository.UpdateAsync(comtoUpdate.Value, magasin);
                return NoContent();
            }
        }

        // POST: api/Magasin
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Magasin>> PostMagasin (Magasin magasin)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await dataRepository.AddAsync(magasin);

            return CreatedAtAction("GetByIdAsync", new { id = magasin.MagasinId }, magasin);
        }

        // DELETE: api/Magasin/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteMagasin(int id)
        {
            var magasin = await dataRepository.GetByIdAsync(id);
            if (magasin == null)
            {
                return NotFound();
            }

             await dataRepository.DeleteAsync(magasin.Value);
            return NoContent();
        }

        private bool MagasinExists(int id)
        {
            return _context.Magasins.Any(e => e.Idmagasin == id);
        }
    }
}
