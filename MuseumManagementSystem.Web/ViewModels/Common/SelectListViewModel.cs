using Microsoft.AspNetCore.Mvc.Rendering;

namespace MuseumManagementSystem.Web.ViewModels.Common;

public class SelectListViewModel
{
    public string? SelectedItem { get; set; }
    public List<SelectListItem>? Items { get; set; } = new();
}
