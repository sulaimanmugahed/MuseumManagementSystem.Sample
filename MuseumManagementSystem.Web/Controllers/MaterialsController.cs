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
    public class MaterialsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStringLocalizer<MaterialsController> _localizer;
        public MaterialsController(IUnitOfWork unitOfWork, IStringLocalizer<MaterialsController> localizer)
        {
            _unitOfWork = unitOfWork;

            _localizer = localizer;
        }

        public async Task<IActionResult> Index()
        {
            var materials = await _unitOfWork.Materials.GetAllAsync();
            var viewModel = materials?
                .Select(a => new MaterialViewModel()
                {
                    Id = a.Id,
                    Name = a.Name,

                });

            return View(materials);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(MaterialViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var material = new Material
            {
                Id = Guid.NewGuid(),
                Name = model.Name,
            };

            await _unitOfWork.Materials.AddAsync(material);
            await _unitOfWork.SaveAsync();
            TempData["AlertMessage"] = _localizer["done"].Value;
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var materialToEdit = await _unitOfWork.Materials.GetAsync(id);
            if (materialToEdit is null)
                return NotFound();


            var viewModel = new MaterialViewModel
            {
                Name = materialToEdit.Name,
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(MaterialViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var materialToEdit = await _unitOfWork.Materials.GetAsync(model.Id);
            materialToEdit.Name = model.Name;

            await _unitOfWork.SaveAsync();
            TempData["AlertMessage"] = _localizer["done"].Value;
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {

            var materialToDelete = await _unitOfWork.Materials.GetAsync(id);
            if (materialToDelete is null)
                return NotFound();

            var artfactHasMaterial = _unitOfWork.Artifacts.IsHasMaterial(id);
            if (artfactHasMaterial)
                return BadRequest(_localizer["cantDeleteMaterialErrorMessage"].Value);

            await _unitOfWork.Materials.DeleteAsync(materialToDelete);
            await _unitOfWork.SaveAsync();

            return Json(new { message = _localizer["done"].Value });



        }

        public async Task<IActionResult> GetChartData()
        {
            var materials = await _unitOfWork.Materials.GetAllAsync();
            var materialsChart = materials?
               .Select(m => new ChartDataViewModel
               {
                   Name = m.Name,
                   ArtifactCount = _unitOfWork.Artifacts.GetArtifactCountForMaterial(m.Id)
               });

            var chartData = new
            {
                data = materialsChart.Select(m => m.ArtifactCount).ToArray(),
                label = _localizer["artifactsCount"].Value,
                labels = materialsChart.Select(m => m.Name).ToArray(),
            };

            return Json(chartData);
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

            var materials = await _unitOfWork.Materials.GetAllAsync();
            var materialsChart = materials?
                          .Select(m => new ChartDataViewModel
                          {
                              Name = m.Name,
                              ArtifactCount = _unitOfWork.Artifacts.GetArtifactCountForMaterial(m.Id)
                          });

            var loclaizedColumnNames = new Dictionary<string, string>
            {
                {"Name", _localizer["material"].Value },
                {"ArtifactCount", _localizer["artifactsCount"].Value },
            };

            var workbook = ExcelHelper.ExportToExcel(materialsChart, loclaizedColumnNames);
            using (var stream = new MemoryStream())
            {
                workbook.SaveAs(stream);
                var content = stream.ToArray();

                return File(content, "application/vnd.openxmlformats-officedocumnt.spreadsheetml.sheet"
                        , "Statistics.xlsx");
            }


        }

    }
}
