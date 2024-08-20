using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuseumManagementSystem.Domain.Settings
{
    public class ImageSearchAppSettings
    {
        public string Host {  get; init; }
        public int Port { get; init; }
        public string Scheme { get; init; }
        public string AppUrl => $"{Scheme}://{Host}:{Port}/";
    }
}
