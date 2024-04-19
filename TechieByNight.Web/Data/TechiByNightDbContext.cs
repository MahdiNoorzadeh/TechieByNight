using Microsoft.EntityFrameworkCore;
using TechieByNight.Web.Models.Domain;

namespace TechieByNight.Web.Data
{
    public class TechiByNightDbContext : DbContext
    {
        public TechiByNightDbContext(DbContextOptions<TechiByNightDbContext> options) : base(options)
        {
        }

        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<BlogPostLike> BlogPostLike { get; set; }
        public DbSet<BlogPostComment> BlogPostComment { get; set; }
    }
}
