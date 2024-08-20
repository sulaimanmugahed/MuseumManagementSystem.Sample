using System;
using System.Collections.Generic;
using System.Text;

namespace MuseumManagementSystem.Application.Responses
{
    public class BaseCommandResponse
    {
        public string Id { get; set; }
        public bool Success { get; set; } = true;
        public string Message { get; set; }
        public List<string> Errors { get; set; }
    }
}
