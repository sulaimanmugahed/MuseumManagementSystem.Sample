using MuseumManagementSystem.Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace MuseumManagementSystem.Web.ViewModels
{
    public class ArtifactViewModel
    {
        public Guid Id { get; set; }

        public string? Name { get; set;}
       

        public long SerialNumber { get; set; } 
        
        public string? OldMuseumNumber { get; set; }

        public string? NewMuseumNumber { get; set; }

        public string? Count { get; set; } 

    }
}
