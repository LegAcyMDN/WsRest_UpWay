﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WsRest_UpWay.Models.Repository;
using WsRest_UpWay.Models.EntityFramework;

namespace WsRest_UpWay.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CaracteristiqueVelosController : ControllerBase
    {
        private readonly IDataRepository<CaracteristiqueVelo> _context;

        public CaracteristiqueVelosController(IDataRepository<CaracteristiqueVelo> context)
        {
            _context = context;
        }

        // GET: api/CaracteristiqueVeloes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CaracteristiqueVelo>>> GetCaracteristiquevelos()
        {
            return await _context.GetAllAsync();
        }

        // GET: api/CaracteristiqueVeloes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CaracteristiqueVelo>> GetCaracteristiqueVelo(int id)
        {
            var caracteristiqueVelo = await _context.GetByIdAsync(id);

            if (caracteristiqueVelo == null)
            {
                return NotFound();
            }

            return caracteristiqueVelo;
        }

        // PUT: api/CaracteristiqueVeloes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCaracteristiqueVelo(int id, CaracteristiqueVelo caracteristiqueVelo)
        {
            if (id != caracteristiqueVelo.CaracteristiqueVeloId)
            {
                return BadRequest();
            }

            var cavToUpdate = await _context.GetByIdAsync(id);

            if (cavToUpdate.Value == null)
                return NotFound();
            _context.UpdateAsync(cavToUpdate.Value, caracteristiqueVelo);
            return NoContent();
        }

        // POST: api/CaracteristiqueVeloes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CaracteristiqueVelo>> PostCaracteristiqueVelo(CaracteristiqueVelo caracteristiqueVelo)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.AddAsync(caracteristiqueVelo);

            return CreatedAtAction("GetCaracteristique", new { id = caracteristiqueVelo.CaracteristiqueVeloId }, caracteristiqueVelo);
        }

        // DELETE: api/CaracteristiqueVeloes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCaracteristiqueVelo(int id)
        {
            var caracteristiqueVelo = await _context.GetByIdAsync(id);
            if (caracteristiqueVelo == null)
            {
                return NotFound();
            }

            _context.DeleteAsync(caracteristiqueVelo.Value);
            return NoContent();
        }
    }
}
