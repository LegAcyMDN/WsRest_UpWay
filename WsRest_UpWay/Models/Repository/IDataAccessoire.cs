using Microsoft.AspNetCore.Mvc;
using WsRest_UpWay.Models.EntityFramework;

namespace WsRest_UpWay.Models.Repository;

public interface IDataAccessoire : IDataRepository<Accessoire>
{
    Task<ActionResult<IEnumerable<Accessoire>>> GetByCategoryPrixAsync(int? categoryName, int min, int max,
        int page = 0);

    Task<ActionResult<IEnumerable<PhotoAccessoire>>> GetPhotosByIdAsync(int id);
}