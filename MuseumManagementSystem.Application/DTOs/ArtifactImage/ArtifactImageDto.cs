using MuseumManagementSystem.Application.DTOs.Artifact;
using MuseumManagementSystem.Application.DTOs.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuseumManagementSystem.Application.DTOs.ArtifactImage
{
    public class ArtifactImageDto:BaseDto
    {
        public string Name { get; set; }

        public string? Url { get; set; } = string.Empty;
        public Guid ArtifactId { get; set; }
        public ArtifactDto Artifact { get; set; } = new();
    }
}
