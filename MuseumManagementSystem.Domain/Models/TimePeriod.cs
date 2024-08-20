


namespace MuseumManagementSystem.Domain.Models
{
   public class TimePeriod : BaseEntity
    {
      public string? Name { get; set; }
      public List<Artifact> Artifacts { get; set; } = [];
   }
}
