
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MuseumManagementSystem.Application.Constants;
using MuseumManagementSystem.Domain.Models;

namespace MuseumManagementSystem.Persistence.ModelsConfigurations
{
   public class BioDegConfig : IEntityTypeConfiguration<BioDeg>
   {
      public void Configure(EntityTypeBuilder<BioDeg> builder)
      {
         builder.ToTable("BioDegs", schema: "Main");
         builder.Property(b => b.Id).HasDefaultValueSql("(newid())");

         builder.Property(b => b.Name).HasColumnType("nvarchar").HasMaxLength(50);

      }
   }
}
