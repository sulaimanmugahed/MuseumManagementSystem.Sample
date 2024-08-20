using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MuseumManagementSystem.Web.ViewModels
{
    public class ArtifactTypeViewModel 
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "requiredFailedValidation")]

        [Remote("IsArtifactTypeNameAvilabel", "RemoteValidations", HttpMethod = "POST",
             AdditionalFields = "__RequestVerificationToken," + nameof(Id),
          ErrorMessage = "assigned_value")]
        [Display(Name = "name")]

        public string Name { get; set; }
        public int ArtifactsCount { get; set; }

    }
}
