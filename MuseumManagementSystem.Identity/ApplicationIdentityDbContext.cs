using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MuseumManagementSystem.Identity.Configurations;
using MuseumManagementSystem.Identity.Models;
using System.Reflection.Emit;

namespace MuseumManagementSystem.Identity;

public class ApplicationIdentityDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationIdentityDbContext(DbContextOptions<ApplicationIdentityDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<ApplicationUser>(entity =>
        {
            entity.ToTable("Users", "security");
        });

        builder.Entity<IdentityRole>(entity =>
        {
            entity.ToTable("Roles", "security");
        });
        builder.Entity<IdentityUserRole<string>>(entity =>
        {
            entity.ToTable("UserRoles", "security");
        });

        builder.Entity<IdentityUserClaim<string>>(entity =>
        {
            entity.ToTable("UserClaims", "security");
        });

        builder.Entity<IdentityUserLogin<string>>(entity =>
        {
            entity.ToTable("UserLogins", "security");
        });

        builder.Entity<IdentityRoleClaim<string>>(entity =>
        {
            entity.ToTable("RoleClaims", "security");

        });

        builder.Entity<IdentityUserToken<string>>(entity =>
        {
            entity.ToTable("UserTokens", "security");
        });

        builder.ApplyConfiguration(new RoleConfig());
        builder.ApplyConfiguration(new UserConfig());
        builder.ApplyConfiguration(new UserRoleConfig());
     
    }
}
