using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using MuseumManagementSystem.Application.Contracts.Identity;
using MuseumManagementSystem.Web.ViewModels;


namespace MuseumManagementSystem.Web.Controllers
{
   
    public class AccountController : Controller
    {
      
        private readonly IAuthService _authService;
        private readonly IStringLocalizer<AccountController> _localizer;

        public AccountController(
           
            IStringLocalizer<AccountController> localizer,
            IAuthService authService)
        {
        
            _localizer = localizer;
            _authService = authService;
        }

        [AllowAnonymous]
        public IActionResult Login(string returnUrl = null)
        {

            if (User.Identity!.IsAuthenticated)
                Response.Redirect(returnUrl);
            
            return View();
        }

       
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            if (!ModelState.IsValid)
                return View(model);
          
            var result =await _authService.SignInAsync(model.EmailOrUsername, model.Password,model.RememberMe);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", _localizer["invalidLogin"].Value);
                return View(model);
            }
                
            return LocalRedirect(returnUrl);
            
        }


        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _authService.SignOutAsync();
            return RedirectToAction("Index","Home");
        }


        public IActionResult AccessDenied()
        {
            return View();
        }



    }
}
