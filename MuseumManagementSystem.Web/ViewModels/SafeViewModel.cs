using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace MuseumManagementSystem.Web.ViewModels
{
    public class SafeViewModel
    {
        public Guid Id { get; set; }
        public Guid StowageId { get; set; }
        public string StowageName { get; set; }

        [Display(Name = "stowage")]
        public string SelectedStowage { get; set; }
        public List<SelectListItem>? Stowages { get; set; } = new();

        [Required(ErrorMessage = "requiredFailedValidation")]

        [Remote("IsTimePeriodNameAvilabel", "RemoteValidations", HttpMethod = "POST",
            AdditionalFields = "__RequestVerificationToken," + nameof(Id),
         ErrorMessage = "assigned_value")]
        [Display(Name = "name")]
        public string Name { get; set; }
        public int? ArtifactsCount { get; set; }
    }
}
