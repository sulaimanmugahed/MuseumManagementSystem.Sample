using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MuseumManagementSystem.Application.Constants;
using MuseumManagementSystem.Domain.Models;

namespace MuseumManagementSystem.Persistence.ModelsConfigurations
{
   public class StowageConfig : IEntityTypeConfiguration<Stowage>
   {
      public void Configure(EntityTypeBuilder<Stowage> builder)
      {
         builder.ToTable("Stowages", schema: "Main");
         builder.Property(s => s.Id).HasDefaultValueSql("(newid())");

         builder.Property(s => s.Name).HasColumnType("nvarchar").HasMaxLength(50);
         builder.Property(s => s.Address).HasColumnType("nvarchar").HasMaxLength(150);


      }
   }
}
