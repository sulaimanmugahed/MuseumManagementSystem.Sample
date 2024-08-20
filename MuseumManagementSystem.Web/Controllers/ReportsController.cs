using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using MuseumManagementSystem.Application.Contracts.Persistence;
using MuseumManagementSystem.Web.ViewModels;
using System.Globalization;

namespace MuseumManagementSystem.Web.Controllers
{
    public class ReportsController : Controller
    {

        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

      

    }
}
