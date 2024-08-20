using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MuseumManagementSystem.Application.Constants;
using MuseumManagementSystem.Domain.Models;

namespace MuseumManagementSystem.Persistence.ModelsConfigurations
{
   public class ArtifactTypeConfig : IEntityTypeConfiguration<ArtifactType>
   {
      public void Configure(EntityTypeBuilder<ArtifactType> builder)
      {
         builder.ToTable("ArtifactTypes", schema: "Main");
         builder.Property(t => t.Id).HasDefaultValueSql("(newid())");

         builder.Property(t => t.Name).HasColumnType("nvarchar").HasMaxLength(50);


      }
   }
}
