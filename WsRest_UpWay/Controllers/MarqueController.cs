using Microsoft.AspNetCore.Mvc;
using WsRest_UpWay.Models.EntityFramework;
using WsRest_UpWay.Models.Repository;

namespace WsRest_UpWay.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarqueController : ControllerBase
    {
        private readonly IDataRepository<Marque> dataRepository;

        public MarqueController(IDataRepository<Marque> dataRepo)
        {
            dataRepository = dataRepo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Marque>>> GetMarques()
        {
            return await dataRepository.GetAllAsync();
        }

        [HttpGet]
        [Route("[action]/{id}")]
        [ActionName("GetById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Marque>> GetMarque(int id)
        {
            var marque = await dataRepository.GetByIdAsync(id);

            if (marque == null)
                return NotFound();

            return marque;
        }

        [HttpGet]
        [Route("[action]/{nom}")]
        [ActionName("GetByNom")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Marque>> GetMarqueByNom(string nom)
        {
            var marque = await dataRepository.GetByStringAsync(nom);
            if (marque == null)
                return NotFound();

            return marque;
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutMarque(int id, Marque marque)
        {
            if (id != marque.Idmarque)
                return BadRequest();

            var accToUpdate = await dataRepository.GetByIdAsync(id);

            if (accToUpdate == null)
                return NotFound();
            else
            {
                await dataRepository.UpdateAsync(accToUpdate.Value, marque);
                return NoContent();
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Marque>> PostMarque(Marque marque)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await dataRepository.AddAsync(marque);

            return CreatedAtAction("GetById", new { id = marque.Idmarque }, marque);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteMarque(int id)
        {
            var accessoire = await dataRepository.GetByIdAsync(id);
            if (accessoire == null)
                return NotFound();

            await dataRepository.DeleteAsync(accessoire.Value);
            return NoContent();
        }
    }
}
