using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TechieByNight.Web.Models.Domain;
using TechieByNight.Web.Models.ViewModels;
using TechieByNight.Web.Repositories;

namespace TechieByNight.Web.Controllers
{
    public class BlogsController : Controller
    {
        private readonly IBlogPostRepository _blogPostRepository;
        private readonly IBlogPostLikeRepository _blogPostLikeRepository;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IBlogPostCommentRepository _blogPostCommentRepository;

        public BlogsController(IBlogPostRepository blogPostRepository, IBlogPostLikeRepository blogPostLikeRepository, SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, IBlogPostCommentRepository blogPostCommentRepository)
        {
            this._blogPostRepository = blogPostRepository;
            this._blogPostLikeRepository = blogPostLikeRepository;
            this._signInManager = signInManager;
            this._userManager = userManager;
            this._blogPostCommentRepository = blogPostCommentRepository;
        }
        [HttpGet]
        public async Task<IActionResult> Index(string urlHandle)
        {

            var liked = false;
            var blogPost =  await _blogPostRepository.GetByUrlHandleAsync(urlHandle);
            var blogPostLikeViewModel = new BlogDeatilsViewModels();



            if (blogPost != null)
            {

              var totalLikes = await _blogPostLikeRepository.GetTotalLikes(blogPost.Id);
              
               if (_signInManager.IsSignedIn(User))
                {
                    // Get likes of this blog for this user 
                 var likesForBlog =  await _blogPostLikeRepository.GetLikesForBlog(blogPost.Id);
                    var userId = _userManager.GetUserId(User);
                    if (userId != null)
                    {
                      var likeFromUser =  likesForBlog.FirstOrDefault(x => x.UserId == Guid.Parse(userId));
                        liked = likeFromUser != null;
                    }
                }


                // Get Comments for blogPost
                var blogCommentsDomainModel = await _blogPostCommentRepository.GetCommentsByBlogIdAsync(blogPost.Id);

                var blogCommentsForView = new List<BlogComment>();
                foreach (var blogComment in blogCommentsDomainModel) 
                {
                    blogCommentsForView.Add(new BlogComment
                    {
                        Description = blogComment.Description,
                        DateAdded = blogComment.DateAdded,
                        Username = (await _userManager.FindByIdAsync(blogComment.UserId.ToString())).UserName
                    });
                }


                blogPostLikeViewModel = new BlogDeatilsViewModels
                {
                    Id = blogPost.Id,
                    Content = blogPost.Content,
                    PageTitle = blogPost.PageTitle,
                    Author = blogPost.Author,
                    FeaturedImageUrl = blogPost.FeaturedImageUrl,
                    Heading = blogPost.Heading,
                    PublishedDate = blogPost.PublishedDate,
                    ShortDescription = blogPost.ShortDescription,
                    UrlHandle = blogPost.UrlHandle,
                    Visible = blogPost.Visible,
                    Tags = blogPost.Tags,
                    TotalLikes = totalLikes,
                    Liked = liked,
                    Comments = blogCommentsForView,
                };

            }

            return View(blogPostLikeViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Index(BlogDeatilsViewModels blogDeatilsViewModels)
        {
            if (_signInManager.IsSignedIn(User))
            {
                var domainModel = new BlogPostComment
                {
                    BlogPostId = blogDeatilsViewModels.Id,
                    Description = blogDeatilsViewModels.CommentDecription,
                    UserId = Guid.Parse(_userManager.GetUserId(User)),
                    DateAdded = DateTime.Now,
                };
              await _blogPostCommentRepository.AddAsync(domainModel);
                return RedirectToAction("Index", "Blogs", 
                    new { urlHandle = blogDeatilsViewModels.UrlHandle });
            }
            return View();
        }
    }
}
