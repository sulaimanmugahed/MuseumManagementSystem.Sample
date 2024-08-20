using MuseumManagementSystem.Application.DTOs.ArtifactCondition;
using MuseumManagementSystem.Application.DTOs.ArtifactImage;
using MuseumManagementSystem.Application.DTOs.ArtifactMaterial;
using MuseumManagementSystem.Application.DTOs.ArtifactType;
using MuseumManagementSystem.Application.DTOs.Safe;
using MuseumManagementSystem.Application.DTOs.TimePeriod;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuseumManagementSystem.Application.DTOs.Artifact
{
    public class CreateArtifactDto:IArtifactDto
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
      

        public Guid ArtifactTypeId { get; set; }

        public Guid ArtifactConditionId { get; set; }


        public Guid SafeId { get; set; }
    }
}
