using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuseumManagementSystem.Application.Models.Identity
{
    public class Result
    {
        public bool Succeeded { get; set; }
        public List<string> Errors { get; set; } = [];
        public Result(bool succeeded, IList<string> errors = null)
        {
            Succeeded = succeeded;
            Errors = (List<string>)(errors ?? new List<string>());
        }

        public static Result Success() => new Result(true);
        

        public static Result Failed(IList<string> errors) => new Result(false,errors);
    }
}
