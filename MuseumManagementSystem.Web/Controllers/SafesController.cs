using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using MuseumManagementSystem.Application.Contracts.Persistence;
using MuseumManagementSystem.Application.Exceptions;
using MuseumManagementSystem.Domain.Models;
using MuseumManagementSystem.Web.Dtos;
using MuseumManagementSystem.Web.ExtensionMethods;
using MuseumManagementSystem.Web.ViewModels;

namespace MuseumManagementSystem.Web.Controllers
{
    [Authorize]
    public class SafesController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IStringLocalizer<SafesController> _localizer;
        public SafesController(IUnitOfWork unitOfWork, IStringLocalizer<SafesController> localizer)
        {
            _unitOfWork = unitOfWork;
            _localizer = localizer;
        }
        public IActionResult Index()
        {
            return View();
        }

       

        public async Task<IActionResult> ShowArtifacts(Guid id)
        {
            var safe = await _unitOfWork.Safes.GetAsync(id, s => s.Stowage);
            
            if(safe == null)
                return NotFound();

            var viewModel = new SafeViewModel
            {
                Id = safe.Id,
                Name = safe.Name!
            };
            return View(viewModel);
        }

        
        public async Task<IActionResult> Create()
        {
            var stowages = await _unitOfWork.Stowages.GetAllAsync();
            var stowageSelectList = new List<SelectListItem>();
            foreach (var stowage in stowages)
            {
                stowageSelectList.Add(new SelectListItem(stowage.Name,
                    stowage.Id.ToString()));
            }

            var viewModel = new SafeCreateViewModel
            {
                Stowages = stowageSelectList
            };

            return View(viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Create(SafeCreateViewModel model)
        {
            var stowages = await _unitOfWork.Stowages.GetAllAsync();
            var stowageSelectList = new List<SelectListItem>();
            foreach (var stowage in stowages)
            {
                stowageSelectList.Add(new SelectListItem(stowage.Name,
                    stowage.Id.ToString()));
            }

            if (!ModelState.IsValid)
            {  
                model.Stowages = stowageSelectList;
                return View(model);
            }

            bool IsNameAssigned = _unitOfWork.Safes.IsNameAssigned(model.Name);
            if (IsNameAssigned)
            {
                ModelState.AddModelError("Name", _localizer["safeNameAssignedValidation"].Value);
                model.Stowages = stowageSelectList;
                return View(model);

            }

            var safe = new Safe
            {
                Id = Guid.NewGuid(),
                Name = model.Name,
                StowageId = Guid.Parse(model.SelectedStowage)
            };
                
            await _unitOfWork.Safes.AddAsync(safe);
            await _unitOfWork.SaveAsync();
            TempData["AlertMessage"] = _localizer["successCreateMessage"].Value;
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var safeToEdit = await _unitOfWork.Safes.GetAsync(id);
            if (safeToEdit is null)
                return NotFound();

            var stowages = await _unitOfWork.Stowages.GetAllAsync();
            var selectedStowage = safeToEdit.StowageId.ToString();
            var stowageSelectList = new List<SelectListItem>();
            foreach (var stowage in stowages)
            {
                stowageSelectList.Add(new SelectListItem(stowage.Name, stowage.Id.ToString()
                    , selectedStowage == stowage.Id.ToString()));
            }

            var viewModel = new SafeEditViewModel
            {
                Id= id,
                Name = safeToEdit.Name,
                Stowages = stowageSelectList
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SafeEditViewModel model)
        { 
            var safeToEdit = await _unitOfWork.Safes.GetAsync(model.Id);
            
            var stowages = await _unitOfWork.Stowages.GetAllAsync();
            var selectedStowage = safeToEdit.StowageId.ToString();
            var stowageSelectList = new List<SelectListItem>();
            foreach (var stowage in stowages)
            {
                stowageSelectList.Add(new SelectListItem(stowage.Name, stowage.Id.ToString()
                    , selectedStowage == stowage.Id.ToString()));
            }

            
            if (!ModelState.IsValid)
            {
                model.Stowages = stowageSelectList;
                return View(model);
            }

            bool IsNameAssigned = _unitOfWork.Safes.IsNameAssigned(model.Name,safeToEdit.Id);
            if (IsNameAssigned)
            {
                ModelState.AddModelError("Name", _localizer["safeNameAssignedValidation"].Value);
                model.Stowages = stowageSelectList;
                return View(model);
            }

            safeToEdit.Name = model.Name;
            safeToEdit.StowageId = new Guid(model.SelectedStowage);

            await _unitOfWork.SaveAsync();
            TempData["AlertMessage"] = _localizer["successEditMessage"].Value;
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var safeToDelete = await _unitOfWork.Safes.GetAsync(id);
            var artifactInSafe = _unitOfWork.Artifacts.IsInSafe(id);
            if (safeToDelete == null)
            return NotFound();

            if (artifactInSafe)
                return BadRequest(_localizer["cantDeleteSafeErrorMessage"].Value);

            await _unitOfWork.Safes.DeleteAsync(safeToDelete);
            await _unitOfWork.SaveAsync();

            return Json(new { message = _localizer["successDeleteMessage"].Value });
        }





        [HttpPost]
        public async Task<IActionResult> ExportToExcelArtifactsBySafeId(Guid id)
        {

            var artifacts = await _unitOfWork.Artifacts.GetAllAsync(
                a=> a.SafeId == id,
                a=> a.ArtifactCondition,
                a => a.BioDeg,
                a => a.ArtifactType,
                a => a.ArtifactMaterials);

            var artifactDto = artifacts?.Select(a => new ArtifactsBySafeIdExportDto
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
            }).ToList();

            var loclaizedColumnNames = new Dictionary<string, string>();

            foreach (var column in typeof(ArtifactsBySafeIdExportDto).GetProperties())
            {
                loclaizedColumnNames.Add(column.Name, _localizer[column.Name.ToLower()].Value);
            }

            var workbook = ExcelHelper.ExportToExcel(artifactDto, loclaizedColumnNames);
            using (var stream = new MemoryStream())
            {
                workbook.SaveAs(stream);
                var content = stream.ToArray();

                return File(content, "application/vnd.openxmlformats-officedocumnt.spreadsheetml.sheet"
                        , "ArtifactsForSafe.xlsx");
            }

        }
    }
} 
