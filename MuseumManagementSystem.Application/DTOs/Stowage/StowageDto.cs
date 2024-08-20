using MuseumManagementSystem.Application.DTOs.Artifact;
using MuseumManagementSystem.Application.DTOs.Common;
using MuseumManagementSystem.Application.DTOs.Safe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuseumManagementSystem.Application.DTOs.Stowage
{
    public class StowageDto : BaseDto
    {
        public string Name { get; set; }

        public List<SafeDto> Artifacts { get; set; } = [];
        public string? Address { get; set; }

    }
}
