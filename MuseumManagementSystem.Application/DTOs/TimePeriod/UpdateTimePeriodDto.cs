using MuseumManagementSystem.Application.DTOs.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuseumManagementSystem.Application.DTOs.TimePeriod
{
    public class UpdateTimePeriodDto:BaseDto, ITimePeriodDto
    {
        public string Name { get; set; }

    }
}
