using TechieByNight.Web.Models.Domain;

namespace TechieByNight.Web.Repositories
{
    public interface IBlogPostLikeRepository
    {
       Task<int> GetTotalLikes(Guid blogPostId);

       Task<IEnumerable<BlogPostLike>> GetLikesForBlog(Guid blogPostId);

        Task<BlogPostLike> AddLikeForBlog(BlogPostLike blogPostLike);

    }
}
