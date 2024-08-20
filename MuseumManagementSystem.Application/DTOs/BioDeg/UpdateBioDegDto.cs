using MuseumManagementSystem.Application.DTOs.BioDeg;
using MuseumManagementSystem.Application.DTOs.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuseumManagementSystem.Application.DTOs.BioDeg
{
    public class UpdateBioDegDto:BaseDto,IBioDegDto
    {
        public string Name { get; set; }

    }
}
