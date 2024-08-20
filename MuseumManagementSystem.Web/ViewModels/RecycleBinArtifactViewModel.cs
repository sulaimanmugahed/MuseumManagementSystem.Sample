using MuseumManagementSystem.Domain.Models;

namespace MuseumManagementSystem.Web.ViewModels
{
    public class RecycleBinArtifactViewModel
    {
        public Guid Id { get; set; } 
        public string? Name { get; set;}

        public long SerialNumber { get; set; }    
        public string? OldMuseumNumber { get; set; }

        public string? NewMuseumNumber { get; set; }

        public string? Count { get; set; } 

        public string? ArtifactType { get; set; }

        public string? Image {  get; set; } 

    }
}
