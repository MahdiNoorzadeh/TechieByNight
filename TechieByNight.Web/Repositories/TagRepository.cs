using Microsoft.EntityFrameworkCore;
using TechieByNight.Web.Data;
using TechieByNight.Web.Models.Domain;

namespace TechieByNight.Web.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly TechiByNightDbContext _techiByNightDbContext;

        public TagRepository(TechiByNightDbContext techiByNightDbContext)
        {
            this._techiByNightDbContext = techiByNightDbContext;
        }
        public async Task<Tag> AddAsync(Tag tag)
        {
            await _techiByNightDbContext.Tags.AddAsync(tag);
            await _techiByNightDbContext.SaveChangesAsync();
            return tag;
        }

        public async Task<Tag?> DeleteAsync(Guid id)
        {
           var exsitingTag = await _techiByNightDbContext.Tags.FindAsync(id);
            if (exsitingTag != null)
            {
                _techiByNightDbContext.Tags.Remove(exsitingTag);
                await _techiByNightDbContext.SaveChangesAsync();
                return exsitingTag;
            }

            return null;
        }

        public async Task<IEnumerable<Tag>> GetAllTagsAsync()
        {
           return await _techiByNightDbContext.Tags.ToListAsync();
        }

        public Task<Tag?> GetSingleTagAsync(Guid id)
        {
           return _techiByNightDbContext.Tags.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Tag?> UpdateAsync(Tag tag)
        {
            var existingTag = await _techiByNightDbContext.Tags.FindAsync(tag.Id);
            if (existingTag != null)
            {
                existingTag.Name = tag.Name;
                existingTag.DisplayName = tag.DisplayName;

                await _techiByNightDbContext.SaveChangesAsync();

                return existingTag;
            }
            return null;
        }
    }
}
