using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using MuseumManagementSystem.Application.Contracts.Identity;
using MuseumManagementSystem.Application.Contracts.Persistence;
using MuseumManagementSystem.Web.Models;
using System.Diagnostics;

namespace MuseumManagementSystem.Web.Controllers
{
   [Authorize]
   public class HomeController : Controller
   {


      private readonly IUnitOfWork _unitOfWork;
      private readonly IUserService _userService;
      private readonly IStringLocalizer<ProfileController> _localizer;
      private readonly ILogger<HomeController> _logger;

        public HomeController(IUnitOfWork unitOfWork, IUserService userService, IStringLocalizer<ProfileController> localizer,ILogger<HomeController> logger)
        {

         _unitOfWork = unitOfWork;
         _userService = userService;
         _localizer = localizer;
            _logger = logger;
        }



      public IActionResult Index()
      {
            return RedirectToAction("Index", "Statistics");

      }

      


      [AllowAnonymous]
      [HttpPost]
      public IActionResult SetLanguage(string culture, string returnUrl)
      {
         Response.Cookies.Append(
             CookieRequestCultureProvider.DefaultCookieName,
             CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
             new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
             );

         return LocalRedirect(returnUrl);
      }

      //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
      //public IActionResult Error()
      //{
      //   return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
      //}


   }



}






