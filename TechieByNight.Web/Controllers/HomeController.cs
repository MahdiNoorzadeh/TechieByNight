using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TechieByNight.Web.Models;
using TechieByNight.Web.Models.ViewModels;
using TechieByNight.Web.Repositories;

namespace TechieByNight.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBlogPostRepository _blogPostRepository;
        private readonly ITagRepository _tagRepository;

        public HomeController(ILogger<HomeController> logger, IBlogPostRepository blogPostRepository, ITagRepository tagRepository)
        {
            _logger = logger;
            this._blogPostRepository = blogPostRepository;
            this._tagRepository = tagRepository;
        }

        public async Task<IActionResult> Index()
        {
            // getting All blogs
           var blogPost = await _blogPostRepository.GetAllAsync();
            // getting all tags
           var tags = await _tagRepository.GetAllTagsAsync();
            // Create HomeViewModel
            var model = new HomeViewModel
            {
                blogPosts = blogPost,
                Tags = tags
            };
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
