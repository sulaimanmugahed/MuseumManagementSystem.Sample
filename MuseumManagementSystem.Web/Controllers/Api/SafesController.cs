using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MuseumManagementSystem.Application.Contracts.Persistence;
using MuseumManagementSystem.Application.Constants;
using MuseumManagementSystem.Web.ViewModels;
using MuseumManagementSystem.Domain.Models;
using Azure.Core;
using MuseumManagementSystem.Application.Models;
using MuseumManagementSystem.Web.Models;
using AutoMapper;

namespace MuseumManagementSystem.Web.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class SafesController(IUnitOfWork unitOfWork) 
        : ControllerBase
    {

        [HttpPost]
        public async Task<IActionResult> GetSafes([FromForm]JQueryDataTable request)
        {

            var result = await unitOfWork.Safes
            .GetAllSafesPagedAsync(request.SearchValue,
            request.Skip,
            request.PageSize);

            var viewModel = result.Data.Select(s =>
            new SafeViewModel
            {
                Id = s.Id,
                Name = s.Name!,
                StowageName = s.Stowage.Name!,
                ArtifactsCount = unitOfWork.Safes
                .GetArtifactCount(s.Id)
            });

            return Ok(new
            {
                recordsFiltered = result.RecordsFiltered,
                recordsTotal = result.RecordsTotal,
                data = viewModel
            });


        }

        [HttpPost("getsafesBystowageid/{id}")]

        public async Task<IActionResult> GetSafesByStowageId(Guid id, [FromForm]JQueryDataTable request)
        {

          var result = await unitOfWork.Safes
          .GetSafesPagedByStowageId(id,request.SearchValue,
          request.Skip,
          request.PageSize);

            var viewModel = result.Data.Select(s =>
            new SafeViewModel
            {
                Id = s.Id,
                Name = s.Name!,
                ArtifactsCount = unitOfWork.Safes
                .GetArtifactCount(s.Id)
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
