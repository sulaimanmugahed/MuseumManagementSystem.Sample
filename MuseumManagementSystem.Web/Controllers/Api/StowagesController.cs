using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MuseumManagementSystem.Application.Contracts.Persistence;
using MuseumManagementSystem.Application.Constants;
using MuseumManagementSystem.Web.ViewModels;
using Azure.Core;
using MuseumManagementSystem.Application.Models;
using MuseumManagementSystem.Domain.Models;
using Microsoft.Identity.Client.Extensions.Msal;
using AutoMapper;
using MuseumManagementSystem.Web.Models;

namespace MuseumManagementSystem.Web.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class StowagesController(IUnitOfWork unitOfWork)
        : ControllerBase
    {

        [HttpPost]
        public async Task<IActionResult> GetStowages([FromForm]JQueryDataTable request)
        {

            var result = await unitOfWork.Stowages
             .GetAllStowagesPagedAsync(request.SearchValue,
             request.Skip,
             request.PageSize);

            var viewModel = result.Data.Select(st =>
            new StowageViewModel
            {
                Id = st.Id,
                Name = st.Name!,
                Address = st.Address,
                ArtifactsCount = unitOfWork.Stowages
                .GetArtifactCount(st.Id.ToString()),
                SafesCount = unitOfWork.Stowages
                .GetSafesCount(st.Id.ToString())
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
