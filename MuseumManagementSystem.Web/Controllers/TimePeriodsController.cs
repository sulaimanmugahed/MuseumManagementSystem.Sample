using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using MuseumManagementSystem.Application.Contracts.Persistence;
using MuseumManagementSystem.Application.Exceptions;
using MuseumManagementSystem.Domain.Models;
using MuseumManagementSystem.Web.ExtensionMethods;
using MuseumManagementSystem.Web.ViewModels;
using System.Globalization;

namespace MuseumManagementSystem.Web.Controllers
{
    [Authorize]
    public class TimePeriodsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStringLocalizer<TimePeriodsController> _localizer;

        public TimePeriodsController(IUnitOfWork unitOfWork, IStringLocalizer<TimePeriodsController> localizer)
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
        public async Task<IActionResult> Create(TimePeriodViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            bool isNameAssigned = _unitOfWork.TimePeriods.IsNameAssigned(model.Name);
            if (isNameAssigned)
            {
                ModelState.AddModelError("Name", _localizer["timePeriodNameAssignedValidation"].Value);
                return View(model);
            }


            var timePeriod = new TimePeriod
            {
                Id = Guid.NewGuid(),
                Name = model.Name,
            };

            await _unitOfWork.TimePeriods.AddAsync(timePeriod);
            await _unitOfWork.SaveAsync();
            TempData["AlertMessage"] = _localizer["successCreateMessage"].Value;
            return RedirectToAction(nameof(Index));
        }



        public async Task<IActionResult> Edit(Guid id)
        {
            var timePeriodToEdit = await _unitOfWork.TimePeriods.GetAsync(id)
                ?? throw new NullValueException(_localizer["nullTimePeriodException"].Value);

            var viewModel = new TimePeriodViewModel
            {
                Id = timePeriodToEdit.Id,
                Name = timePeriodToEdit.Name,

            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(TimePeriodViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var isNameAssigned = _unitOfWork.TimePeriods.IsNameAssigned(model.Name, model.Id);
            if (isNameAssigned)
            {
                ModelState.AddModelError("Name", _localizer["timePeriodNameAssignedValidation"].Value);
                return View(model);
            }

            var timePeriodToEdit = await _unitOfWork.TimePeriods.GetAsync(model.Id);

            timePeriodToEdit.Name = model.Name;
            await _unitOfWork.SaveAsync();

            TempData["AlertMessage"] = _localizer["successEditMessage"].Value;
            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var timePeriodToDelete = await _unitOfWork.TimePeriods.GetAsync(id);
            if (timePeriodToDelete == null)
                return NotFound();

            bool isContainArtifacts = _unitOfWork.TimePeriods.IsContainArtifacts(id);
            if (isContainArtifacts)
                return BadRequest(_localizer["cantDeleteTimePeriodErrorMessage"].Value);


            await _unitOfWork.TimePeriods.DeleteAsync(timePeriodToDelete);
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


            var timePeriods = await _unitOfWork.TimePeriods.GetAllAsync();
            var timePeriodsChart = timePeriods?.Select(t =>
            new ChartDataViewModel
            {
                Name = t.Name,
                ArtifactCount = _unitOfWork.TimePeriods.GetArtifactCount(t.Id.ToString())
            });

            var loclaizedColumnNames = new Dictionary<string, string>
            {
                {"Name", _localizer["timePeriod"].Value },
                {"ArtifactCount", _localizer["artifactsCount"].Value },
            };

            var workbook = ExcelHelper.ExportToExcel(timePeriodsChart, loclaizedColumnNames);
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
            var timePeriods = await _unitOfWork.TimePeriods
              .GetAllAsync();
            var timePeriodsChart = timePeriods?.Select(t =>
            new ChartDataViewModel
            {
                Name = t.Name,
                ArtifactCount = _unitOfWork.TimePeriods.GetArtifactCount(t.Id.ToString())

            });

            var chartData = new
            {
                Labels = timePeriodsChart?.Select(t => t.Name).ToArray(),
                Data = timePeriodsChart?.Select(t => t.ArtifactCount).ToArray()
            };

            return Json(chartData);
        }

    }
}
