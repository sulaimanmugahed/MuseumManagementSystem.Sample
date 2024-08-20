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
    public class ArtifactConditionsController(IUnitOfWork unitOfWork)
        : ControllerBase
    {
        

        public async Task<IActionResult> GetArtifactConditions([FromForm]JQueryDataTable request)
        {

            var result = await unitOfWork.ArtifactConditions
            .GetAllArtifactConditionsPagedAsync(request.SearchValue,
            request.Skip,
            request.PageSize);
            var viewModel = result.Data.Select(c =>
            new ArtifactConditionViewModel
            {
                Id = c.Id,
                Name = c.Name!,
                ArtifactsCount = unitOfWork.ArtifactConditions
                .GetArtifactCount(c.Id.ToString())
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
