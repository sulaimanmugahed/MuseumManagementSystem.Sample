namespace MuseumManagementSystem.Web.ViewModels
{
    public class SafePaginationViewModel
    {
      public int  TotalCount { get; set; }
      public int TotalPages { get; set; }
      public int CurrentPage { get; set; }
      public int PageSize { get; set; }
      public string SearchQuery { get; set; }
      public Guid StowageId { get; set; }
      public List<SafeViewModel> Safes { get; set; }
               
    }
}
