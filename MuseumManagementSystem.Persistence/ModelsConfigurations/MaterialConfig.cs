using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MuseumManagementSystem.Application.Constants;
using MuseumManagementSystem.Domain.Models;

namespace MuseumManagementSystem.Persistence.ModelsConfigurations
{
   public class MaterialConfig : IEntityTypeConfiguration<Material>
   {
      public void Configure(EntityTypeBuilder<Material> builder)
      {
         builder.ToTable("Material", schema: "Main");
         builder.Property(m => m.Id).HasDefaultValueSql("(newid())");

         builder.Property(m => m.Name).HasColumnType("nvarchar").HasMaxLength(50);

      }
   }
}
