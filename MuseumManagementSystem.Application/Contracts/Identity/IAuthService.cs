using MuseumManagementSystem.Application.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuseumManagementSystem.Application.Contracts.Identity
{
    public interface IAuthService
    {
        Task<Result> SignInAsync(string emailOrUsername, string password,bool rememberMe);
        Task SignOutAsync();
    }
}
