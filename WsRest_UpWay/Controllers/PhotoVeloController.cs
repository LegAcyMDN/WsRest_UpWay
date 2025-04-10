using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WsRest_UpWay.Models.EntityFramework;
using WsRest_UpWay.Models.Repository;
using WsRest_UpWay.Models;

namespace WsRest_UpWay.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PhotoVelosController : ControllerBase
{
    private readonly IDataRepository<PhotoVelo> _dataRepository;

    public PhotoVelosController(IDataRepository<PhotoVelo> dataRepository)
    {
        _dataRepository = dataRepository;
    }

    [HttpGet]
    [Authorize(Policy = Policies.Admin)]
    public async Task<ActionResult<IEnumerable<PhotoVelo>>> Gets()
    {
        return await _dataRepository.GetAllAsync();
    }

    [HttpGet("{id}")]
    [Authorize]
    public async Task<ActionResult<PhotoVelo>> Get(int id)
    {
        var photoVelo = await _dataRepository.GetByIdAsync(id);
        if (photoVelo.Value == null)
            return NotFound();

        return photoVelo;
    }

    [HttpPost]
    [Authorize(Policy = Policies.Admin)]
    public async Task<ActionResult<PhotoVelo>> PostPhotoVelo(PhotoVelo photoVelo)
    {
        await _dataRepository.AddAsync(photoVelo);
        return CreatedAtAction("Get", new { id = photoVelo.PhotoVeloId }, photoVelo);
    }

    [HttpPut("{id}")]
    [Authorize(Policy = Policies.Admin)]
    public async Task<IActionResult> PutPhotoVelo(int id, PhotoVelo photoVelo)
    {
        if (id != photoVelo.PhotoVeloId) return BadRequest();
        
        var existingPhoto = await _dataRepository.GetByIdAsync(id);
        if (existingPhoto.Value == null)
            return NotFound();

        await _dataRepository.UpdateAsync(existingPhoto.Value, photoVelo);
        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = Policies.Admin)]
    public async Task<IActionResult> DeletePhotoVelo(int id)
    {
        var photoVelo = await _dataRepository.GetByIdAsync(id);
        if (photoVelo.Value == null)
            return NotFound();

        await _dataRepository.DeleteAsync(photoVelo.Value);
        return NoContent();
    }

    [HttpGet("count")]
    [Authorize(Policy = Policies.Admin)]
    public async Task<ActionResult<int>> Count()
    {
        return await _dataRepository.GetCountAsync();
    }
}