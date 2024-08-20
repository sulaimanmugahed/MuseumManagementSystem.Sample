using MuseumManagementSystem.Domain.Models;

namespace MuseumManagementSystem.Web.ViewModels
{
    public class ArtifactDetailsViewModel
    {
        public Guid Id { get; set; }
        public string SerialNumber { get; set; }  
        public string? Name { get; set; }
        public string? OldMuseumNumber { get; set; }
        public string? NewMuseumNumber { get; set; }
        public string? Count { get; set; }
        public string? Size { get; set; }
        public string? Description { get; set; }
        public string? Note { get; set; }
        public string? ArtifactType { get; set; }
        public string? ArtifactCondition { get; set; }
        public string? BioDeg { get; set; }
        public string? TimePeriod { get; set; }
        public string? Safe { get; set; }
        public string? ImportantMaterial { get; set; }

        public string? CreatedBy { get; set; }
        public string? DateCreated { get; set; }
        public string? LastModifiedDate { get; set; }
        public string? LastModifiedBy { get; set; }
        public string? ImageLink { get; set; }

       public string? Materials { get; set; }


        public List<ArtifactImageViewModel> Images { get; set; } = new();
    }
}
