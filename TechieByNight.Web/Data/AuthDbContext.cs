using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace TechieByNight.Web.Data
{
    public class AuthDbContext : IdentityDbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Seed The Roles (User, Admin, SuperAdmin)

            var adminRoleId = "56ee2ce1-422c-4e2d-abc4-1a849ee96b28";
            var superAdminRoleId = "e5b56b50-5558-4fd3-a10f-a51270753be7";
            var userRoleId = "5ab3fa76-27de-4e14-bd18-ae7d97116b2d";

            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name="Admin",
                    NormalizedName = "Admin",
                    Id= adminRoleId,
                    ConcurrencyStamp = adminRoleId
                },
                new IdentityRole
                {
                    Name="SuperAdmin",
                    NormalizedName="SuperAdmin",
                    Id=superAdminRoleId,
                    ConcurrencyStamp = superAdminRoleId
                },
                new IdentityRole
                {
                    Name="User",
                    NormalizedName="User",
                    Id=userRoleId,
                    ConcurrencyStamp=userRoleId
                }
            };

            builder.Entity<IdentityRole>().HasData(roles);


            // Seed Super Admin User

            var superAdminId = "4feb2932-39cb-4755-91cd-a6f289bb453e";

            var SuperAdminUser = new IdentityUser
            {
                UserName = "mahdinoorzadeh1@gmail.com",
                Email = "mahdinoorzadeh1@gmail.com",
                NormalizedEmail = "mahdinoorzadeh1@gmail.com".ToUpper(),
                NormalizedUserName = "mahdinoorzadeh1@gmail.com".ToUpper(),
                Id = superAdminId
            };

            SuperAdminUser.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(SuperAdminUser, "Mahdi2077");

            builder.Entity<IdentityUser>().HasData(SuperAdminUser);

            // Add All the roles to SuperAdminUser

            var superadminRoles = new List<IdentityUserRole<string>>
            {
                new IdentityUserRole<string>
                {
                    RoleId = adminRoleId,
                    UserId = superAdminId
                },
                new IdentityUserRole<string>
                {
                    RoleId = superAdminRoleId,
                    UserId = superAdminId
                },
                new IdentityUserRole<string>
                {
                    RoleId = userRoleId,
                    UserId = superAdminId
                },
            };

            builder.Entity<IdentityUserRole<string>>().HasData(superadminRoles);
        }
    }
}

