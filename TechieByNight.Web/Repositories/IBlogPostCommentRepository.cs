using TechieByNight.Web.Models.Domain;

namespace TechieByNight.Web.Repositories
{
    public interface IBlogPostCommentRepository
    {
        Task<BlogPostComment> AddAsync(BlogPostComment blogPostComment);
        Task<IEnumerable<BlogPostComment>> GetCommentsByBlogIdAsync( Guid blogPostId);
    }
}
