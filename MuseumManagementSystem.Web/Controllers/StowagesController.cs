using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using MuseumManagementSystem.Application.Contracts.Persistence;
using MuseumManagementSystem.Application.Constants;
using MuseumManagementSystem.Domain.Models;
using MuseumManagementSystem.Web.ViewModels;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Authorization;
using MuseumManagementSystem.Web.Dtos;
using MuseumManagementSystem.Web.ExtensionMethods;

namespace MuseumManagementSystem.Web.Controllers
{
    [Authorize]
    public class StowagesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStringLocalizer<StowagesController> _localizer;
        public StowagesController(IUnitOfWork unitOfWork, IStringLocalizer<StowagesController> localizer)
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
        public async Task<IActionResult> Create(StowageViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            bool IsNameAssigned = _unitOfWork.Stowages.IsNameAssigned(model.Name);
            if (IsNameAssigned)
            {
                ModelState.AddModelError("Name", _localizer["stowageNameAssignedValidation"].Value);
                return View(model);

            }

            var stowage = new Stowage()
            {
                Id = Guid.NewGuid(),
                Name = model.Name,
                Address = model.Address,
            };

            await _unitOfWork.Stowages.AddAsync(stowage);
            await _unitOfWork.SaveAsync();
            TempData["AlertMessage"] = _localizer["successCreateMessage"].Value;
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Edit(Guid id)
        {
            var stowageToEdit = await _unitOfWork.Stowages.GetAsync(id);
            if (stowageToEdit == null)
                return NotFound();

            var viewModel = new StowageViewModel()
            {
                Id = stowageToEdit.Id,
                Name = stowageToEdit.Name,
                Address = stowageToEdit.Address,
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(StowageViewModel model)
        {

            if (!ModelState.IsValid)
                return View(model);

            var stowage = await _unitOfWork.Stowages.GetAsync(model.Id);
            if (stowage == null)
                return NotFound();

            bool IsNameAssigned = _unitOfWork.Stowages.IsNameAssigned(model.Name, stowage.Id);
            if (IsNameAssigned)
            {
                ModelState.AddModelError("Name", _localizer["stowageNameAssignedValidation"].Value);
                return View(model);
            }

            stowage.Name = model.Name;
            stowage.Address = model.Address;
            await _unitOfWork.SaveAsync();
            TempData["AlertMessage"] = _localizer["successEditMessage"].Value;
            return RedirectToAction(nameof(Index));
        }



        public async Task<IActionResult> ManageSafes(Guid id)
        {
            var stowage = await _unitOfWork.Stowages.GetAsync(id);
            if (stowage == null)
                return NotFound();

            var viewModel = new StowageViewModel
            {
                Id = stowage.Id,
                Name = stowage.Name,
            };
            return View(viewModel);

        }

        [HttpGet]
        public IActionResult AddNewSafe(Guid id)
        {
            var safe = new SafeViewModel()
            {
                StowageId = id,
            };
            return View(safe);
        }

        [HttpPost]
        public async Task<IActionResult> AddNewSafe(SafeViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);


            var safe = new Safe
            {
                Id = Guid.NewGuid(),
                Name = model.Name,
                StowageId = model.StowageId,

            };

            await _unitOfWork.Safes.AddAsync(safe);
            await _unitOfWork.SaveAsync();
            TempData["AlertMessage"] = _localizer["successCreateMessage"].Value;
            return RedirectToAction(nameof(ManageSafes), new { id = safe.StowageId });
        }

        public async Task<IActionResult> ShowArtifacts(Guid id)
        {
            var stowage = await _unitOfWork.Stowages.GetAsync(id);

            if (stowage == null)
                return NotFound();

            var viewModel = new StowageViewModel
            {
                Id = stowage.Id,
                Name = stowage.Name!
            };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var stowageToDelete = await _unitOfWork.Stowages.GetAsync(id);
            var IsHasSafes = _unitOfWork.Safes.IsInStowage(id);
            if (stowageToDelete == null)
                return NotFound();

            if (IsHasSafes)
                return BadRequest(_localizer["cantDeleteStowageErrorMessage"].Value);

            await _unitOfWork.Stowages.DeleteAsync(stowageToDelete);
            await _unitOfWork.SaveAsync();

            return Json(new { message = _localizer["successDeleteMessage"].Value });
        }

        [HttpPost]
        public async Task<IActionResult> ExportToExcelArtifactsByStowageId(Guid id)
        {

            var artifacts = await _unitOfWork.Artifacts.GetAllAsync(
                a => a.Safe.StowageId == id,
                a => a.ArtifactCondition,
                a => a.BioDeg,
                a => a.ArtifactType,
                a => a.Safe,
                a => a.ArtifactMaterials);

            var artifactDto = artifacts?.Select(a => new ArtifactsByStowageIdExportDto
            {
                Name = a.Name,
                SerialNumber = a.SerialNumber,
                NewMuseumNumber = a.NewMuseumNumber,
                OldMuseumNumber = a.OldMuseumNumber,
                Description = a.Description,
                Count = a.Count,
                Size = a.Size,
                Note = a.Note,
                ImportantMaterial = a.GetImportantMaterialName(),
                Materials = a.GetMaterialsName(),
                ArtifactCondition = a.ArtifactCondition?.Name,
                TimePeriod = a.TimePeriod?.Name,
                BioDeg = a.BioDeg?.Name,
                ArtifactType = a.ArtifactType?.Name,
                Safe = a.Safe?.Name
            }).ToList();

            var loclaizedColumnNames = new Dictionary<string, string>();

            foreach (var column in typeof(ArtifactsByStowageIdExportDto).GetProperties())
            {
                loclaizedColumnNames.Add(column.Name, _localizer[column.Name.ToLower()].Value);
            }

            var workbook = ExcelHelper.ExportToExcel(artifactDto, loclaizedColumnNames);
            using (var stream = new MemoryStream())
            {
                workbook.SaveAs(stream);
                var content = stream.ToArray();

                return File(content, "application/vnd.openxmlformats-officedocumnt.spreadsheetml.sheet"
                        , "ArtifactsForStowage.xlsx");
            }

        }
    }
}
