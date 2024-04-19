using Microsoft.EntityFrameworkCore;
using TechieByNight.Web.Data;
using TechieByNight.Web.Models.Domain;

namespace TechieByNight.Web.Repositories
{
    public class BlogPostCommentRepository : IBlogPostCommentRepository
    {
        private readonly TechiByNightDbContext _techiByNightDbContext;

        public BlogPostCommentRepository(TechiByNightDbContext techiByNightDbContext)
        {
            this._techiByNightDbContext = techiByNightDbContext;
        }
        public async Task<BlogPostComment> AddAsync(BlogPostComment blogPostComment)
        {
            await _techiByNightDbContext.BlogPostComment.AddAsync(blogPostComment);
            await _techiByNightDbContext.SaveChangesAsync();
            return blogPostComment;
        }

        public async Task<IEnumerable<BlogPostComment>> GetCommentsByBlogIdAsync(Guid blogPostId)
        {
           return await _techiByNightDbContext.BlogPostComment.Where(x => x.BlogPostId == blogPostId).ToListAsync();
        }
    }
}
