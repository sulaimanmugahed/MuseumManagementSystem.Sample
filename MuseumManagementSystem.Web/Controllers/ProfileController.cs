using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using MuseumManagementSystem.Application.Contracts;
using MuseumManagementSystem.Application.Contracts.Identity;
using MuseumManagementSystem.Application.Models.Identity;
using MuseumManagementSystem.Web.ExtensionMethods;
using MuseumManagementSystem.Web.Services;
using MuseumManagementSystem.Web.ViewModels;


namespace MuseumManagementSystem.Web.Controllers
{
    [Authorize]
    [Route("manage")]
    public class ProfileController : Controller
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<ProfileController> _localizer;
        private readonly IWebHostEnvironment _hostEnvironment;


        public ProfileController(
            IUserService userService,
            IMapper mapper,
            ICurrentUserService currentUserService,
            IStringLocalizer<ProfileController> localizer,
            IWebHostEnvironment hostEnvironment)
        {
            _userService = userService;
            _mapper = mapper;
            _currentUserService = currentUserService;
            _localizer = localizer;
            _hostEnvironment = hostEnvironment;
        }

        public ICurrentUserService CurrentUserService { get; }

        public async Task<IActionResult> Index()
        {
            var userId = _currentUserService.UserId;
            
            var currentUser = await _userService.GetByIdAsync(userId);
            if (currentUser is null)
            {
                return Unauthorized();
            }
            var viewModel = _mapper.Map<ProfileViewModel>(currentUser);
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Update(ProfileViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = _mapper.Map<User>(model);
            user.ProfilePicture = model.ProfilePictureUrl;
            if (model.ProfilePicture != null)
            {
                string imageToSave = await model.ProfilePicture
                .UploadImage("UserProfileImages", "profile",
                _hostEnvironment, model.ProfilePictureUrl);
                user.ProfilePicture = imageToSave;
            }

            var result = await _userService.UpdateProfile(user);
            if (!result.Succeeded)
                return BadRequest();

            return Json(new { message = _localizer["successEditMessage"].Value });
        }

    }
}
