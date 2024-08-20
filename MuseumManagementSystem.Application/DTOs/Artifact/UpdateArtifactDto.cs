using MuseumManagementSystem.Application.DTOs.ArtifactImage;
using MuseumManagementSystem.Application.DTOs.ArtifactMaterial;
using MuseumManagementSystem.Application.DTOs.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuseumManagementSystem.Application.DTOs.Artifact
{
    public class UpdateArtifactDto:BaseDto,IArtifactDto
    {
        public string Name { get; set; }

        public long SerialNumber { get; set; }
        public string? OldMuseumNumber { get; set; }
        public string? NewMuseumNumber { get; set; }
        public string? Count { get; set; }
        public string? Description { get; set; }
        public string? Size { get; set; }
        public string? Note { get; set; }
        public string? ImageLink { get; set; }
        public string? ImportantMaterial { get; set; }

        public Guid BioDegId { get; set; }

        public Guid TimePeriodId { get; set; }

        public List<ArtifactImageDto> Images { get; set; } = new();

        public List<ArtifactMaterialDto> ArtifactMaterials { get; set; } = new();

        public Guid ArtifactTypeId { get; set; }

        public Guid ArtifactConditionId { get; set; }


        public Guid SafeId { get; set; }
    }
}
