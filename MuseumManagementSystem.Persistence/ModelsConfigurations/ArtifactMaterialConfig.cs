using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MuseumManagementSystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuseumManagementSystem.Persistence.ModelsConfigurations
{
    public class ArtifactMaterialConfig : IEntityTypeConfiguration<ArtifactMaterial>
    {
        public void Configure(EntityTypeBuilder<ArtifactMaterial> builder)
        {
            builder.ToTable("ArtifactMaterials", schema: "Main");

            //Set PrimaryKey(Composite)
            builder.HasKey(am => new { am.ArtifactId, am.MaterialId });

            builder.Navigation(am => am.Material).AutoInclude();

            //RelationWithArtifact
            builder.HasOne(am => am.Artifact)
                .WithMany(a => a.ArtifactMaterials)
                .HasForeignKey(am => am.ArtifactId);

            //RelationWithMaterial
            builder.HasOne(am => am.Material)
                .WithMany(m => m.ArtifactMaterials)
                .HasForeignKey(am => am.MaterialId);


            builder.HasQueryFilter(am => !am.Artifact.IsDeleted);


        }
    }
}
