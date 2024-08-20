namespace MuseumManagementSystem.Domain.Models.Common;
public interface IAuditable
{
   public DateTime DateCreated { get; set; }
   public string CreatedBy { get; set; }
   public DateTime? LastModifiedDate { get; set; }
   public string? LastModifiedBy { get; set; }


   public void Create(string user)
   {
      CreatedBy = user;
      DateCreated = DateTime.Now;
   }

   public void Update(string user)
   {
      LastModifiedBy = user;
      LastModifiedDate = DateTime.Now;
   }


}
