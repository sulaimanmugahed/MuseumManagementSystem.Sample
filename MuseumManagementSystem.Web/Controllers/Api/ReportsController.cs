using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using MuseumManagementSystem.Application.Constants;
using MuseumManagementSystem.Application.Contracts.Persistence;
using MuseumManagementSystem.Domain.Models;
using MuseumManagementSystem.Persistence;
using MuseumManagementSystem.Web.Extensions;
using MuseumManagementSystem.Web.Services;
using MuseumManagementSystem.Web.ViewModels;
using System.Globalization;

namespace MuseumManagementSystem.Web.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController(IUnitOfWork unitOfWork,ICacheService _cacheService,IMapper mapper) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetArtifacts()
        {
            IEnumerable<ReportViewModel>? artifactsVm;
            string hashKey = HashKeys.ArtifactReports.ToString();

            artifactsVm = await _cacheService.GetHash<ReportViewModel>(hashKey);
            if (artifactsVm is null)
            {
                var artifacts = await unitOfWork.Artifacts.GetAllAsync(
                    a=> a.ArtifactCondition,
                    a => a.ArtifactMaterials,
                    a=>a.ArtifactType,
                    a=> a.Safe,
                    a=> a.Safe.Stowage);

                artifactsVm = mapper.Map<IEnumerable<ReportViewModel>>(artifacts);
 
                await _cacheService.SetHash(artifactsVm.Select(a => (a.Id.ToString(),a)), hashKey, TimeSpan.FromMinutes(1));
            }

            return Ok(new { Data = artifactsVm });
        }
    }
}
