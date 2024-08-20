namespace MuseumManagementSystem.Domain.Models.Common;
public interface ISoftDeleteable
{
    public bool IsDeleted { get; set; }
    public DateTime? Deleted { get; set; }
    public string? DeletedBy { get; set; }


    public void Delete(string user)
    {
        DeletedBy = user;
        IsDeleted = true;
        Deleted = DateTime.Now;
    }

    public void Recovery()
    {
        DeletedBy = null;
        IsDeleted = false;
        Deleted = null;
    }



}
