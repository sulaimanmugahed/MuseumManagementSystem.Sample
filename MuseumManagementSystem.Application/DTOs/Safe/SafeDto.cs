using MuseumManagementSystem.Application.DTOs.Artifact;
using MuseumManagementSystem.Application.DTOs.Common;
using MuseumManagementSystem.Application.DTOs.Stowage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuseumManagementSystem.Application.DTOs.Safe
{
    public class SafeDto :BaseDto
    {
        public string Name { get; set; }

        public List<ArtifactDto> Artifacts { get; set; } = [];
        public StowageDto? Stowage { get; set; }
        public Guid StowageId { get; set; }
    }
}
