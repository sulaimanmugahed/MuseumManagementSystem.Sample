using MuseumManagementSystem.Application.DTOs.ArtifactCondition;
using MuseumManagementSystem.Application.DTOs.ArtifactImage;
using MuseumManagementSystem.Application.DTOs.ArtifactMaterial;
using MuseumManagementSystem.Application.DTOs.ArtifactType;
using MuseumManagementSystem.Application.DTOs.BioDeg;
using MuseumManagementSystem.Application.DTOs.Common;
using MuseumManagementSystem.Application.DTOs.Safe;
using MuseumManagementSystem.Application.DTOs.TimePeriod;
using MuseumManagementSystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuseumManagementSystem.Application.DTOs.Artifact
{
    public class ArtifactDto:BaseDto
    {
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
        public BioDegDto? BioDeg { get; set; }


        public Guid TimePeriodId { get; set; }
        public TimePeriodDto? TimePeriod { get; set; }


        public List<ArtifactImageDto> Images { get; set; } = new();

        public List<ArtifactMaterialDto> ArtifactMaterials { get; set; } = new();


        public Guid ArtifactTypeId { get; set; }
        public ArtifactTypeDto? ArtifactType { get; set; }


        public Guid ArtifactConditionId { get; set; }
        public ArtifactConditionDto? ArtifactCondition { get; set; }


        public Guid SafeId { get; set; }
        public SafeDto? Safe { get; set; }
    }
}
