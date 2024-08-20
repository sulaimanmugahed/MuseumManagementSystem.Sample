using MuseumManagementSystem.Application.DTOs.ArtifactType;
using MuseumManagementSystem.Application.DTOs.Common;
using MuseumManagementSystem.Application.DTOs.TimePeriod;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuseumManagementSystem.Application.DTOs.Artifact
{
    public class ArtifactListDto:BaseDto
    {
        public string Name { get; set; }
        public long SerialNumber { get; set; }
        public string? OldMuseumNumber { get; set; }
        public string? NewMuseumNumber { get; set; }
        public string? Count { get; set; }
        public ArtifactTypeDto? ArtifactType { get; set; }


    }
}
