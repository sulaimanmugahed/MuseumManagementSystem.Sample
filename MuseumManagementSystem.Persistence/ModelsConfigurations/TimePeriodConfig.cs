
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MuseumManagementSystem.Application.Constants;
using MuseumManagementSystem.Domain.Models;

namespace MuseumManagementSystem.Persistence.ModelsConfigurations
{
   internal class TimePeriodConfig : IEntityTypeConfiguration<TimePeriod>
   {
      public void Configure(EntityTypeBuilder<TimePeriod> builder)
      {
         builder.ToTable("TimePeriods", schema: "Main");
         builder.Property(t => t.Id).HasDefaultValueSql("(newid())");

         builder.Property(t => t.Name).HasColumnType("nvarchar").HasMaxLength(50);


      }
   }
}
