using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using MuseumManagementSystem.Application.Contracts.Identity;
using MuseumManagementSystem.Application.Models.Identity;
using MuseumManagementSystem.Application.Constants;
using MuseumManagementSystem.Domain.Models;
using MuseumManagementSystem.Application.Constants;
using MuseumManagementSystem.Identity.Models;
using MuseumManagementSystem.Web.ExtensionMethods;
using MuseumManagementSystem.Web.ViewModels;

using System;
using System.Net.Mail;

namespace MuseumManagementSystem.Web.Controllers
{
    [Authorize(Roles =Roles.SuperAdmin)]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        private readonly IStringLocalizer<UsersController> _localizer;
        private readonly IMapper _mapper;

        public UsersController(
            IStringLocalizer<UsersController> localizer,
            IUserService userService,
            IMapper mapper,
            IRoleService roleService)
        {
            _localizer = localizer;
            _userService = userService;
            _mapper = mapper;
            _roleService = roleService;
        }

        public  IActionResult Index()
        {
            return View();
        }


        public IActionResult Create()
        {
            var viewModel = new AddUserViewModel();
            LoadRolesSelectList(viewModel);
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AddUserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                LoadRolesSelectList(model);
                return View(model);
            }
            var user = _mapper.Map<User>(model);
            var result = await _userService.CreateAsync(user, model.Password,model.SelectedRole);
            if (!result.Succeeded)
                return BadRequest();

           



            TempData["AlertMessage"] = _localizer["successCreateMessage"].Value;
            return RedirectToAction(nameof(Index));
        }
      

        public async Task<IActionResult> Edit(string id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null)
                return NotFound();
            
            var selectedRole  = await _userService.GetRoleId(id);
            var viewModel = _mapper.Map<EditUserViewModel>(user);
            LoadRolesSelectList(viewModel, selectedRole);

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditUserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                LoadRolesSelectList(model);
                return View(model);
            }

            var userToEdit = await _userService.GetByIdAsync(model.Id);
            if (userToEdit == null)
                return NotFound();

            var user = _mapper.Map(model, userToEdit);
            var result = await _userService.UpdateAsync(user, model.SelectedRole,model.NewPassword);
            if (!result.Succeeded)
                return BadRequest();

            TempData["AlertMessage"] = _localizer["successEditMessage"].Value;
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {

            var user = await _userService.GetByIdAsync(id);

            if (user == null)
                return NotFound();
            
            var result = await _userService.DeleteAsync(user);
            if (!result.Succeeded)
            return BadRequest();

            return Json(new { message = _localizer["successDeleteMessage"].Value });

        }

        private void LoadRolesSelectList(AddUserViewModel model)
        {
            var roles = _roleService.GetAll();
            model.Roles = roles.ToSelectListWithLocalized("Id", "Name",_localizer);
        }
        private void LoadRolesSelectList(EditUserViewModel model,string? selectedValue = null)
        {
            var roles = _roleService.GetAll();
            model.Roles = roles.ToSelectListWithLocalized("Id", "Name", _localizer, selectedValue);
        }
    }
}
