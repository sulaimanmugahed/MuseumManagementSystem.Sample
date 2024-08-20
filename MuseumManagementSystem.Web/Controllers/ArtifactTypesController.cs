using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using MuseumManagementSystem.Application.Contracts.Persistence;
using MuseumManagementSystem.Application.Constants;
using MuseumManagementSystem.Application.Exceptions;
using MuseumManagementSystem.Domain.Models;
using MuseumManagementSystem.Web.ExtensionMethods;
using MuseumManagementSystem.Web.ViewModels;
using System.Globalization;
using Microsoft.AspNetCore.Authorization;


namespace MuseumManagementSystem.Web.Controllers
{
    [Authorize]
    public class ArtifactTypesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStringLocalizer<ArtifactTypesController> _localizer;
        public ArtifactTypesController(IUnitOfWork unitOfWork, IStringLocalizer<ArtifactTypesController> localizer)
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
        public async Task<IActionResult> Create(ArtifactTypeViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var artifactType = new ArtifactType
            {
                Id = Guid.NewGuid(),
                Name = model.Name,
            };

            await _unitOfWork.ArtifactTypes.AddAsync(artifactType);
            await _unitOfWork.SaveAsync();
            TempData["AlertMessage"] = _localizer["successCreateMessage"].Value;
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var artifactTypeToEdit = await _unitOfWork.ArtifactTypes.GetAsync(id);
            if (artifactTypeToEdit is null)
                return NotFound();


            var viewModel = new ArtifactTypeViewModel
            {
                Name = artifactTypeToEdit.Name,

            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ArtifactTypeViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);


            var artifactTypeToEdit = await _unitOfWork.ArtifactTypes.GetAsync(model.Id);
            artifactTypeToEdit.Name = model.Name;
            await _unitOfWork.SaveAsync();
            TempData["AlertMessage"] = _localizer["successEditMessage"].Value;
            return RedirectToAction(nameof(Index));
        }



        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {

            var artifactTypeToDelete = await _unitOfWork.ArtifactTypes.GetAsync(id);
            if (artifactTypeToDelete is null)
                return NotFound();

            // errrrrrrrrrrrrrrrrooorrrrrr here

            if (artifactTypeToDelete.Artifacts.Any())
                return BadRequest(_localizer["cantDeleteTypeErrorMessage"].Value);


            await _unitOfWork.ArtifactTypes.DeleteAsync(artifactTypeToDelete);
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


            var artifactTypes = await _unitOfWork.ArtifactTypes
                .GetAllAsync();
            var artifactTypesChart = artifactTypes?
                .Select(t => new ChartDataViewModel
                {
                    Name = t.Name,
                    ArtifactCount = _unitOfWork.ArtifactTypes.GetArtifactCount(t.Id.ToString())

                });

            var loclaizedColumnNames = new Dictionary<string, string>
            {
                {"Name", _localizer["artifactType"].Value },
                {"ArtifactCount", _localizer["artifactsCount"].Value },
            };

            var workbook = ExcelHelper.ExportToExcel(artifactTypesChart, loclaizedColumnNames);
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
            var artifactTypes = await _unitOfWork.ArtifactTypes
              .GetAllAsync();
            var artifactTypesChart = artifactTypes?
              .Select(t => new ChartDataViewModel
              {
                  Name = t.Name,
                  ArtifactCount = _unitOfWork.ArtifactTypes.GetArtifactCount(t.Id.ToString())

              });

            var chartData = new
            {
                label = _localizer["artifactsCount"].Value,
                labels = artifactTypesChart.Select(t => t.Name).ToArray(),
                data = artifactTypesChart.Select(t => t.ArtifactCount).ToArray()
            };

            return Json(chartData);
        }





    }
}
