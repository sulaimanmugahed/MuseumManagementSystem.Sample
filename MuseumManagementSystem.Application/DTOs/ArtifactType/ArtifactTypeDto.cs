using MuseumManagementSystem.Application.DTOs.Artifact;
using MuseumManagementSystem.Application.DTOs.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuseumManagementSystem.Application.DTOs.ArtifactType
{
    public class ArtifactTypeDto:BaseDto
    {
        public string Name { get; set; }

        public List<ArtifactDto> Artifacts { get; set; } = [];

    }
}
