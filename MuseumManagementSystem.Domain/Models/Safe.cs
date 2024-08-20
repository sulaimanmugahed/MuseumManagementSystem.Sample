


namespace MuseumManagementSystem.Domain.Models
{
   public class Safe : BaseEntity
    {
      public string? Name { get; set; } 

      public List<Artifact> Artifacts { get; set; } = [];
      public Guid StowageId { get; set; }
      public Stowage? Stowage { get; set; }
   }
}
