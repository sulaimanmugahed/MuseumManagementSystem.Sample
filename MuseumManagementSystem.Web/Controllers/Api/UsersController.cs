using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MuseumManagementSystem.Application.Contracts.Identity;
using MuseumManagementSystem.Application.Models.Identity;
using MuseumManagementSystem.Application.Models;
using MuseumManagementSystem.Application.Constants;
using MuseumManagementSystem.Web.ViewModels;
using System.Data;
using MuseumManagementSystem.Web.Models;

namespace MuseumManagementSystem.Web.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController(IUserService userService, IMapper mapper) 
        : ControllerBase
    {


        [HttpPost]
        public async Task<IActionResult> GetUsers([FromForm] JQueryDataTable request)
        {

            var result = await userService
                .GetAllUsersPagedAsync(request.SearchValue,
                request.Skip,
                request.PageSize,
                request.SortColumn,
                request.SortColumnDirection);
           
            return Ok(new
            {
                recordsFiltered = result.RecordsFiltered,
                recordsTotal = result.RecordsTotal,
                data = mapper.Map<IEnumerable<UserViewModel>>(result.Data)
            });
        }
    }
}
