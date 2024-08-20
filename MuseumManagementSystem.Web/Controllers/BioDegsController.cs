using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using MuseumManagementSystem.Application.Contracts.Persistence;
using MuseumManagementSystem.Domain.Models;
using MuseumManagementSystem.Web.ExtensionMethods;
using MuseumManagementSystem.Web.ViewModels;
using System.Globalization;

namespace MuseumManagementSystem.Web.Controllers
{
    [Authorize]
    public class BioDegsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStringLocalizer<BioDegsController> _localizer;

        public BioDegsController(IUnitOfWork unitOfWork, IStringLocalizer<BioDegsController> localizer)
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
        public async Task<IActionResult> Create(BioDegViewModel model)
        {
            if(!ModelState.IsValid)
                return View(model);

            var isNameAssigned = _unitOfWork.BioDegs.IsNameAssigned(model.Name);
            if (isNameAssigned)
            {
                ModelState.AddModelError("Name", _localizer["bioDegNameAssignedValidation"].Value);
                return View(model);
            }

            var bioDeg = new BioDeg
            {
                Id = Guid.NewGuid(),
                Name = model.Name,
            };

           await _unitOfWork.BioDegs.AddAsync(bioDeg);
            await _unitOfWork.SaveAsync();

            TempData["AlertMessage"] = _localizer["successCreateMessage"].Value;

            return RedirectToAction(nameof(Index));
        }

       
        public async Task<IActionResult> Edit(Guid id)
        { 
            var bioDegToEdit = await _unitOfWork.BioDegs.GetAsync(id);
            if(bioDegToEdit == null)
                return NotFound();

            var viewModel = new BioDegViewModel
            {
                Id= bioDegToEdit.Id,
                Name = bioDegToEdit.Name,

            };

            return View(viewModel);  
        }

        [HttpPost]
        public async Task<IActionResult> Edit(BioDegViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var bioDegToEdit = await _unitOfWork.BioDegs.GetAsync(model.Id);
            if (bioDegToEdit == null)
                return NotFound();

            var IsNameAssigned = _unitOfWork.BioDegs.IsNameAssigned(model.Name,model.Id);
            if (IsNameAssigned)
            {
                ModelState.AddModelError("Name", _localizer["bioDegNameAssignedValidation"].Value);
                return View(model);
            }


            bioDegToEdit.Name = model.Name;

            await _unitOfWork.SaveAsync();

            TempData["AlertMessage"] = _localizer["successEditMessage"].Value;
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Delete(Guid id)
        {
            var bioDegToDelete = await _unitOfWork.BioDegs.GetAsync(id);
            if (bioDegToDelete == null)
                return NotFound();

            bool isContainArtifacts = _unitOfWork.BioDegs.IsContainArtifacts(id);
            if (isContainArtifacts)
                return BadRequest(_localizer["cantDeleteBioDegErrorMessage"].Value);


            await _unitOfWork.BioDegs.DeleteAsync(bioDegToDelete);
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


            var bioDegs = await _unitOfWork.BioDegs
                .GetAllAsync();
            var bioDegsChart = bioDegs?
                .Select(t => new ChartDataViewModel
                {
                    Name = t.Name,
                    ArtifactCount = _unitOfWork.BioDegs.GetArtifactCount(t.Id.ToString())

                });

            var loclaizedColumnNames = new Dictionary<string, string>
            {
                {"Name", _localizer["bioDeg"].Value },
                {"ArtifactCount", _localizer["artifactsCount"].Value },
            };

            var workbook = ExcelHelper.ExportToExcel(bioDegsChart, loclaizedColumnNames);
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
            var bioDegs = await _unitOfWork.BioDegs
              .GetAllAsync();
            var bioDegsChart = bioDegs?
              .Select(t => new ChartDataViewModel
              {
                  Name = t.Name,
                  ArtifactCount = _unitOfWork.BioDegs.GetArtifactCount(t.Id.ToString())

              });

            var chartData = new
            {
                Labels = bioDegsChart.Select(t => t.Name).ToArray(),
                Data = bioDegsChart.Select(t => t.ArtifactCount).ToArray()
            };

            return Json(chartData);
        }
    }
}
