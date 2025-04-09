using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WsRest_UpWay.Models;
using WsRest_UpWay.Models.EntityFramework;
using WsRest_UpWay.Models.Repository;

namespace WsRest_UpWay.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PhotoAccessoiresController : ControllerBase
{
    private readonly IDataRepository<PhotoAccessoire> _dataRepository;

    public PhotoAccessoiresController(IDataRepository<PhotoAccessoire> dataRepository)
    {
        _dataRepository = dataRepository;
    }

    [HttpGet]
    [Authorize(Policy = Policies.Admin)]
    public async Task<ActionResult<IEnumerable<PhotoAccessoire>>> Gets()
    {
        return await _dataRepository.GetAllAsync();
    }

    [HttpGet("{id}")]
    [Authorize]
    public async Task<ActionResult<PhotoAccessoire>> Get(int id)
    {
        var photoAccessoire = await _dataRepository.GetByIdAsync(id);
        if (photoAccessoire.Value == null)
            return NotFound();

        return photoAccessoire;
    }

    [HttpPost]
    [Authorize(Policy = Policies.Admin)]
    public async Task<ActionResult<PhotoAccessoire>> PostPhotoAccessoire(PhotoAccessoire photoAccessoire)
    {
        await _dataRepository.AddAsync(photoAccessoire);
        return CreatedAtAction("Get", new { id = photoAccessoire.PhotoAcessoireId }, photoAccessoire);
    }

    [HttpPut("{id}")]
    [Authorize(Policy = Policies.Admin)]
    public async Task<IActionResult> PutPhotoAccessoire(int id, PhotoAccessoire photoAccessoire)
    {
        var existingPhoto = await _dataRepository.GetByIdAsync(id);
        if (existingPhoto.Value == null)
            return NotFound();

        await _dataRepository.UpdateAsync(existingPhoto.Value, photoAccessoire);
        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = Policies.Admin)]
    public async Task<IActionResult> DeletePhotoAccessoire(int id)
    {
        var photoAccessoire = await _dataRepository.GetByIdAsync(id);
        if (photoAccessoire.Value == null)
            return NotFound();

        await _dataRepository.DeleteAsync(photoAccessoire.Value);
        return NoContent();
    }

    [HttpGet("count")]
    [Authorize(Policy = Policies.Admin)]
    public async Task<ActionResult<int>> Count()
    {
        return await _dataRepository.GetCountAsync();
    }
}
