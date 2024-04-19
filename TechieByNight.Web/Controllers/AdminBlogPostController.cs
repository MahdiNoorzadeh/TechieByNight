using Microsoft.AspNetCore.Mvc;
using TechieByNight.Web.Models.ViewModels;
using TechieByNight.Web.Repositories;
using Microsoft.AspNetCore.Mvc.Rendering;
using TechieByNight.Web.Models.Domain;
using Microsoft.AspNetCore.Authorization;

namespace TechieByNight.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminBlogPostController : Controller
    {
        private readonly ITagRepository _tagRepository;
        private readonly IBlogPostRepository _blogPostRepository;

        public AdminBlogPostController(ITagRepository tagRepository, IBlogPostRepository blogPostRepository)
        {
            this._tagRepository = tagRepository;
            this._blogPostRepository = blogPostRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            // get tags from repository 
           var tags = await _tagRepository.GetAllTagsAsync();

            var model = new AddBlogPostRequest
            {
                Tags = tags.Select(x => new SelectListItem { Text = x.DisplayName, Value = x.Id.ToString() })
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddBlogPostRequest addBlogPostRequest)
        {
            // Map the view model to the domain model
            var blogPostDomain = new BlogPost
            {
                Heading = addBlogPostRequest.Heading,
                PageTitle = addBlogPostRequest.PageTitle,
                Content = addBlogPostRequest.Content,
                ShortDescription = addBlogPostRequest.ShortDescription,
                FeaturedImageUrl = addBlogPostRequest.FeaturedImageUrl,
                UrlHandle = addBlogPostRequest.UrlHandle,
                PublishedDate = addBlogPostRequest.PublishedDate,
                Author = addBlogPostRequest.Author,
                Visible = addBlogPostRequest.Visible,

            };
            // Map Tags from selected Tags
            var selectedTags = new List<Tag>();
            foreach (var selectedTagId in addBlogPostRequest.SelectedTags)
            {
                var selectedTagIdAsGuid = Guid.Parse(selectedTagId);
                var exsitingTag = await _tagRepository.GetSingleTagAsync(selectedTagIdAsGuid);
                if (exsitingTag != null)
                {
                    selectedTags.Add(exsitingTag);
                }
            }
            // Maping Tags back to domain model
            blogPostDomain.Tags = selectedTags;
            await _blogPostRepository.AddAsync(blogPostDomain);

            return RedirectToAction("Add");
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            // Call the repository
           var blogPost = await _blogPostRepository.GetAllAsync();
           

            return View(blogPost);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            // Retrive the result from the repository
         var blogPost =  await _blogPostRepository.GetAsync(id);
         var tagsDomainModel = await _tagRepository.GetAllTagsAsync();
          
         if (blogPost != null)
            {
                // map the domain model into the view model
                var model = new EditBlogPostRequest
                {
                    Id = blogPost.Id,
                    Heading = blogPost.Heading,
                    PageTitle = blogPost.PageTitle,
                    Content = blogPost.Content,
                    Author = blogPost.Author,
                    FeaturedImageUrl = blogPost.FeaturedImageUrl,
                    UrlHandle = blogPost.UrlHandle,
                    ShortDescription = blogPost.ShortDescription,
                    PublishedDate = blogPost.PublishedDate,
                    Visible = blogPost.Visible,
                    Tags = tagsDomainModel.Select(x => new SelectListItem
                    {
                        Text = x.Name,
                        Value = x.Id.ToString()
                    })
                    ,
                    SelectedTags = blogPost.Tags.Select(x => x.Id.ToString()).ToArray()
                };

                return View(model);
            }

            // pass data to view
            return View(null);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditBlogPostRequest editBlogPostRequest)
        {
            // map view model back to domain model 
            var blogPostDomainModel = new BlogPost
            {
                Id = editBlogPostRequest.Id,
                Heading = editBlogPostRequest.Heading,
                PageTitle = editBlogPostRequest.PageTitle,
                Content = editBlogPostRequest.Content,
                Author = editBlogPostRequest.Author,
                ShortDescription = editBlogPostRequest.ShortDescription,
                FeaturedImageUrl = editBlogPostRequest.FeaturedImageUrl,
                PublishedDate = editBlogPostRequest.PublishedDate,
                UrlHandle = editBlogPostRequest.UrlHandle,
                Visible = editBlogPostRequest.Visible,
            };

            // Map Tags into domain Model
            var selectedTags = new List<Tag>();
            foreach (var selectedTag in editBlogPostRequest.SelectedTags)
            {
                if(Guid.TryParse(selectedTag, out var tag))
                {
                  var foundTag =  await _tagRepository.GetSingleTagAsync(tag);

                    if (foundTag != null)
                    {
                        selectedTags.Add(foundTag);
                    }
                }
            }

            blogPostDomainModel.Tags = selectedTags;

            //submit imformation to repository to update
            var updatedBlog = await _blogPostRepository.UpdateAsync(blogPostDomainModel);

            if (updatedBlog != null) 
            {
                // show success notification
                return RedirectToAction("Edit");
            }
            return RedirectToAction("Edit");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(EditBlogPostRequest editBlogPostRequest)
        {
            // Talk to repository to delete this blog post and tags 
          var deletedBlogPost =  await _blogPostRepository.DeleteAsync(editBlogPostRequest.Id);
           if (deletedBlogPost != null)
            {
                // Show success notification
                return RedirectToAction("List");
            }
            // Show Error
            return RedirectToAction("Edit", new  { id = editBlogPostRequest.Id });

            // display the response


        }
    }
}
