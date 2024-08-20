using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using MuseumManagementSystem.Application.Constants;
using MuseumManagementSystem.Application.Contracts.Identity;
using MuseumManagementSystem.Application.Contracts.Persistence;
using MuseumManagementSystem.Application.Exceptions;
using MuseumManagementSystem.Domain.Models;
using MuseumManagementSystem.Web.ExtensionMethods;
using MuseumManagementSystem.Web.Services;
using MuseumManagementSystem.Web.ViewModels;


namespace MuseumManagementSystem.Web.Controllers
{
    [Authorize(Policy = Policies.AllExcepyBase)]
    public class ArtifactsController(
         IUnitOfWork unitOfWork,
         IWebHostEnvironment webHostEnvironment,
         IStringLocalizer<ArtifactsController> localizer,
         IMapper mapper,
         ICacheService cacheService,
         IUserService userService
         ) : Controller
    {

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Create()
        {
            var viewModel = new ArtifactCreateViewModel();
            await LoadDropdownData(viewModel);
            return View(viewModel);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(ArtifactCreateViewModel model)
        {

            if (!ModelState.IsValid)
            {
                await LoadDropdownData(model);
                return View(model);
            }


            var artifact = mapper.Map<Artifact>(model);

            if (model.ArtifactTypesSelectList.SelectedItem is not null)
            {
                artifact.SetType(Guid.Parse(model.ArtifactTypesSelectList.SelectedItem));
            }

            if (model.ArtifactConditionsSelectList.SelectedItem is not null)
            {
                artifact.SetCondition(Guid.Parse(model.ArtifactConditionsSelectList.SelectedItem));
            }

            if (model.BioDegsSelectList.SelectedItem is not null)
            {
                artifact.SetBioDeg(Guid.Parse(model.BioDegsSelectList.SelectedItem));
            }

            if (model.SafesSelectList.SelectedItem is not null)
            {
                artifact.SetSafe(Guid.Parse(model.SafesSelectList.SelectedItem));
            }

            if (model.TimePeriodsSelectList.SelectedItem is not null)
            {
                artifact.SetTimePeriod(Guid.Parse(model.TimePeriodsSelectList.SelectedItem));
            }

            if (model.MaterialsSelectList.SelectedItem is not null)
            {
                var selectedmaterials = model.MaterialsSelectList.SelectedItem.Split(",").Select(m => Guid.Parse(m)).ToList();
                Guid importantMaterial = Guid.Empty;
                if (model.ImportantMaterialsSelectList.SelectedItem is not null)
                {
                    importantMaterial = selectedmaterials.FirstOrDefault(m => m == Guid.Parse(model.ImportantMaterialsSelectList.SelectedItem));
                }
                artifact.AddMaterials(selectedmaterials, importantMaterial);
            }


            //Upload Images
            if (Request.Form.Files.Count != 0)
            {
                foreach (var image in Request.Form.Files)
                {
                    
                        var imageUrl = await image.UploadImage(Folders.ArtifactImagesFolder,
                            artifact.Id.ToString(),
                            webHostEnvironment
                            );

                        artifact.AddImage(imageUrl);
                    
                }
            }

            await unitOfWork.Artifacts.AddAsync(artifact);
            await unitOfWork.SaveAsync();

            await cacheService.SetToHash(mapper.Map<ReportViewModel>(artifact), HashKeys.ArtifactReports.ToString(), artifact.Id.ToString());
            TempData["AlertMessage"] = localizer["successCreateMessage"].Value;
            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> Edit(Guid id)
        {

            var artifact = await unitOfWork.Artifacts.GetAsync(id,
                a => a.ArtifactMaterials,
                a => a.Images);

            if (artifact is null)
                return NotFound();


            var materials = await unitOfWork.Materials.GetAllAsync();
            var materialsSelectList = materials.ToMultipleSelectList(item => item.Id.ToString(),
                item => item.Name, artifact.ArtifactMaterials.Select(am => am.MaterialId.ToString()));

            var images = artifact.Images.Select(i => new ArtifactImageViewModel
            {
                Id = i.Id,
                Url = i.Url,
            }).ToList();

            var viewModel = mapper.Map<ArtifactEditViewModel>(artifact);
            viewModel.MaterialsSelectList.Items = materialsSelectList;

            await LoadDropdownDataWithSelectedValues(viewModel, artifact);
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ArtifactEditViewModel model)
        {

            var artifactToEdit = await unitOfWork.Artifacts.GetAsync(model.Id, a=> a.ArtifactMaterials);
            if (artifactToEdit is null)
                return NotFound();

            var materials = await unitOfWork.Materials.GetAllAsync();
            var materialsSelectList = materials.ToMultipleSelectList(item => item.Id.ToString(),
                item => item.Name, artifactToEdit.ArtifactMaterials.Select(am => am.MaterialId.ToString()));

            if (!ModelState.IsValid)
            {

                await LoadDropdownDataWithSelectedValues(model, artifactToEdit);
                model.MaterialsSelectList.Items = materialsSelectList;
                return View(model);
            }


            mapper.Map(model, artifactToEdit);


            if (model.ArtifactTypesSelectList.SelectedItem is not null)
            {
                artifactToEdit.SetType(Guid.Parse(model.ArtifactTypesSelectList.SelectedItem));
            }

            if (model.ArtifactConditionsSelectList.SelectedItem is not null)
            {
                artifactToEdit.SetCondition(Guid.Parse(model.ArtifactConditionsSelectList.SelectedItem));
            }

            if (model.BioDegsSelectList.SelectedItem is not null)
            {
                artifactToEdit.SetBioDeg(Guid.Parse(model.BioDegsSelectList.SelectedItem));
            }

            if (model.SafesSelectList.SelectedItem is not null)
            {
                artifactToEdit.SetSafe(Guid.Parse(model.SafesSelectList.SelectedItem));
            }

            if (model.TimePeriodsSelectList.SelectedItem is not null)
            {
                artifactToEdit.SetTimePeriod(Guid.Parse(model.TimePeriodsSelectList.SelectedItem));
            }

            if (model.MaterialsSelectList.SelectedItem is not null)
            {
                var selectedmaterials = model.MaterialsSelectList.SelectedItem.Split(",").Select(m => Guid.Parse(m)).ToList();
                Guid importantMaterial = Guid.Empty;
                if (model.ImportantMaterialsSelectList.SelectedItem is not null)
                {
                    importantMaterial = selectedmaterials.FirstOrDefault(m => m == Guid.Parse(model.ImportantMaterialsSelectList.SelectedItem));
                }
                artifactToEdit.UpdateMaterials(selectedmaterials, importantMaterial);
            }


            if (Request.Form.Files.Count != 0)
            {
                foreach (var image in Request.Form.Files)
                {
                    
                    
                        var imageUrl = await image.UploadImage(Folders.ArtifactImagesFolder,
                            artifactToEdit.Id.ToString(),
                            webHostEnvironment
                            );

                        await unitOfWork.ArtifactImages.AddAsync(
                        new ArtifactImage
                        {
                            Id = Guid.NewGuid(),
                            ArtifactId = model.Id,
                            Url = imageUrl,
                        });
                      
                }
            }




            await unitOfWork.Artifacts.UpdateAsync(artifactToEdit);

            await unitOfWork.SaveAsync();
            await cacheService.SetToHash(mapper.Map<ReportViewModel>(artifactToEdit), HashKeys.ArtifactReports.ToString(), artifactToEdit.Id.ToString());

            TempData["AlertMessage"] = localizer["successEditMessage"].Value;

            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {


            var artifact = await unitOfWork.Artifacts.GetAsync(id);
                if (artifact is null)
                return NotFound();

            await unitOfWork.Artifacts.DeleteAsync(artifact);
            await unitOfWork.SaveAsync();

            await cacheService.RemoveFromHash(HashKeys.ArtifactReports.ToString(), artifact.Id.ToString());

            return Json(new { message = localizer["successDeleteMessage"].Value });
        }

        public async Task<IActionResult> Details(Guid id)
        {

            var artifact = await unitOfWork.Artifacts.GetArtifactDetailAsync(id);
            if (artifact is null)
                return NotFound();

            var viewModel = mapper.Map<ArtifactDetailsViewModel>(artifact);
            viewModel.Materials = artifact.GetMaterialsName();
            viewModel.ImportantMaterial = artifact.GetImportantMaterialName();
            viewModel.CreatedBy = await userService.GetUserName(artifact.CreatedBy);
            viewModel.LastModifiedBy = await userService.GetUserName(artifact.LastModifiedBy);

            return View(viewModel);
        }

        public IActionResult ArtifactsWithoutSafe()
        {
            return View();
        }

        public IActionResult ArtifactsWithoutNewMuseumNumber()
        {
            return View();
        }
        public IActionResult ArtifactsWithoutOldMuseumNumber()
        {
            return View();
        }

        

        private async Task LoadDropdownData(ArtifactCreateViewModel viewModel)
        {

            var materials = await unitOfWork.Materials.GetAllAsync();
            viewModel.MaterialsSelectList.Items = materials?.ToSelectList();
            viewModel.ImportantMaterialsSelectList.Items = materials?.ToSelectList();

            var artifactConditions = await unitOfWork.ArtifactConditions.GetAllAsync();
            viewModel.ArtifactConditionsSelectList.Items = artifactConditions?.ToSelectList();

            var artifactSafes = await unitOfWork.Safes.GetAllAsync();
            viewModel.SafesSelectList.Items = artifactSafes?.ToSelectList();

            var artifactTimePeriods = await unitOfWork.TimePeriods.GetAllAsync();
            viewModel.TimePeriodsSelectList.Items = artifactTimePeriods?.ToSelectList();

            var artifactTypes = await unitOfWork.ArtifactTypes.GetAllAsync();
            viewModel.ArtifactTypesSelectList.Items = artifactTypes?.ToSelectList();

            var artifactBioDegs = await unitOfWork.BioDegs.GetAllAsync();
            viewModel.BioDegsSelectList.Items = artifactBioDegs?.ToSelectList();

        }

        private async Task LoadDropdownDataWithSelectedValues(ArtifactEditViewModel viewModel, Artifact artifact)
        {

            var artifactTypes = await unitOfWork.ArtifactTypes.GetAllAsync();
            viewModel.ArtifactTypesSelectList.Items = artifactTypes?.ToSelectList(artifact.ArtifactTypeId);

            var artifactTimePeriods = await unitOfWork.TimePeriods.GetAllAsync();
            viewModel.TimePeriodsSelectList.Items = artifactTimePeriods?.ToSelectList(artifact.TimePeriodId);

            var artifactSafes = await unitOfWork.Safes.GetAllAsync();
            viewModel.SafesSelectList.Items = artifactSafes?.ToSelectList(artifact.SafeId);

            var artifactConditions = await unitOfWork.ArtifactConditions.GetAllAsync();
            viewModel.ArtifactConditionsSelectList.Items = artifactConditions?.ToSelectList(artifact.ArtifactConditionId);

           var artifactBioDegs = await unitOfWork.BioDegs.GetAllAsync();
           viewModel.BioDegsSelectList.Items = artifactBioDegs?.ToSelectList(artifact.BioDegId);

            var artifactImportantMaterials = await unitOfWork.Materials.GetAllAsync();

            viewModel.ImportantMaterialsSelectList.Items = artifactImportantMaterials?
                .ToSelectList(artifact.ArtifactMaterials
                .FirstOrDefault(m => m.IsImportantMaterial)?.Material.Id);

        }

    }
}
