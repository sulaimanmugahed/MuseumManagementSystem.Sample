using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MuseumManagementSystem.Web.Services;

namespace MuseumManagementSystem.Web.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class IntelligentSearchController (ImageSearchClientService imageSearchClient) : ControllerBase
    {
        [HttpPost(nameof(GetSimilarImages))]
        public async Task<IActionResult> GetSimilarImages(IFormFile image)
        {
            
            var result = await imageSearchClient.GetSimilerImages(image);
            if(!result.IsSuccess)
                return BadRequest(result.ErrorMessage);

            return Ok(result);
        }
    }
}
