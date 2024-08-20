using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace MuseumManagementSystem.Web.ViewModels
{
    public class SafeEditViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "requiredFailedValidation")]

        [Remote("IsSafeNameAvilabel", "RemoteValidations", HttpMethod = "POST",
       AdditionalFields = "__RequestVerificationToken," + nameof(Id),
       ErrorMessage = "assigned_value")]
        [Display(Name = "name")]
        public string Name { get; set; }

        [Display(Name = "stowage")]
        public string SelectedStowage { get; set; }
        public List<SelectListItem>? Stowages { get; set; } = new();
        
    }
}
