using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace MuseumManagementSystem.Web.ViewModels
{
    public class SafeCreateViewModel
    {
      

        [Required(ErrorMessage = "requiredFailedValidation")]

        [Remote("IsSafeNameAvilabel", "RemoteValidations", HttpMethod = "POST",
       
       ErrorMessage = "assigned_value")]
        [Display(Name = "name")]
        public string Name { get; set; }

        [Display(Name = "stowage")]
        public string SelectedStowage { get; set; }
        public List<SelectListItem>? Stowages { get; set; } = new();
        
    }
}
