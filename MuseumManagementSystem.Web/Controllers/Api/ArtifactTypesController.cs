using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MuseumManagementSystem.Application.Contracts.Persistence;
using MuseumManagementSystem.Application.Constants;
using MuseumManagementSystem.Web.ViewModels;
using System.Globalization;
using Azure.Core;
using MuseumManagementSystem.Application.Models;
using MuseumManagementSystem.Domain.Models;
using MuseumManagementSystem.Web.Models;
using AutoMapper;

namespace MuseumManagementSystem.Web.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtifactTypesController(IUnitOfWork unitOfWork) 
        : ControllerBase
    {

        [HttpPost]
        public async Task<IActionResult> GetArtifactTypes([FromForm]JQueryDataTable request)
        {

            var result = await unitOfWork.ArtifactTypes
            .GetAllArtifactTypesPagedAsync(request.SearchValue,
            request.Skip,
            request.PageSize);

            var viewModel = result.Data.Select(t =>
            new ArtifactTypeViewModel
            {
                Id = t.Id,
                Name = t.Name!,
                ArtifactsCount = unitOfWork.ArtifactTypes
                .GetArtifactCount(t.Id.ToString())
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
