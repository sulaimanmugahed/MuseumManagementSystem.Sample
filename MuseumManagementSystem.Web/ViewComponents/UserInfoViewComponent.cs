using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using MuseumManagementSystem.Application.Contracts;
using MuseumManagementSystem.Application.Contracts.Identity;
using MuseumManagementSystem.Identity.Services;
using MuseumManagementSystem.Web.ViewModels;

namespace MuseumManagementSystem.Web.ViewComponents
{
    [Authorize]
    [ViewComponent(Name = "userinfo")]
    public class UserInfoViewComponent: ViewComponent
    {
        private readonly IUserService _userService;
        private readonly IStringLocalizer<UserInfoViewComponent> _localizer;
        private readonly ICurrentUserService _currentUserService;

        public UserInfoViewComponent(IUserService userService, IStringLocalizer<UserInfoViewComponent> localizer, ICurrentUserService currentUserService)
        {
            _userService = userService;
            _localizer = localizer;
            _currentUserService = currentUserService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var userId = _currentUserService.UserId;
            var currentUser = await _userService.GetByIdAsync(userId);

            if(currentUser is null)
            {
                throw new UnauthorizedAccessException();
            }

            var role = _userService.GetRoleName(currentUser.Id);
            ViewBag.Name = currentUser.FirstName ?? string.Empty;
            ViewBag.ProfilePicture = currentUser.ProfilePicture ?? "images/app1.jpg";
            ViewBag.Role = _localizer[$"nameOf{role}"].Value ?? string.Empty;
         
            
            return View();
        }

    }
}
