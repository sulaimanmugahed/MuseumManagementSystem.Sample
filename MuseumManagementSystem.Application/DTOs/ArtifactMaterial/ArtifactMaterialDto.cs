using MuseumManagementSystem.Application.DTOs.Artifact;
using MuseumManagementSystem.Application.DTOs.Material;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuseumManagementSystem.Application.DTOs.ArtifactMaterial
{
    public class ArtifactMaterialDto
    {
        public Guid ArtifactId { get; set; }
        public ArtifactDto Artifact { get; set; }

        public Guid MaterialId { get; set; }
        public MaterialDto Material { get; set; }
    }
}
