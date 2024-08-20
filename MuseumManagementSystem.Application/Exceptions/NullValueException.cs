using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuseumManagementSystem.Application.Exceptions
{
    public class NullValueException:Exception
    {
        public NullValueException() { }
        public NullValueException(string message) : base(message) { }
        public NullValueException(string message, Exception innerException) : base(message, innerException) { }
    }
}
