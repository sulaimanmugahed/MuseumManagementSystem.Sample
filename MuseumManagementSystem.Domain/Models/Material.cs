


namespace MuseumManagementSystem.Domain.Models
{
   public class Material : BaseEntity
    {

      public string? Name { get; set; }
      public List<ArtifactMaterial> ArtifactMaterials { get; set; } = [];
   }
}
