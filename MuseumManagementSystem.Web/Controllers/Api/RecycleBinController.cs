using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MuseumManagementSystem.Application.Contracts.Persistence;
using MuseumManagementSystem.Domain.Models;
using MuseumManagementSystem.Web.ExtensionMethods;
using MuseumManagementSystem.Web.ViewModels;

namespace MuseumManagementSystem.Web.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecycleBinController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        : ControllerBase
    {


        [HttpPost]
        public IActionResult GetDeletedArtifacts()
        {

            var pageSize = int.Parse(Request.Form["length"]);
            var skip = int.Parse(Request.Form["start"]);
            var searchValue = Request.Form["search[value]"];
            var sortColumn = Request.Form[string.Concat("columns[", Request.Form["order[0][column]"], "][name]")];
            var sortColumnDirection = Request.Form["order[0][dir]"];

            var artifacts = unitOfWork.Artifacts.GetDeletedArtifactsToDataTable(searchValue,sortColumn,sortColumnDirection);
           
            var data = artifacts.Skip(skip).Take(pageSize).ToList();

            var recordsTotal = artifacts.Count();

            var jsonData = new { recordsFiltered = recordsTotal, recordsTotal, data };

            return Ok(jsonData);
        }


        [HttpPost(nameof(Clear))]
        public async Task<IActionResult> Clear()
        {

           var artifactToDelete = await unitOfWork.Artifacts.GetAllDeletedArtifactAsync();
            if (artifactToDelete is null || artifactToDelete.Count == 0) 
            {
                return Ok();
            }

            foreach (var artifact in artifactToDelete)
            {
                if (artifact.Images is not null)
                {
                    foreach (var image in artifact.Images)
                    {
                        var existingPath = Path.Combine(webHostEnvironment.WebRootPath, image.Url!);
                        if (System.IO.File.Exists(existingPath))
                        {
                            System.IO.File.Delete(existingPath);
                        }
                    }
                }
            }

            await unitOfWork.Artifacts.RemoveRange(artifactToDelete);
            await unitOfWork.SaveAsync();

            return Ok();
        }
    }
}
