using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TechieByNight.Web.Data;

namespace TechieByNight.Web.Repositories
{
    public class UserRepository : IUserRepostiory
    {
        private readonly AuthDbContext _authDbContext;

        public UserRepository(AuthDbContext authDbContext)
        {
            this._authDbContext = authDbContext;
        }

        public async Task<IEnumerable<IdentityUser>> GetAll()
        {
           var users = await _authDbContext.Users.ToListAsync();
            var superAdminUser = await _authDbContext.Users.FirstOrDefaultAsync
                (x => x.Email == "mahdinoorzadeh1@gmail.comd");

            if (superAdminUser != null)
            {
                users.Remove(superAdminUser);
            }
            return users;
        }
    }
}
