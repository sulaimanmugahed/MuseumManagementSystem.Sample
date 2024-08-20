using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using MuseumManagementSystem.Application.Contracts.Persistence;
using MuseumManagementSystem.Application.Constants;
using MuseumManagementSystem.Application.Exceptions;


using MuseumManagementSystem.Web.ViewModels;
using MuseumManagementSystem.Web.Services;
using AutoMapper;

namespace MuseumManagementSystem.Web.Controllers
{
    [Authorize]
    public class RecycleBinController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IStringLocalizer<RecycleBinController> _localizer;
        private readonly ICacheService _cacheService;
        private readonly IMapper _mapper;

        public RecycleBinController(IUnitOfWork unitOfWork,
            IWebHostEnvironment webHostEnvironment,
            IStringLocalizer<RecycleBinController> localizer,
            ICacheService cacheService,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
            _localizer = localizer;
            _cacheService = cacheService;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }




        [HttpPost]
        public async Task<IActionResult> Recovery(Guid id)
        {
            var artifactToRecovery = await _unitOfWork.Artifacts.GetDeletedArtifactAsync(id);
            if (artifactToRecovery is null)
                return NotFound();

            await _unitOfWork.Artifacts.RecoveryAsync(id);
            await _unitOfWork.SaveAsync();
            await _cacheService.SetToHash(_mapper.Map<ReportViewModel>(artifactToRecovery), HashKeys.ArtifactReports.ToString(), artifactToRecovery.Id.ToString());

            return Json(new { message = _localizer["successRecoveryMessage"].Value });
        }




        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {

            var artifact = await _unitOfWork.Artifacts.GetDeletedArtifactAsync(id)
                ?? throw new NullValueException(_localizer["nullArtifactException"].Value);

            if (artifact.Images != null)
            {
                foreach (var image in artifact.Images)
                {
                    var existingPath = Path.Combine(_webHostEnvironment.WebRootPath, image.Url!);
                    if (System.IO.File.Exists(existingPath))
                    {
                        System.IO.File.Delete(existingPath);
                    }
                }
            }

            await _unitOfWork.Artifacts.DeleteAsync(artifact);
            await _unitOfWork.SaveAsync();

            return Json(new { message = _localizer["successDeleteMessage"].Value });
        }



    }
}
