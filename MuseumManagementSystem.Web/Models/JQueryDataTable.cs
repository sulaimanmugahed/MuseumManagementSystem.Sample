namespace MuseumManagementSystem.Web.Models
{
    public class JQueryDataTable
    {
        public int PageSize {  get; set; }
        public int Skip { get; set; }
        public string? SearchValue { get; set; }
        public string? SortColumn { get; set; }
        public string? SortColumnDirection { get; set; }

    }
}
