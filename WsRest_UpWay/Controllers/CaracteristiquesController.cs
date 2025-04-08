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
    public class CaracteristiquesController : ControllerBase
    {
        private readonly IDataRepository<Caracteristique> _dataRepository;

        public CaracteristiquesController(IDataRepository<Caracteristique> dataRepository)
        {
            _dataRepository = dataRepository;
        }

        // GET: api/Caracteristiques
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Caracteristique>>> GetCaracteristiques()
        {
            return await _dataRepository.GetAllAsync();
        }

        // GET: api/Caracteristiques/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Caracteristique>> GetCaracteristique(int id)
        {
            var caracteristique = await _dataRepository.GetByIdAsync(id);

            if (caracteristique == null)
            {
                return NotFound();
            }

            return caracteristique;
        }

        // PUT: api/Caracteristiques/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCaracteristique(int id, Caracteristique caracteristique)
        {
            if (id != caracteristique.CaracteristiqueId)
            {
                return BadRequest();
            }

            var carToUpdate = await _dataRepository.GetByIdAsync(id);

            if (carToUpdate.Value == null)
                return NotFound();
            await _dataRepository.UpdateAsync(carToUpdate.Value, caracteristique);
            return NoContent();
        }

        // POST: api/Caracteristiques
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Caracteristique>> PostCaracteristique(Caracteristique caracteristique)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _dataRepository.AddAsync(caracteristique);

            return CreatedAtAction("GetById", new { id = caracteristique.CaracteristiqueId }, caracteristique);
        }

        // DELETE: api/Caracteristiques/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCaracteristique(int id)
        {
            var caracteristique = await _dataRepository.GetByIdAsync(id);
            if (caracteristique.Value == null)
                return NotFound();

            await _dataRepository.DeleteAsync(caracteristique.Value);
            return NoContent();
        }
    }
}
