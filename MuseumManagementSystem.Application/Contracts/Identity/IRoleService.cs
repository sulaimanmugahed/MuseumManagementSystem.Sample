using MuseumManagementSystem.Application.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuseumManagementSystem.Application.Contracts.Identity
{
    public interface IRoleService
    {
        List<Role> GetAll();
        Role GetById(string userId);
    }
}
