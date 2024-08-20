

namespace MuseumManagementSystem.Domain.Models
{
   public class ArtifactType : BaseEntity
    {

      public string? Name { get; set; }


      public List<Artifact> Artifacts { get; set; } = [];
   }
}
