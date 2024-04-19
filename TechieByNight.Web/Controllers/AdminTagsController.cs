using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechieByNight.Web.Data;
using TechieByNight.Web.Models.Domain;
using TechieByNight.Web.Models.ViewModels;
using TechieByNight.Web.Repositories;

namespace TechieByNight.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminTagsController : Controller
    {
        private readonly ITagRepository _tagRepository;

        public AdminTagsController(ITagRepository tagRepository)
        {
            this._tagRepository = tagRepository;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Add")]
        public async Task<IActionResult> Add(AddTagRequest addTagRequest)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            // Maping AddTagRequest to Tag domain model
            var tag = new Tag
            {
                Name = addTagRequest.Name,
                DisplayName = addTagRequest.DisplayName,
            };

           await _tagRepository.AddAsync(tag);

            return RedirectToAction("List");
        }

        [HttpGet]
        [ActionName("List")]
        public async Task<IActionResult> List()
        {
            // Use dbContext to read the tags
            var tags = await _tagRepository.GetAllTagsAsync();

            return View(tags);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
          var tag = await _tagRepository.GetSingleTagAsync(id);

            if (tag != null)
            {
                var editTagRequest = new EditTagRequest
                {
                    Id = tag.Id,
                    Name = tag.Name,
                    DisplayName = tag.DisplayName,
                };

                return View(editTagRequest);
            }

            return View(null);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditTagRequest editTagRequest)
        {
            var tag = new Tag
            {
                Id = editTagRequest.Id,
                Name = editTagRequest.Name,
                DisplayName = editTagRequest.DisplayName,
            };


            var updatedTag = await _tagRepository.UpdateAsync(tag);

            if (updatedTag != null)
            {
                // Show success notification
            }
            else
            {
                // Show error notification
            }

            
            return RedirectToAction("Edit", new {id = editTagRequest.Id});
        }

        [HttpPost]
        public async Task<IActionResult> Delete(EditTagRequest editTagRequest)
        {
            var deletedTag = await _tagRepository.DeleteAsync(editTagRequest.Id);
            if (deletedTag != null)
            {
                // Show success notification
                return RedirectToAction("List");
            }
            // Show error notification
            return RedirectToAction("Edit", new { id = editTagRequest.Id });
        }


    }
}
