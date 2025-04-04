using Microsoft.AspNetCore.Mvc;
using WsRest_UpWay.Models.EntityFramework;

namespace WsRest_UpWay.Models.Repository;
public interface IDataContenuArticles : IDataRepository<ContenuArticle>
{
    Task<ActionResult<IEnumerable<ContenuArticle>>> GetByArticleIdAsync(int id);
}
