

namespace MuseumManagementSystem.Domain.Models
{
   public class Stowage : BaseEntity

    {
      public string? Name { get; set; }

      public string? Address { get; set; }

      public List<Safe> Safes { get; set; } = [];

   }
}
