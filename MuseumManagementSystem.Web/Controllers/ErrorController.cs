using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using MuseumManagementSystem.Application.Contracts.Identity;
using MuseumManagementSystem.Application.Exceptions;

namespace MuseumManagementSystem.Web.Controllers
{
    
    public class ErrorController(IAuthService authService, ILogger<ErrorController> logger,IStringLocalizer<ErrorController> stringLocalizer) : Controller
    {
       

        [Route("Error/{statusCode}")]
        public async Task<IActionResult> HttpStatusCodeHandler(int statusCode)
        {
            if (HttpContext.Request.Headers.XRequestedWith == "XMLHttpRequest")
            {
               return BadRequest(stringLocalizer["something_wrong_msg"].Value);
            }
            else
            {
                switch (statusCode)
                {
                    case 404:
                        return View("NotFound");

                    case 500:
                        return View("InternalServerError");

                    case 403:
                        return Forbid();

                    case 401:
                        await authService.SignOutAsync();
                        return RedirectToAction("Login", "Account");


                    default:
                        return View(nameof(Error));

                }
            }

           
           
           
        }

        [Route(nameof(Error))]
        [AllowAnonymous]
        public IActionResult Error()
        {
            var exceptionDetailes = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            var exception = HttpContext.Features.Get<IExceptionHandlerPathFeature>()!.Error;
            int statusCode = 500;
            switch (exception)
            {
                case NullReferenceException nullValue:
                    statusCode = 400;
                    break;

                case UnauthorizedAccessException invalidData:
                    statusCode = 401;
                    break;

            }
            
         

            return StatusCode(statusCode);
        }
    }
}
