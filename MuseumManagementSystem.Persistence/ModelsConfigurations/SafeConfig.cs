using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MuseumManagementSystem.Application.Constants;
using MuseumManagementSystem.Domain.Models;

namespace MuseumManagementSystem.Persistence.ModelsConfigurations
{
   public class SafeConfig : IEntityTypeConfiguration<Safe>
   {
      public void Configure(EntityTypeBuilder<Safe> builder)
      {
         builder.ToTable("Safes", schema: "Main");
         builder.Property(sa => sa.Id).HasDefaultValueSql("(newid())");

         builder.Property(sa => sa.Name).HasColumnType("nvarchar").HasMaxLength(50);

         builder.HasOne(sa => sa.Stowage)
             .WithMany(st => st.Safes);

      }
   }
}
