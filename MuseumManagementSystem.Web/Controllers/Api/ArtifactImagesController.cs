using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MuseumManagementSystem.Application.Contracts.Persistence;
using MuseumManagementSystem.Application.Constants;
using MuseumManagementSystem.Web.ExtensionMethods;
using Microsoft.Extensions.Hosting.Internal;
using Microsoft.AspNetCore.Hosting;

namespace MuseumManagementSystem.Web.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtifactImagesController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment,ILogger<ArtifactImagesController> logger)
        : ControllerBase
    {
       

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            var image = await unitOfWork.ArtifactImages.GetAsync(id);
            if (image == null)
            {
                return NotFound();
            }

            string existingPath = Path.Combine(webHostEnvironment.WebRootPath, image.Url!);
            logger.LogInformation(existingPath);
            if (System.IO.File.Exists(existingPath))
            {
                System.IO.File.Delete(existingPath);
                logger.LogInformation("deleted successfully !!");

            }

            await unitOfWork.ArtifactImages.DeleteAsync(image);
            await unitOfWork.SaveAsync();

            return Ok();

        }
    }
}
