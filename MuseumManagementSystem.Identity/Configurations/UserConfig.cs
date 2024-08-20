using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MuseumManagementSystem.Identity.Models;
using MuseumManagementSystem.Application.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuseumManagementSystem.Identity.Configurations
{
    public class UserConfig:IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            var hasher = new PasswordHasher<ApplicationUser>();
            builder.HasData(
                 new ApplicationUser
                 {
                     Id = SuperAdminDefaultData.Id,
                     Email = SuperAdminDefaultData.Email,
                     NormalizedEmail = SuperAdminDefaultData.Email.ToUpper(),
                     FirstName = SuperAdminDefaultData.FirstName,
                     LastName = SuperAdminDefaultData.LastName,
                     UserName = SuperAdminDefaultData.UserName,
                     NormalizedUserName = SuperAdminDefaultData.UserName.ToUpper(),
                     PasswordHash = hasher.HashPassword(null, SuperAdminDefaultData.Password),
                     EmailConfirmed = true
                 }
                 
            );

            builder.Property(x => x.FirstName).HasColumnType("nvarchar").HasMaxLength(15).IsRequired();
            builder.Property(x => x.LastName).HasColumnType("nvarchar").HasMaxLength(15).IsRequired();
        }
    }
}
