using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MuseumManagementSystem.Domain.Models;

namespace MuseumManagementSystem.Persistence.ModelsConfigurations
{
   public class ArtifactImageConfig : IEntityTypeConfiguration<ArtifactImage>
   {
      public void Configure(EntityTypeBuilder<ArtifactImage> builder)
      {
         builder.ToTable("ArtifactImages", schema: "Main");
         builder.Property(i => i.Id).HasDefaultValueSql("(newid())");
         builder.Property(i => i.Url).HasColumnType("nvarchar").HasMaxLength(500);

         //Relation With Artifact
         builder.HasOne(i => i.Artifact)
             .WithMany(i => i.Images);

            builder.HasQueryFilter(i => !i.Artifact.IsDeleted);
      }
   }
}
