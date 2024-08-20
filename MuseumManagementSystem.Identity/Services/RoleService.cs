using Microsoft.AspNetCore.Identity;
using MuseumManagementSystem.Application.Contracts.Identity;
using MuseumManagementSystem.Application.Models.Identity;
using MuseumManagementSystem.Application.Constants;
using MuseumManagementSystem.Application.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuseumManagementSystem.Identity.Services
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        public RoleService(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

    
        public List<Role> GetAll()
        {
            return _roleManager.Roles.Where(r => r.Name != Roles.SuperAdmin)
            .Select(role => new Role
            {
                Id = role.Id,
                Name = role.Name!,
               
            }).ToList();
        }


        public Role GetById(string Id)
        {
            var role = _roleManager.FindByIdAsync(Id).Result;
           
            return new Role
            {
                Id = role.Id,
                Name = role.Name
            };
        }
    }
}
