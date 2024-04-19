using Microsoft.EntityFrameworkCore;
using TechieByNight.Web.Data;
using TechieByNight.Web.Models.Domain;

namespace TechieByNight.Web.Repositories
{
    public class BlogPostRepository : IBlogPostRepository
    {
        private readonly TechiByNightDbContext _techiByNightDbContext;

        public BlogPostRepository(TechiByNightDbContext techiByNightDbContext)
        {
            this._techiByNightDbContext = techiByNightDbContext;
        }
        public async Task<BlogPost> AddAsync(BlogPost blogpost)
        {
           await _techiByNightDbContext.AddAsync(blogpost);
           await _techiByNightDbContext.SaveChangesAsync();
           return blogpost;
        }

        public async Task<BlogPost?> DeleteAsync(Guid id)
        {
           var exsitingBlog = await _techiByNightDbContext.BlogPosts.FindAsync(id);

            if (exsitingBlog != null)
            {
                _techiByNightDbContext.BlogPosts.Remove(exsitingBlog);
                await _techiByNightDbContext.SaveChangesAsync();
                return exsitingBlog;
            }
            return null;
        }

        public async Task<IEnumerable<BlogPost>> GetAllAsync()
        {
           return await _techiByNightDbContext.BlogPosts.Include(x => x.Tags).ToListAsync();

        }

        public async Task<BlogPost?> GetAsync(Guid id)
        {
            return await _techiByNightDbContext.BlogPosts.Include(x => x.Tags).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<BlogPost?> GetByUrlHandleAsync(string urlHandle)
        {
          return await _techiByNightDbContext.BlogPosts.Include(x => x.Tags).FirstOrDefaultAsync(x => x.UrlHandle == urlHandle);
        }

        public async Task<BlogPost?> UpdateAsync(BlogPost blogpost)
        {
         var exsitingBlog = await _techiByNightDbContext.BlogPosts.Include(x => x.Tags).FirstOrDefaultAsync(x => x.Id == blogpost.Id);
            if (exsitingBlog != null)
            {
                exsitingBlog.Id = blogpost.Id;
                exsitingBlog.Heading = blogpost.Heading;
                exsitingBlog.PageTitle = blogpost.PageTitle;
                exsitingBlog.Content = blogpost.Content;
                exsitingBlog.ShortDescription = blogpost.ShortDescription;
                exsitingBlog.Author = blogpost.Author;
                exsitingBlog.FeaturedImageUrl = blogpost.FeaturedImageUrl;
                exsitingBlog.UrlHandle = blogpost.UrlHandle;
                exsitingBlog.Visible = blogpost.Visible;
                exsitingBlog.PublishedDate = blogpost.PublishedDate;
                exsitingBlog.Tags = blogpost.Tags;

                await _techiByNightDbContext.SaveChangesAsync();
                return exsitingBlog;
            }
            return null;
        }
    }
}
