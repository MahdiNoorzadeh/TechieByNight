using TechieByNight.Web.Models.Domain;

namespace TechieByNight.Web.Repositories
{
    public interface ITagRepository
    {
       Task<IEnumerable<Tag>> GetAllTagsAsync();
       Task<Tag?> GetSingleTagAsync(Guid id);
       Task<Tag>AddAsync(Tag tag);
       Task<Tag?> UpdateAsync(Tag tag);
       Task<Tag?> DeleteAsync(Guid id);    
    }
}
