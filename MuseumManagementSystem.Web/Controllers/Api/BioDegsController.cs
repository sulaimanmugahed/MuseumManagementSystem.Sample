using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MuseumManagementSystem.Application.Contracts.Persistence;
using MuseumManagementSystem.Application.Constants;
using MuseumManagementSystem.Web.ViewModels;
using Azure.Core;
using MuseumManagementSystem.Application.Models;
using MuseumManagementSystem.Domain.Models;
using MuseumManagementSystem.Web.Models;
using AutoMapper;

namespace MuseumManagementSystem.Web.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class BioDegsController(IUnitOfWork unitOfWork) 
        : ControllerBase
    {

        [HttpPost]
        public async Task<IActionResult> GetBioDegs([FromForm]JQueryDataTable request)
        {

            var result = await unitOfWork.BioDegs
             .GetAllBioDegsPagedAsync(request.SearchValue,
             request.Skip,
             request.PageSize);

            var viewModel = result.Data.Select(b =>
            new BioDegViewModel
            {
                Id = b.Id,
                Name = b.Name!,
                ArtifactsCount = unitOfWork.BioDegs
                .GetArtifactCount(b.Id.ToString())
            });

            return Ok(new
            {
                recordsFiltered = result.RecordsFiltered,
                recordsTotal = result.RecordsTotal,
                data = viewModel
            });

        }
    }
}
