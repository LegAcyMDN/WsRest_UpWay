using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WsRest_UpWay.Models.EntityFramework;
using WsRest_UpWay.Models.Repository;

namespace WsRest_UpWay.Models.DataManager
{
    public class ArticleManager : IDataRepository<Article>
    {
        readonly S215UpWayContext s215UpWayContext;

        public ArticleManager() { }

        public ArticleManager(S215UpWayContext context)
        {
            s215UpWayContext = context;
        }

        public async Task AddAsync(Article art)
        {
            await s215UpWayContext.Articles.AddAsync(art);
            await s215UpWayContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Article art)
        {
            s215UpWayContext.Articles.Remove(art);
            await s215UpWayContext.SaveChangesAsync();
        }

        public async Task<ActionResult<IEnumerable<Article>>> GetAllAsync()
        {
            return await s215UpWayContext.Articles.ToListAsync();
        }

        public async Task<ActionResult<Article>> GetByIdAsync(int id)
        {
            return await s215UpWayContext.Articles.FirstOrDefaultAsync(u => u.ArticleId == id);
        }

        public async Task<ActionResult<Article>> GetByStringAsync(string str)
        {
            return await s215UpWayContext.Articles
                    .Include(a => a.ArticleCategorieArt)
                    .FirstOrDefaultAsync(u => u.ArticleCategorieArt.TitreCategorieArticle.ToUpper() == str.ToLower());
        }

        public async Task UpdateAsync(Article artToUpdate, Article art)
        {
            s215UpWayContext.Entry(artToUpdate).State = EntityState.Modified;
            artToUpdate.ArticleId = art.ArticleId;
            artToUpdate.CategorieArticleId = art.ArticleId;
            await s215UpWayContext.SaveChangesAsync();
        }
    }
}
