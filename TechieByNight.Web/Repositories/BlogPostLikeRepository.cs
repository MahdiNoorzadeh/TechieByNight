
using Microsoft.EntityFrameworkCore;
using TechieByNight.Web.Data;
using TechieByNight.Web.Models.Domain;

namespace TechieByNight.Web.Repositories
{
    public class BlogPostLikeRepository : IBlogPostLikeRepository
    {
        private readonly TechiByNightDbContext _techiByNightDbContext;

        public BlogPostLikeRepository(TechiByNightDbContext techiByNightDbContext)
        {
            this._techiByNightDbContext = techiByNightDbContext;
        }

        public async Task<BlogPostLike> AddLikeForBlog(BlogPostLike blogPostLike)
        {
            await _techiByNightDbContext.BlogPostLike.AddAsync(blogPostLike);
            await _techiByNightDbContext.SaveChangesAsync();
            return blogPostLike;
        }

        public async Task<IEnumerable<BlogPostLike>> GetLikesForBlog(Guid blogPostId)
        {
           return await _techiByNightDbContext.BlogPostLike.Where(x => x.BlogPostId == blogPostId).ToListAsync();

        }

        public async Task<int> GetTotalLikes(Guid blogPostId)
        {
          return  await _techiByNightDbContext.BlogPostLike.CountAsync(x => x.BlogPostId== blogPostId);
        }
    }
}
