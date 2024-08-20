using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MuseumManagementSystem.Web.Services;



namespace MuseumManagementSystem.Web.Controllers;

[Authorize]
public class IntelligentSearchController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

  
}
