using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuseumManagementSystem.Application.Exceptions
{
    public class InvalidDataException : Exception
    {
        public InvalidDataException() { }
        public InvalidDataException(string message):base(message) { }
        public InvalidDataException(string message, Exception innerException):base(message, innerException) { }
      
      
    }
   
}
