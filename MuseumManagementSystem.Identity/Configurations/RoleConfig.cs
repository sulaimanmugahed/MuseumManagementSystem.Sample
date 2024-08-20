using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MuseumManagementSystem.Application.Constants;

namespace MuseumManagementSystem.Identity.Configurations
{
    public class RoleConfig : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(

                  new IdentityRole
                  {
                      Id = "cbc43a8e-f7bb-4445-baaf-1add431ffbbf",
                      Name = Roles.SuperAdmin,
                      NormalizedName = Roles.SuperAdmin.ToUpper(),
                  },

                    new IdentityRole
                    {
                        Id = "cbc43a4e-f7bb-4445-baaf-1add431ccbbf",
                        Name = Roles.Admin,
                        NormalizedName = Roles.Admin.ToUpper(),
                    },

                new IdentityRole
                {
                    Id = "cac43a6e-f7bb-4448-baaf-1add431ccbbf",
                    Name = Roles.Member,
                    NormalizedName = Roles.Member.ToUpper(),
                },
                 new IdentityRole
                 {
                     Id = "aac43b6e-f7bb-4648-baaf-1add421ccbbf",
                     Name = Roles.Base,
                     NormalizedName = Roles.Base.ToUpper(),
                 }

            );
        }
    }
}
