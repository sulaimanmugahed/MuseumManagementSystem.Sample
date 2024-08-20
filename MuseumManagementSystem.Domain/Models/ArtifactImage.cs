namespace MuseumManagementSystem.Domain.Models
{
   public class ArtifactImage : BaseEntity
    {

      public string? Url { get; set; } = string.Empty;

      public Guid ArtifactId { get; set; }
      public Artifact Artifact { get; set; }


   }
}
