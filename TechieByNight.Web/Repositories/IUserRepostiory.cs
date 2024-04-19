using Microsoft.AspNetCore.Identity;
using Microsoft.Identity.Client;

namespace TechieByNight.Web.Repositories
{
    public interface IUserRepostiory
    {
        Task<IEnumerable<IdentityUser>> GetAll();
    }
}
