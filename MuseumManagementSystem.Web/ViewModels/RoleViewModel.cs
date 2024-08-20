using Microsoft.AspNetCore.Mvc.Rendering;

namespace MuseumManagementSystem.Web.ViewModels
{
    public class RoleViewModel
    {
        public string SelectedRole { get; set; }
       public List<SelectListItem> Roles { get; set; }
    }
}
