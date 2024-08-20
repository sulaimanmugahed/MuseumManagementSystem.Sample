using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using MuseumManagementSystem.Application.Constants;
using MuseumManagementSystem.Application.Contracts.Persistence;
using MuseumManagementSystem.Domain.Models;
using MuseumManagementSystem.Web.ExtensionMethods;
using MuseumManagementSystem.Web.ViewModels;
using System.Globalization;

namespace MuseumManagementSystem.Web.Controllers
{
    [Authorize]
    public class ArtifactConditionsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStringLocalizer<ArtifactConditionsController> _localizer;

        public ArtifactConditionsController(IUnitOfWork unitOfWork, IStringLocalizer<ArtifactConditionsController> localizer)
        {
            _unitOfWork = unitOfWork;
            _localizer = localizer;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ArtifactConditionViewModel model)
        {
            if(!ModelState.IsValid)
                return View(model);

            var isNameAssigned = _unitOfWork.ArtifactConditions.IsNameAssigned(model.Name);
            if (isNameAssigned)
            {
                ModelState.AddModelError("Name", _localizer["artifactConditionNameAssignedValidation"].Value);
                return View(model);
            }

            var bioDeg = new ArtifactCondition
            {
                Id = Guid.NewGuid(),
                Name = model.Name,
            };

           await  _unitOfWork.ArtifactConditions.AddAsync(bioDeg);
            await _unitOfWork.SaveAsync();

            TempData["AlertMessage"] = _localizer["successCreateMessage"].Value;

            return RedirectToAction(nameof(Index));
        }

       
        public async Task<IActionResult> Edit(Guid id)
        { 
            var artifactConditionToEdit = await _unitOfWork.ArtifactConditions.GetAsync(id);
            if(artifactConditionToEdit == null)
                return NotFound();

            var viewModel = new ArtifactConditionViewModel
            {
                Id= artifactConditionToEdit.Id,
                Name = artifactConditionToEdit.Name,

            };

            return View(viewModel);  
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ArtifactConditionViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var artifactConditionToEdit = await _unitOfWork.ArtifactConditions.GetAsync(model.Id);
            if (artifactConditionToEdit is null)
                return NotFound();

            var IsNameAssigned = _unitOfWork.ArtifactConditions.IsNameAssigned(model.Name,model.Id);
            if (IsNameAssigned)
            {
                ModelState.AddModelError("Name", _localizer["artifactConditionNameAssignedValidation"].Value);
                return View(model);
            }


            artifactConditionToEdit.Name = model.Name;

            await _unitOfWork.SaveAsync();

            TempData["AlertMessage"] = _localizer["successEditMessage"].Value;
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Delete(Guid id)
        {
            var artifactConditionToDelete = await _unitOfWork.ArtifactConditions.GetAsync(id);
            if (artifactConditionToDelete is null)
                return NotFound();

            bool isContainArtifacts = _unitOfWork.ArtifactConditions.IsContainArtifacts(id);
            if (isContainArtifacts)
                return BadRequest(_localizer["cantDeleteArtifactConditionErrorMessage"].Value);


            await _unitOfWork.ArtifactConditions.DeleteAsync(artifactConditionToDelete);
            await _unitOfWork.SaveAsync();

            return Json(new { message = _localizer["successDeleteMessage"].Value });
        }

        [HttpPost]
        public async Task<IActionResult> Export(string type)
        {
            switch (type)
            {
                case "excel":
                    return await ExportToExcel();

                default: return NotFound();
            }

        }



        public async Task<IActionResult> ExportToExcel()
        {


            var artifactConditions = await _unitOfWork.ArtifactConditions
                .GetAllAsync();
            var artifactConditionsChart = artifactConditions
                .Select(t => new ChartDataViewModel
                {
                    Name = t.Name,
                    ArtifactCount = _unitOfWork.ArtifactConditions.GetArtifactCount(t.Id.ToString())

                });

            var loclaizedColumnNames = new Dictionary<string, string>
            {
                {"Name", _localizer["artifactCondition"].Value },
                {"ArtifactCount", _localizer["artifactsCount"].Value },
            };

            var workbook = ExcelHelper.ExportToExcel(artifactConditionsChart, loclaizedColumnNames);
            using (var stream = new MemoryStream())
            {
                workbook.SaveAs(stream);
                var content = stream.ToArray();

                return File(content, "application/vnd.openxmlformats-officedocumnt.spreadsheetml.sheet"
                        , "Statistics.xlsx");
            }


        }


        public async Task<IActionResult> GetChartData()
        {
            var artifactConditions = await _unitOfWork.ArtifactConditions
              .GetAllAsync();
            var artifactConditionsChart = artifactConditions
              .Select(t => new ChartDataViewModel
              {
                  Name = t.Name,
                  ArtifactCount = _unitOfWork.ArtifactConditions.GetArtifactCount(t.Id.ToString())

              });

            var chartData = new
            {
                Labels = artifactConditionsChart.Select(t => t.Name).ToArray(),
                Data = artifactConditionsChart.Select(t => t.ArtifactCount).ToArray()
            };

            return Json(chartData);
        }
    }
}
