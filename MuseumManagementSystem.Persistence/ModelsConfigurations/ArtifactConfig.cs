using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MuseumManagementSystem.Application.Constants;
using MuseumManagementSystem.Domain.Models;


namespace MuseumManagementSystem.Persistence.ModelsConfigurations
{
   public class ArtifactConfig : IEntityTypeConfiguration<Artifact>
   {
      public void Configure(EntityTypeBuilder<Artifact> builder)
      {
         builder.ToTable("Artifacts", schema: "Main");
            builder.Property(i => i.Id).HasDefaultValueSql("(newid())");
            builder.Property(a => a.SerialNumber).HasColumnType("bigint");
         builder.Property(a => a.OldMuseumNumber).HasColumnType("nvarchar").HasMaxLength(50);
         builder.Property(a => a.NewMuseumNumber).HasColumnType("nvarchar").HasMaxLength(50);
         builder.Property(a => a.Name).HasColumnType("nvarchar").HasMaxLength(200);
         builder.Property(a => a.Description).HasColumnType("nvarchar").HasMaxLength(4000);
         builder.Property(a => a.Note).HasColumnType("nvarchar").HasMaxLength(4000);
         builder.Property(a => a.Count).HasColumnType("nvarchar").HasMaxLength(50);
         builder.Property(a => a.Size).HasColumnType("nvarchar").HasMaxLength(200);
         builder.Property(a => a.ImageLink).HasColumnType("nvarchar").HasMaxLength(1500);
         builder.Property(a => a.IsDeleted).HasDefaultValue(0);

         builder.HasQueryFilter(a => !a.IsDeleted);


    

         //Relation with ArtifactType many to one
         builder.HasOne(a => a.ArtifactType)
             .WithMany(t => t.Artifacts)
             .OnDelete(DeleteBehavior.Restrict);

         //Relation with Safe many to one
         builder.HasOne(a => a.Safe)
             .WithMany(t => t.Artifacts)
             .OnDelete(DeleteBehavior.Restrict);

         //Relation with TimePeriod many to one
         builder.HasOne(a => a.TimePeriod)
             .WithMany(t => t.Artifacts)
             .OnDelete(DeleteBehavior.Restrict);

         //Relation with BioDeg many to one
         builder.HasOne(a => a.BioDeg)
             .WithMany(t => t.Artifacts)
             .OnDelete(DeleteBehavior.Restrict);

         //Relation with ArtifactCondition many to one
         builder.HasOne(a => a.ArtifactCondition)
             .WithMany(c => c.Artifacts)
             .OnDelete(DeleteBehavior.Restrict);

            builder.Property<uint>("Version").IsRowVersion();

      }
   }
}
