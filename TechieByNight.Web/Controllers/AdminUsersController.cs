using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TechieByNight.Web.Models.ViewModels;
using TechieByNight.Web.Repositories;

namespace TechieByNight.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminUsersController : Controller
    {

        private readonly IUserRepostiory _userRepostiory;
        private readonly UserManager<IdentityUser> _userManager;

        public AdminUsersController(IUserRepostiory userRepostiory, UserManager<IdentityUser> userManager)
        {
            this._userRepostiory = userRepostiory;
            this._userManager = userManager;
        }
        [HttpGet]
        public async Task<IActionResult> List()
        {
            var users = await _userRepostiory.GetAll();

            var usersViewModel = new UserViewModel();
            usersViewModel.Users = new List<User>();

            foreach (var user in users)
            {
                usersViewModel.Users.Add(new Models.ViewModels.User
                {
                    Id = Guid.Parse(user.Id),
                    Username = user.UserName,
                    EmailAddress = user.Email,
                });
            }

            return View(usersViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> List(UserViewModel request)
        {
            var identityUser = new IdentityUser
            {
                UserName = request.Username,
                Email = request.Email,

            };
           var identityResult = await _userManager.CreateAsync(identityUser, request.Password);

            if (identityResult is not null)
            {
                if (identityResult.Succeeded)
                {
                    // assign roles to user
                    var roles = new List<string> { "User" };
                    if (request.AdminRoleCheckBox)
                    {
                        roles.Add("Admin");
                    }

                   identityResult = await _userManager.AddToRolesAsync(identityUser, roles);

                    if (identityResult is not null && identityResult.Succeeded)
                    {
                        return RedirectToAction("List", "AdminUsers");
                    }
                }
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
           var user = await _userManager.FindByIdAsync(id.ToString());
            if (user is not null)
            {
              var identityResult =  await _userManager.DeleteAsync(user);

                if (identityResult is not null && identityResult.Succeeded) {
                    return RedirectToAction("List", "AdminUsers");
                }
            }
            return View();
        }
    }
}
