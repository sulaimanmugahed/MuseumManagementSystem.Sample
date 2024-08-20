using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MuseumManagementSystem.Application.Contracts.Persistence;

using MuseumManagementSystem.Web.ViewModels;
using MuseumManagementSystem.Web.Models;
using Microsoft.AspNetCore.Authorization;
using MuseumManagementSystem.Application.Constants;

namespace MuseumManagementSystem.Web.Controllers.Api
{
    [Authorize(Policy = Policies.AllExcepyBase)]
    [Route("api/[controller]")]
    [ApiController]
    public class ArtifactsController(IUnitOfWork unitOfWork, IMapper mapper)
        : ControllerBase
    {
       

        [HttpPost]
        public async Task<IActionResult> GetArtifacts([FromForm] JQueryDataTable request)
        {
            var result = await unitOfWork.Artifacts
            .GetAllArtifactsPagedAsync(
            request.SearchValue,
            request.Skip,
            request.PageSize,
            request.SortColumn,
            request.SortColumnDirection);

            return Ok(new
            {
                recordsFiltered = result.RecordsFiltered,
                recordsTotal = result.RecordsTotal,
                data = mapper.Map<IEnumerable<ArtifactViewModel>>(result.Data)
            });
          
        }

        [HttpPost("getartifactsbysafeid/{id}")]
        public async Task<IActionResult> GetArtifactsBySafeId(Guid id, [FromForm] JQueryDataTable request)
        {

            var result = await unitOfWork.Artifacts
           .GetAllArtifactsPagedAsync(request.SearchValue,
           request.Skip,
           request.PageSize,
           request.SortColumn,
           request.SortColumnDirection,
           a=> a.SafeId == id);

            return Ok(new
            {
                recordsFiltered = result.RecordsFiltered,
                recordsTotal = result.RecordsTotal,
                data = mapper.Map<IEnumerable<ArtifactViewModel>>(result.Data)
            });
        }


        [HttpPost("getartifactsbystowageid/{id}")]
        public async Task<IActionResult> GetArtifactsByStowageId(Guid id, [FromForm] JQueryDataTable request)
        {

            var result = await unitOfWork.Artifacts
           .GetAllArtifactsPagedAsync(request.SearchValue,
           request.Skip,
           request.PageSize,
           request.SortColumn,
           request.SortColumnDirection,
           a => a.Safe.StowageId == id);

            return Ok(new
            {
                recordsFiltered = result.RecordsFiltered,
                recordsTotal = result.RecordsTotal,
                data = mapper.Map<IEnumerable<ArtifactViewModel>>(result.Data)
            });
        }

        [HttpPost(nameof(GetAllArtifactsWithoutSafe))]
        public async Task<IActionResult> GetAllArtifactsWithoutSafe([FromForm] JQueryDataTable request)
        {
            var result = await unitOfWork.Artifacts.GetAllArtifactsPagedAsync(
                 request.SearchValue,
            request.Skip,
            request.PageSize,
            request.SortColumn,
            request.SortColumnDirection,
            a => a.Safe == null
                );

            return Ok(new
            {
                recordsFiltered = result.RecordsFiltered,
                recordsTotal = result.RecordsTotal,
                data = mapper.Map<IEnumerable<ArtifactViewModel>>(result.Data)
            });
        }

        [HttpPost(nameof(GetAllArtifactsWithoutOldMuseumNumber))]
        public async Task<IActionResult> GetAllArtifactsWithoutOldMuseumNumber([FromForm] JQueryDataTable request)
        {
            var result = await unitOfWork.Artifacts.GetAllArtifactsPagedAsync(
                 request.SearchValue,
            request.Skip,
            request.PageSize,
            request.SortColumn,
            request.SortColumnDirection,
            a => a.OldMuseumNumber == null
                );

            return Ok(new
            {
                recordsFiltered = result.RecordsFiltered,
                recordsTotal = result.RecordsTotal,
                data = mapper.Map<IEnumerable<ArtifactViewModel>>(result.Data)
            });
        }

        [HttpPost(nameof(GetAllArtifactsWithoutNewMuseumNumber))]
        public async Task<IActionResult> GetAllArtifactsWithoutNewMuseumNumber([FromForm] JQueryDataTable request)
        {
            var result = await unitOfWork.Artifacts.GetAllArtifactsPagedAsync(
                 request.SearchValue,
            request.Skip,
            request.PageSize,
            request.SortColumn,
            request.SortColumnDirection,
            a => a.NewMuseumNumber == null
                );

            return Ok(new
            {
                recordsFiltered = result.RecordsFiltered,
                recordsTotal = result.RecordsTotal,
                data = mapper.Map<IEnumerable<ArtifactViewModel>>(result.Data)
            });
        }
    }
}
