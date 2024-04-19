using TechieByNight.Web.Models.Domain;

namespace TechieByNight.Web.Models.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<BlogPost> blogPosts { get; set; }
        public IEnumerable <Tag> Tags { get; set; }
    }
}
