


namespace MuseumManagementSystem.Domain.Models
{
   public class BioDeg : BaseEntity
    {

      public string? Name { get; set; }
      public List<Artifact> Artifacts { get; set; } = [];
   }
}
