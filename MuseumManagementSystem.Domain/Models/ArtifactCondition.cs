namespace MuseumManagementSystem.Domain.Models
{
   public class ArtifactCondition : BaseEntity
    {

      public string? Name { get; set; }

      public List<Artifact> Artifacts { get; set; } = [];
   }
}
