using MuseumManagementSystem.Application.Contracts.Identity;
using MuseumManagementSystem.Application.Models.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MuseumManagementSystem.Identity.Models;
using System.ComponentModel.DataAnnotations;

namespace MuseumManagementSystem.Identity.Services
{
    public class AuthService : IAuthService
    {

        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public AuthService(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<Result> SignInAsync(string emailOrUsername, string password, bool rememberMe)
        {
            var userName = new EmailAddressAttribute().IsValid(emailOrUsername) ?
                _userManager.FindByEmailAsync(emailOrUsername).Result!.UserName : emailOrUsername;

            var result = await _signInManager.PasswordSignInAsync(userName, password, rememberMe, false);
            if (!result.Succeeded)
                return new Result(false);

            return Result.Success();
        }

        public async Task SignOutAsync()
        {
           await _signInManager.SignOutAsync();
        }
    }
}
