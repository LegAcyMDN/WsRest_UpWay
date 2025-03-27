using Microsoft.AspNetCore.Mvc;
using WsRest_UpWay.Models.EntityFramework;

namespace WsRest_UpWay.Models.Repository;

public interface IDataAccessoire : IDataRepository<Accessoire>
{
    Task<ActionResult<IEnumerable<Accessoire>>> GetByCategoryAsync(string categoryName, int page = 0);

    Task<ActionResult<IEnumerable<Accessoire>>> GetByCategoryPrixAsync(string categoryName, int min, int max,
        int page = 0);

    Task<ActionResult<IEnumerable<Accessoire>>> GetByPrixAsync(int min, int max, int page = 0);

    Task<ActionResult<IEnumerable<PhotoAccessoire>>> GetPhotosByIdAsync(int id);
}