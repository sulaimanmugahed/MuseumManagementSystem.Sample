using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MuseumManagementSystem.Application.Constants;
using MuseumManagementSystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuseumManagementSystem.Persistence.ModelsConfigurations
{
    public class ArtifactConditionConfig : IEntityTypeConfiguration<ArtifactCondition>
    {
        public void Configure(EntityTypeBuilder<ArtifactCondition> builder)
        {
            builder.ToTable("ArtifactConditions", schema: "Main");
            builder.Property(t => t.Id).HasDefaultValueSql("(newid())");
            builder.Property(t => t.Name).HasColumnType("nvarchar").HasMaxLength(100);
        }
    }
}
