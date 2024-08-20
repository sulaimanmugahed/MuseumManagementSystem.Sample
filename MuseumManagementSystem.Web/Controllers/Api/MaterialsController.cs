using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MuseumManagementSystem.Application.Contracts.Persistence;
using MuseumManagementSystem.Application.Constants;
using MuseumManagementSystem.Web.ViewModels;
using Azure.Core;
using MuseumManagementSystem.Application.Models;
using MuseumManagementSystem.Domain.Models;
using AutoMapper;
using MuseumManagementSystem.Web.Models;

namespace MuseumManagementSystem.Web.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaterialsController(IUnitOfWork unitOfWork)
        : ControllerBase
    {

        [HttpPost]
        public async Task<IActionResult> GetMaterials([FromForm]JQueryDataTable request)
        {

            var result = await unitOfWork.Materials
            .GetAllMaterialsPagedAsync(request.SearchValue,
            request.Skip,
            request.PageSize);

            var viewModel = result.Data.Select(m =>
            new MaterialViewModel
            {
                Id = m.Id,
                Name = m.Name!,
                ArtifactsCount = unitOfWork.Artifacts
                .GetArtifactCountForMaterial(m.Id),
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
