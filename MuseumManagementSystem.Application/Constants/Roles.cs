using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuseumManagementSystem.Application.Constants
{
    public abstract class Roles
    {
        
        public const string SuperAdmin = nameof(SuperAdmin);
        public const string Admin = nameof(Admin);
        public const string Member = nameof(Member);
        public const string Base = nameof(Base);
        public const string BaseId = "cac63a6e-f7bb-4648-baaf-1add731ccbbf";

    }


    public abstract class Policies
    {
        public const string AllExcepyBase = nameof(AllExcepyBase);

    }
}
