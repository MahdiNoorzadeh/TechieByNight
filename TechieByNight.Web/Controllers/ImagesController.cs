using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TechieByNight.Web.Repositories;

namespace TechieByNight.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository _imageRepository;

        public ImagesController(IImageRepository imageRepository)
        {
            this._imageRepository = imageRepository;
        }

        [HttpPost]
        public async Task<IActionResult> UploadAsync(IFormFile file)
        {
           // Call a repository
           var imageUrl = await _imageRepository.UploadAsync(file);

            if (imageUrl == null)
            {
                return Problem("Something went wrong uploading the Image!", null, (int)HttpStatusCode.InternalServerError);
            }
            return new JsonResult(new { link = imageUrl });
        }

        [HttpGet]
        public IActionResult HealthCheck()
        {
            return Ok("API is up and running!");
        }

    }
}
