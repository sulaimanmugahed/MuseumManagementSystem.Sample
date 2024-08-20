
using Microsoft.EntityFrameworkCore;
using MuseumManagementSystem.Application.Contracts;
using MuseumManagementSystem.Domain.Models;
using MuseumManagementSystem.Domain.Models.Common;


namespace MuseumManagementSystem.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        private readonly ICurrentUserService _currentUserService;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> dbContextoptions, ICurrentUserService currentUserService)
            : base(dbContextoptions)
        {
            _currentUserService = currentUserService;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //aplly external config for model
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);



        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var user = _currentUserService.UserId;
            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is IAuditable auditableEntity)
                {
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            auditableEntity.Create(user);
                            break;

                        case EntityState.Modified:
                            auditableEntity.Update(user);
                            break;
                    }
                }
                if (entry.Entity is ISoftDeleteable softDeleteableEntity)
                {
                    switch (entry.State)
                    {
                        case EntityState.Unchanged:
                            if (softDeleteableEntity.IsDeleted)
                                softDeleteableEntity.Recovery();

                            break;

                        case EntityState.Deleted:
                            if (!softDeleteableEntity.IsDeleted)
                            {
                                softDeleteableEntity.Delete(user);
                                entry.State = EntityState.Modified;
                            }
                            break;
                    }
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }

        public DbSet<Artifact> Artifacts { get; set; }
        public DbSet<ArtifactImage> ArtifactImages { get; set; }
        public DbSet<ArtifactMaterial> ArtifactMaterials { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<Safe> Safes { get; set; }
        public DbSet<Stowage> Stowages { get; set; }
        public DbSet<ArtifactType> ArtifactTypes { get; set; }
        public DbSet<TimePeriod> TimePeriods { get; set; }
        public DbSet<BioDeg> BioDegs { get; set; }
        public DbSet<ArtifactCondition> ArtifactConditions { get; set; }



    }
}
