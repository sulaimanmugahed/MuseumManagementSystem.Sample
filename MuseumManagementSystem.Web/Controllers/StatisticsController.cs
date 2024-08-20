using ClosedXML.Excel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using MuseumManagementSystem.Application.Contracts.Persistence;
using MuseumManagementSystem.Domain.Models;
using MuseumManagementSystem.Web.ExtensionMethods;
using MuseumManagementSystem.Web.ViewModels;
using System.Globalization;
using System.Reflection;

namespace MuseumManagementSystem.Web.Controllers
{
    [Authorize]
    public class StatisticsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStringLocalizer<SafesController> _localizer;

        public StatisticsController(IUnitOfWork unitOfWork, IStringLocalizer<SafesController> localizer)
        {
            _unitOfWork = unitOfWork;
            _localizer = localizer;
        }
        public async Task<IActionResult> Index()
        {

            var artifacts = await _unitOfWork.Artifacts.GetAllAsync(a=> a.Safe);
            StatisticsViewModel statisticsViewModel = new()
            { 
                
                TotalArtifactCount = artifacts.Count(),
                ArtifactConditionsCount = await _unitOfWork.ArtifactConditions.CountAsync(),
                ArtifactTypesCount = await _unitOfWork.ArtifactTypes.CountAsync(),
                BiodegsCount = await _unitOfWork.BioDegs.CountAsync(),
                MaterialsCount = await _unitOfWork.Materials.CountAsync(),
                TimePeriodsCount = await _unitOfWork.TimePeriods.CountAsync(),
                ArtifactWithoutNewMuseumNumberCount = artifacts.Where(a=> a.NewMuseumNumber is null).Count(),
                ArtifactWithoutOldMuseumNumberCount = artifacts.Where(a => a.OldMuseumNumber is null).Count(),
                ArtifactWithoutSafeCount = artifacts.Where(a => a.Safe is null).Count(),

            };
   
            return View(statisticsViewModel);
            
        }

        public  IActionResult ArtifactTypesStatistics()
        {
           
            return View();

        }

        public IActionResult MaterialsStatistics()
        {        
            return View();
        }

        public IActionResult BioDegsStatistics()
        {
            return View();
        }

        public IActionResult TimePeriodsStatistics()
        {

            return View();

        }

        public IActionResult ArtifactConditionsStatistics()
        {

            return View();

        }










    }
}
