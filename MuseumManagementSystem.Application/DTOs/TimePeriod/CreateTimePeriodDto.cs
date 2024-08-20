using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuseumManagementSystem.Application.DTOs.TimePeriod
{
    public class CreateTimePeriodDto : ITimePeriodDto
    {
        public string Name { get ; set; }
    }
}
